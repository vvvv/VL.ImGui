using SkiaSharp;
using Stride.Core.Mathematics;
using VL.Skia;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets.Internal", IsStylable = false)]
    public sealed partial class SkiaWidget : Widget
    {
        public ILayer? Layer { private get; set; }

        public Vector2 Size { private get; set; } = new Vector2(1f, 1f);

        internal override void UpdateCore(Context context)
        {
            if (context is SkiaContext skiaContext)
            {
                var _ = Size.FromHectoToImGui();
                skiaContext.Widget(new Vector2(_.X, _.Y), Render);
            }
        }

        void Render(CallerInfo caller)
        {
            //caller.Canvas.ClipRect(, SKClipOperation.) //would love to clear the clip
            try
            {
                Layer?.Render(caller);

            }
            finally
            {

            }
        }
    }
}
