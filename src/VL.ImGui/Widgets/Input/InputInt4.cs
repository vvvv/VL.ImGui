using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Int4)")]
    internal partial class InputInt4 : Widget
    {

        public string? Label { get; set; }

        public int Step { private get; set; } = 1;

        public int StepFast { private get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<Int4> Value { get; } = new BehaviorSubject<Int4>(Int4.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value;

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.InputInt4(Label ?? string.Empty, ref x, Flags))
                Value.OnNext(Unsafe.As<int, Int4>(ref x));
        }
    }
}
