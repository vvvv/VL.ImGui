using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class PlotLines : Widget
    {

        public string? Label { get; set; }

        public IEnumerable<float> Values { get; set; } = Enumerable.Empty<float>();

        public int VisibleCount { get; set; }

        public int Offset { get; set; }

        public string OverlayText { get; set; } = String.Empty;

        public float ScaleMin { get; set; }

        public float ScaleMax { get; set; }

        public Vector2 Size { get; set; }

        internal override void Update(Context context)
        {
            var values = Values.ToArray();
            ImGuiNET.ImGui.PlotLines(Label ?? string.Empty, ref values[0], VisibleCount, Offset, OverlayText, ScaleMin, ScaleMax, Size.ToImGui());
        }
    }
}
