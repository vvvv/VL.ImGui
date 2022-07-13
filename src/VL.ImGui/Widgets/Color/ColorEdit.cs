using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class ColorEdit : ChannelWidget<Color4>
    {
        public string? Label { get; set; }

        public ImGuiNET.ImGuiColorEditFlags Flags { private get; set; }

        public ColorEdit()
        {
            Value = Color4.White;
        }

        internal override void Update(Context context)
        {
            var value = Update().ToImGui();
            if (ImGuiNET.ImGui.ColorEdit4(Label ?? string.Empty, ref value, Flags))
                Value = value.ToVLColor4();
        }
    }
}
