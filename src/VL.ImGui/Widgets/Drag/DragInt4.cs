using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Int4)", Category = "ImGui.Widgets")]
    internal partial class DragInt4 : ChannelWidget<Int4>
    {
        public string? Label { get; set; }

        public int Speed { private get; set; } = 1;

        public int Min { private get; set; } = 0;

        public int Max { private get; set; } = 100;

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var value = Update();

            ref var x = ref value.X;
            if (ImGuiNET.ImGui.DragInt4(Label ?? string.Empty, ref x, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value = Unsafe.As<int, Int4>(ref x);
        }
    }
}
