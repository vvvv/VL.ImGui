using SkiaSharp;
using Stride.Core.Mathematics;
using System;
using VL.Core;
using VL.Skia;

namespace VL.ImGui.Widgets
{
    [GenerateNode(GenerateImmediate = false)]
    public sealed partial class ImmediateModeWidget : Widget
    {
        public Action<Context>? Updator { get; set; }

        public void Update(Action<Context> updator)
        {
            Updator = updator;
        }

        internal override void Update(Context context)
        {
            Updator?.Invoke(context);
        }
    }
}
