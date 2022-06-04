using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Checkbox")]
    internal partial class Checkbox : Widget
    {

        public string? Label { get; set; }

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

        public BehaviorSubject<Boolean> ObservableValue { get; } = new BehaviorSubject<Boolean>(false);

        internal override void Update(Context context)
        {
            var value = ObservableValue.Value;
            if (ImGuiNET.ImGui.Checkbox(Label ?? string.Empty, ref value))
                ObservableValue.OnNext(value);
        }
    }
}
