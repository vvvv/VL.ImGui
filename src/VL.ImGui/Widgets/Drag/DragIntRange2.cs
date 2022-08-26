using VL.Lib.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Int Range)", Category = "ImGui.Widgets")]
    internal partial class DragIntRange2 : ChannelWidget<Range<int>>
    {
        public string? Label { get; set; }

        public int Speed { private get; set; } = 1;

        public int Min { private get; set; } = 0;

        public int Max { private get; set; } = 100;

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update();

            value.Split(out int from, out int to);
            if (ImGuiNET.ImGui.DragIntRange2(Label ?? string.Empty, ref from, ref to, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value = new Range<int>(from, to);
        }
    }
}
