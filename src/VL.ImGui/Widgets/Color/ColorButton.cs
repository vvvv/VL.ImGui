using Stride.Core.Mathematics;
using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Button = true, Tags = "rgba, hsv, hsl")]
    internal partial class ColorButton : ChannelWidget<Unit>
    {
        public string? Label { get; set; }

        public Color4 Color { private get; set; }

        public Vector2 Size { private get; set; }

        public ImGuiNET.ImGuiColorEditFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.ColorButton(Label ?? string.Empty, Color.ToImGui(), Flags, Size.ToImGui()))
                Value = Unit.Default;
        }
    }
}
