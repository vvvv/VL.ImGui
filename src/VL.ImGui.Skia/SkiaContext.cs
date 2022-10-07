using ImGuiNET;
using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;
using VL.Skia;

namespace VL.ImGui
{
    using ImGui = ImGuiNET.ImGui;

    internal sealed class SkiaContext : Context
    {
        public readonly List<ILayer> Layers = new List<ILayer>();

        public override void NewFrame()
        {
            Layers.Clear();

            base.NewFrame();
        }

        internal void AddLayer(Vector2 size, ILayer widgetFunc, in ImDrawListPtr drawList)
        {
            var id = Layers.Count;
            Layers.Add(widgetFunc);
            ImGui.Image(new IntPtr(id), Unsafe.As<Vector2, System.Numerics.Vector2>(ref size));
            drawList.AddImage(new IntPtr(id), default, Unsafe.As<Vector2, System.Numerics.Vector2>(ref size));
        }
    }
}
