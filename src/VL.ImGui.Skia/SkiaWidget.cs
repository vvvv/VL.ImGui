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

        public Vector2 Size { private get; set; } = new Vector2(1f, 1f);

        public CommonSpace Space { private get; set; }

        internal override void UpdateCore(Context context)
        {
            if (context is SkiaContext skiaContext)
            {
                var _ = Size.FromHectoToImGui();
                skiaContext.Widget(new Vector2(_.X, _.Y), Render);
            }
        }

        void Render(CallerInfo caller, SKRect clipRect)
        {
            _transformLayer.Update(Layer, clipRect, Space, out _);
            _transformLayer.Render(caller);
        }
    }
}
