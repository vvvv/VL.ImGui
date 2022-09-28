using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Int2)", Category = "ImGui.Widgets", Tags = "number, updown")]
    internal partial class InputInt2 : ChannelWidget<Int2>
    {

        public string? Label { get; set; }

        public int Step { private get; set; } = 1;

        public int StepFast { private get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update();

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.InputInt2(Label ?? string.Empty, ref x, Flags))
                Value = Unsafe.As<int, Int2>(ref x);
        }
    }
}
