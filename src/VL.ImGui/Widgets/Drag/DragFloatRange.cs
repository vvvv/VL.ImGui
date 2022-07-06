using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;
using VL.Lib.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Float Range)", Category = "ImGui.Widgets")]
    internal partial class DragFloatRange2 : Widget
    {
        public string? Label { get; set; }

        public float Speed { private get; set; } = 0.01f;

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<Range<float>> Value { get; } = new BehaviorSubject<Range<float>>(new Range<float>(0, 0));

        internal override void Update(Context context)
        {
            var value = Value.Value;

            value.Split(out float from, out float to);
            if (ImGuiNET.ImGui.DragFloatRange2(Label ?? string.Empty, ref from, ref to, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(new Range<float>(from, to));
        }
    }
}
