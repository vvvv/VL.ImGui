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

        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public ImGuiNET.ImGuiSelectableFlags Flags { private get; set; }

        public BehaviorSubject<bool> Value { get; } = new BehaviorSubject<bool>(false);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.Selectable(Label ?? string.Empty, ref value, Flags, Size.ToImGui()))
                Value.OnNext(value);
        }
    }
}
