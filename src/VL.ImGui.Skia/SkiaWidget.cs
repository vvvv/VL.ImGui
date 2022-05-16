using SkiaSharp;
using Stride.Core.Mathematics;
using System;
using VL.Core;
using VL.Skia;

namespace VL.ImGui.Skia
{
    public sealed class SkiaWidget : Widget
    {
        private readonly InViewportUpstream _transformLayer = new InViewportUpstream();

        public ILayer? Layer { private get; set; }

        public Vector2 Size { private get; set; } = new Vector2(100, 100);

        public CommonSpace Space { private get; set; }

        internal override void Update(Context context)
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

        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {
            return factory.NewNodeDescription(nameof(SkiaWidget), "ImGui", fragmented: true, _c =>
            {
                var _w = new SkiaWidget();
                var _inputs = new[]
                {
                    _c.Input("Layer", _w.Layer),
                    _c.Input("Size", _w.Size),
                    _c.Input("Space", _w.Space)
                };
                var _outputs = new[]
                {
                    _c.Pin("Output", typeof(Widget))
                };
                return _c.NewNode(_inputs, _outputs, c =>
                {
                    var s = new SkiaWidget();
                    var inputs = new IVLPin[]
                    {
                        c.Input(v => s.Layer = v, s.Layer),
                        c.Input(v => s.Size = v, s.Size),
                        c.Input(v => s.Space = v, s.Space)
                    };
                    return c.Node(inputs, new[] { c.Output(() => s) });
                });
            });
        }
    }
}
