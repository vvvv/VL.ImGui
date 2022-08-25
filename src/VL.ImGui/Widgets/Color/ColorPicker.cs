using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class ColorPicker : ChannelWidget<Color4>
    {
        public string? Label { get; set; }

        public ImGuiNET.ImGuiColorEditFlags Flags { private get; set; }

        public ColorPicker()
        {
            Value = Color4.White;
        }

        internal override void UpdateCore(Context context)
        {
            var value = Update().ToImGui();
            if (ImGuiNET.ImGui.ColorPicker4(Label ?? string.Empty, ref value, Flags))
                Value = value.ToVLColor4();
        }
    }
}
