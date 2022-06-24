using SkiaSharp;
using Stride.Core.Mathematics;
using System;
using VL.Core;
using VL.Skia;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    public sealed partial class RetainedModeWidget : Widget
    {
        public Widget Widget { get; set; }

        internal override void Update(Context context)
        {
            Widget.Update(context);
        }
    }
}
