using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Slider (Float)")]
    internal partial class SliderFloat : Widget
    {
        public string? Label { get; set; }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        [Documentation(@"Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "" % .3f"" -> 1.234; "" % 5.2f secs"" -> 01.23 secs; ""Biscuit: % .0f"" -> Biscuit: 1; etc.")]
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<float> Value { get; } = new BehaviorSubject<float>(0f);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.SliderFloat(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value);
        }
    }
}
