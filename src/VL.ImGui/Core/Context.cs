using System;
using System.Collections.Generic;
using System.Text;

namespace VL.ImGui
{
    using ImGui = ImGuiNET.ImGui;

    public static class ContextHelpers
    {        
        public static Context? Validate(this Context? c) => c ?? Context.Current;
    }


    public class Context : IDisposable
    {
        private readonly IntPtr _context;
        private readonly List<Widget> _widgetsToReset = new List<Widget>();

        [ThreadStatic]
        internal static Context? Current = null;

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
            return new Frame(_context, this);
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
            readonly Context? previous2;

            public Frame(IntPtr context, Context c)
            {
                previous = ImGui.GetCurrentContext();
                ImGui.SetCurrentContext(context);
                previous2 = Current;
                Current = c;
            }

            public void Dispose()
            {
                Current = previous2;
                ImGui.SetCurrentContext(previous);
            }
        }
    }
}
