﻿using ImGuiNET;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets.Primitives
{
    [GenerateNode(Category = "ImGui.Primitives")]
    internal partial class Triangle : PrimitiveWidget
    {
        public Vector2 Point1 { private get; set; } = new Vector2(0, 100);

        public Vector2 Point2 { private get; set; } = new Vector2(50, 0);

        public Vector2 Point3 { private get; set; } = new Vector2(100, 100);

        public Color4 Color { private get; set; } = Color4.White;

        /// <summary>
        /// Filled shapes must always use clockwise winding order. The anti-aliasing fringe depends on it. Counter-clockwise shapes will have "inward" anti-aliasing.
        /// </summary>
        public bool IsFilled { private get; set; } = false;

        public float Thickness { private get; set; } = 1f;

        protected override void Draw(Context context, in ImDrawListPtr drawList, in System.Numerics.Vector2 offset)
        {
            var color = (uint)Color.ToRgba();

            if (IsFilled)
            {
                drawList.AddTriangleFilled(Point1.ToImGui() + offset, Point2.ToImGui() + offset, Point3.ToImGui() + offset, color);
            }
            else
            {
                drawList.AddTriangle(Point1.ToImGui() + offset, Point2.ToImGui() + offset, Point3.ToImGui() + offset, color, Thickness);
            }
        }
    }
}
