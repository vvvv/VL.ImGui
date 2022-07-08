using System;
using System.Collections.Generic;
using System.Text;

namespace VL.ImGui
{
    using ImGui = ImGuiNET.ImGui;

    public class Context : IDisposable
    {
        private readonly IntPtr _context;
        private readonly List<Widget> _widgetsToReset = new List<Widget>();

        public Context()
        {
            _context = ImGui.CreateContext();
        }

        public virtual void NewFrame()
        {
            try
            {
                foreach (var widget in _widgetsToReset)
                    widget.Reset();
            }
            finally
            {
                _widgetsToReset.Clear();
                ImGui.NewFrame();
            }
        }

        public Frame MakeCurrent()
        {
            return new Frame(_context);
        }

        public void Update(Widget? widget)
        {
            if (widget is null)
                return;

            widget.Update(this);
            _widgetsToReset.Add(widget);
        }

        public void Dispose()
        {
            ImGui.DestroyContext(_context);
        }

        public readonly struct Frame : IDisposable
        {
            readonly IntPtr previous;

            public Frame(IntPtr context)
            {
                previous = ImGui.GetCurrentContext();
                ImGui.SetCurrentContext(context);
            }

            public void Dispose()
            {
                ImGui.SetCurrentContext(previous);
            }
        }
    }
}
