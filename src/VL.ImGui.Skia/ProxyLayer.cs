using VL.Skia;

namespace VL.ImGui.Skia
{
    public class ProxyLayer : LinkedLayerBase
    {
        public new ILayer Layer { set { base.Input = value; } }
    }
}
