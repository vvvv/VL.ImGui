using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Float)")]
    internal partial class InputFloat : Widget
    {

        public string? Label { get; set; }

        public float Step { private get; set; } = 0f;

        public float StepFast { private get; set; } = 0f;

        [Documentation(@"Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "" % .3f"" -> 1.234; "" % 5.2f secs"" -> 01.23 secs; ""Biscuit: % .0f"" -> Biscuit: 1; etc.")]
        public string? Format { private get; set; } = "%.3f";

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<float> Value { get; } = new BehaviorSubject<float>(0f);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.InputFloat(Label ?? string.Empty, ref value, Step, StepFast, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value);
        }
    }
}
