using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Slider (Int)")]
    internal partial class SliderInt : Widget
    {
        public string? Label { get; set; }

        public int Min { private get; set; } = 0;

        public int Max { private get; set; } = 100;

        /// <summary>
        /// Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "%.3f" -> 1.234; "%5.2f secs" -> 01.23 secs; "Biscuit: % .0f" -> Biscuit: 1; etc.
        /// </summary>
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<int> Value { get; } = new BehaviorSubject<int>(0);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.SliderInt(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value);
        }
    }
}
