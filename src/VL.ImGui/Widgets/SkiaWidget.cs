using SkiaSharp;
using Stride.Core.Mathematics;
using VL.Skia;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", IsStylable = false)]
    public sealed partial class SkiaWidget : Widget
    {
        private readonly InViewportUpstream _transformLayer = new InViewportUpstream();

        public ILayer? Layer { private get; set; }

        public Vector2 Size { private get; set; } = new Vector2(100, 100);

        public CommonSpace Space { private get; set; }

        internal override void UpdateCore(Context context)
        {
            if (context is SkiaContext skiaContext)
            {
                skiaContext.Widget(Size, Render);
            }
        }

        void Render(CallerInfo caller, SKRect clipRect)
        {
            _transformLayer.Update(Layer, clipRect, Space, out _);
            _transformLayer.Render(caller);
        }
    }
}
