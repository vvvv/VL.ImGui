using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class Selectable : Widget
    {
        private bool _value;

        public bool Value
        {
            get => ObservableValue.Value;
            set
            {
                if (value != _value)
                {
                    _value = value;
                    ObservableValue.OnNext(value);
                }
            }
        }

        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public ImGuiNET.ImGuiSelectableFlags Flags { private get; set; }

        public BehaviorSubject<bool> ObservableValue { get; } = new BehaviorSubject<bool>(false);

        internal override void Update(Context context)
        {
            var value = ObservableValue.Value;
            var size = ImGuiConversion.FromVector2(Size);
            if (ImGuiNET.ImGui.Selectable(Label ?? string.Empty, ref value, Flags, size))
                ObservableValue.OnNext(value);
        }
    }
}
