using ImGuiNET;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets.Primitives
{
    [GenerateNode(Category = "ImGui.Primitives", Name = "Bezier (Quadratic)")]
    internal partial class BezierQuadratic : PrimitiveWidget
    {
        public Vector2 Point1 { private get; set; }

        public Vector2 Point2 { private get; set; }

        public Vector2 Point3 { private get; set; }

        /// <summary>
        /// Use 0 to automatically calculate tessellation (preferred).
        /// </summary>
        public int SegmentsCount { private get; set; } = 0;

        public Color4 Color { private get; set; } = Color4.White;

        public float Thickness { private get; set; } = 1f;

        protected override void Draw(Context context, in ImDrawListPtr drawList, in System.Numerics.Vector2 offset)
        {
            var color = (uint)Color.ToRgba();

            drawList.AddBezierQuadratic(Point1.ToImGui() + offset, Point2.ToImGui() + offset, Point3.ToImGui() + offset, color, Thickness, SegmentsCount);
        }
    }
}
