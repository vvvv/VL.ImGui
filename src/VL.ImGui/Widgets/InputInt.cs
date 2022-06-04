using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Int)")]
    internal partial class InputInt : Widget
    {
        private int _value;

        public int Value
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

        public int Step { private get; set; } = 1;

        public int StepFast { private get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<int> ObservableValue { get; } = new BehaviorSubject<int>(0);

        internal override void Update(Context context)
        {
            var value = ObservableValue.Value;
            if (ImGuiNET.ImGui.InputInt(Label ?? string.Empty, ref value, Step, StepFast, Flags))
                ObservableValue.OnNext(value);
        }
    }
}
