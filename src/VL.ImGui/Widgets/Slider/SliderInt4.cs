using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Slider (Int4)")]
    internal partial class SliderInt4 : Widget
    {
        public string? Label { get; set; }

        public int Min { private get; set; } = 0;

        public int Max { private get; set; } = 100;

        /// <summary>
        /// Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "%.3f" -> 1.234; "%5.2f secs" -> 01.23 secs; "Biscuit: % .0f" -> Biscuit: 1; etc.
        /// </summary>
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<Int4> Value { get; } = new BehaviorSubject<Int4>(Int4.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value;

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.SliderInt4(Label ?? string.Empty, ref x, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(Unsafe.As<int, Int4>(ref x));
        }
    }
}
