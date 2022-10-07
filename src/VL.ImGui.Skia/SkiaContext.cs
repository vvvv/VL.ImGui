using Stride.Core.Mathematics;
using System.Runtime.CompilerServices;

namespace VL.ImGui
{
    using ImGui = ImGuiNET.ImGui;

    internal sealed class SkiaContext : Context
    {
        public readonly List<RenderCallback> WidgetFuncs = new List<RenderCallback>();

        public override void NewFrame()
        {
            WidgetFuncs.Clear();

            base.NewFrame();
        }

        internal void Widget(Vector2 size, RenderCallback widgetFunc)
        {
            var id = WidgetFuncs.Count;
            WidgetFuncs.Add(widgetFunc);
            ImGui.Image(new IntPtr(id), Unsafe.As<Vector2, System.Numerics.Vector2>(ref size));
        }
    }
}
