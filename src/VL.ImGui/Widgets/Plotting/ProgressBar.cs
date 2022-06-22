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
    internal partial class ProgressBar : Widget
    {

        public string? Label { get; set; }

        public float Fraction { get; set; }

        public string OverlayText { get; set; } = String.Empty;

        public Vector2 Size { get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.ProgressBar(Fraction, Size.ToImGui(), OverlayText);        }
    }
}
