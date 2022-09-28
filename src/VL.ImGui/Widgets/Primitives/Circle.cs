using ImGuiNET;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets.Primitives
{
    [GenerateNode(Category = "ImGui.Primitives")]
    internal partial class Circle : PrimitiveWidget
    {
        public Vector2 Center { private get; set; } = Vector2.Zero;

        public float Radius { private get; set; } = 100f;

        public Color4 Color { private get; set; } = Color4.White;

        public bool IsFilled { private get; set; } = false;

        public float Thickness { private get; set; } = 1f;

        protected override void Draw(Context context, in ImDrawListPtr drawList, in System.Numerics.Vector2 offset)
        {
            var color = (uint)Color.ToRgba();

            if (IsFilled)
            {
                drawList.AddCircleFilled(Center.ToImGui() + offset, Radius, color);
            }
            else
            {
                drawList.AddCircle(Center.ToImGui() + offset, Radius, color, 0, Thickness);
            }
        }
    }
}
