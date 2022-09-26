using ImGuiNET;
using Stride.Core.Mathematics;
using VL.Lib.Collections;

namespace VL.ImGui.Widgets.Primitives
{
    [GenerateNode(Category = "ImGui.Primitives", Tags ="Segment")]
    internal partial class NGon : PrimitiveWidget
    {
        public Vector2 Center { private get; set; } = Vector2.Zero;

        public float Radius { private get; set; } = 100f;

        public int SegmentsCount { private get; set; } = 3;

        public Color4 Color { private get; set; } = Color4.White;

        public bool IsFilled { private get; set; } = false;

        public float Thickness { private get; set; } = 1f;

        protected override void Draw(Context context, in ImDrawListPtr drawList, in System.Numerics.Vector2 offset)
        {
            var color = (uint)Color.ToRgba();

            if (IsFilled)
            {
                drawList.AddNgonFilled(Center.ToImGui(), Radius, color, SegmentsCount);
            }
            else
            {
                drawList.AddNgon(Center.ToImGui(), Radius, color, SegmentsCount, Thickness);
            }
        }
    }
}
