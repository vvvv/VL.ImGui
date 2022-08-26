using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Int3)", Category = "ImGui.Widgets")]
    internal partial class InputInt3 : ChannelWidget<Int3>
    {

        public string? Label { get; set; }

        public int Step { private get; set; } = 1;

        public int StepFast { private get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update();

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.InputInt3(Label ?? string.Empty, ref x, Flags))
                Value = Unsafe.As<int, Int3>(ref x);
        }
    }
}
