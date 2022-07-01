using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Int3)", Category = "ImGui.Widgets")]
    internal partial class DragInt3 : Widget
    {
        public string? Label { get; set; }

        public int Speed { private get; set; } = 1;

        public int Min { private get; set; } = 0;

        public int Max { private get; set; } = 100;

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<Int3> Value { get; } = new BehaviorSubject<Int3>(Int3.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value;

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.DragInt3(Label ?? string.Empty, ref x, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(Unsafe.As<int, Int3>(ref x));
        }
    }
}
