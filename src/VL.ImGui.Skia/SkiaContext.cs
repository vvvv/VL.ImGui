using Stride.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VL.ImGui.Skia
{
    using ImGui = ImGuiNET.ImGui;

    internal sealed class SkiaContext : Context
    {
        public readonly List<WidgetFunc> WidgetFuncs = new List<WidgetFunc>();

        public override void NewFrame()
        {
            WidgetFuncs.Clear();

            base.NewFrame();
        }

        internal void Widget(Vector2 size, WidgetFunc widgetFunc)
        {
            var id = WidgetFuncs.Count;
            WidgetFuncs.Add(widgetFunc);
            ImGui.Image(new IntPtr(id), Unsafe.As<Vector2, System.Numerics.Vector2>(ref size));
        }
    }
}
