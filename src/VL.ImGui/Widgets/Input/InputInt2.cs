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
    [GenerateNode(Name = "Input (Int2)", Category = "ImGui.Widgets")]
    internal partial class InputInt2 : Widget
    {

        public string? Label { get; set; }

        public int Step { private get; set; } = 1;

        public int StepFast { private get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<Int2> Value { get; } = new BehaviorSubject<Int2>(Int2.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value;

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.InputInt2(Label ?? string.Empty, ref x, Flags))
                Value.OnNext(Unsafe.As<int, Int2>(ref x));
        }
    }
}
