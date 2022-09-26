using ImGuiNET;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets.Primitives
{
    [GenerateNode(Category = "ImGui.Primitives")]
    internal partial class Rect : PrimitiveWidget
    {
        public Vector2 TopLeft { private get; set; } = Vector2.Zero;

        public Vector2 BottomRight { private get; set; } = new Vector2(100, 100);

        public float Rounding { private get; set; }

        public Color4 Color { private get; set; } = Color4.White;

        /// <summary>
        /// Filled shapes must always use clockwise winding order. The anti-aliasing fringe depends on it. Counter-clockwise shapes will have "inward" anti-aliasing.
        /// </summary>
        public bool IsFilled { private get; set; } = false;

        public float Thickness { private get; set; } = 1f;

        public ImDrawFlags Flags { private get; set; }

        protected override void Draw(Context context, in ImDrawListPtr drawList, in System.Numerics.Vector2 offset)
        {
            var color = (uint)Color.ToRgba();

            if (IsFilled)
            {
                drawList.AddRectFilled(TopLeft.ToImGui() + offset, BottomRight.ToImGui() + offset, color, Rounding, Flags);
            }
            else
            {
                drawList.AddRect(TopLeft.ToImGui() + offset, BottomRight.ToImGui() + offset, color, Rounding, Flags, Thickness);
            }
        }
    }
}
