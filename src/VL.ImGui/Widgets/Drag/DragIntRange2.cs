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
    [GenerateNode(Name = "Drag (Int Range)", Category = "ImGui.Widgets")]
    internal partial class DragIntRange2 : Widget
    {
        public string? Label { get; set; }

        public int Speed { private get; set; } = 1;

        public int Min { private get; set; } = 0;

        public int Max { private get; set; } = 100;

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<Range<int>> Value { get; } = new BehaviorSubject<Range<int>>(new Range<int>(0, 0));

        internal override void Update(Context context)
        {
            var value = Value.Value;

            value.Split(out int from, out int to);
            if (ImGuiNET.ImGui.DragIntRange2(Label ?? string.Empty, ref from, ref to, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(new Range<int>(from, to));
        }
    }
}
