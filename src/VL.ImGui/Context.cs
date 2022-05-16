using System;
using System.Collections.Generic;
using System.Text;

namespace VL.ImGui
{
    using ImGui = ImGuiNET.ImGui;

    internal class Context : IDisposable
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

        public void Update(Widget widget)
        {
            if (widget is null)
                return;

            // Collect upstream widgets
            var stack = new Stack<Widget>();
            var w = widget;
            while (w != null)
            {
                stack.Push(w);
                w = w.Input;
            }

            var isGroup = stack.Count > 1;
            if (isGroup)
                ImGui.BeginGroup();

            try
            {
                while (stack.Count > 0)
                {
                    var c = stack.Pop();
                    c.Update(this);
                    _widgetsToReset.Add(c);
                }
            }
            finally
            {
                if (isGroup)
                    ImGui.EndGroup();
            }
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
