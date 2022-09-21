using SkiaSharp;
using Stride.Core.Mathematics;
using System;
using VL.Core;
using VL.Skia;

namespace VL.ImGui.Widgets
{
    [GenerateNode(GenerateRetained = false, Category = "ImGui.Internal")]
    public sealed partial class RetainedMode : Widget
    {
        public Widget Widget { get; set; }

        internal override void UpdateCore(Context context)
        {
            context.Update(Widget);
        }
    }
}
