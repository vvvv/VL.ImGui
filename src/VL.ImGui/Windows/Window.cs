using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using VL.Core;

namespace VL.ImGui.Windows
{
    using ImGui = ImGuiNET.ImGui;

    internal sealed class Window : Widget
    {
        public Widget Content { get; set; }

        public string Name { get; set; } = "Window";

        public bool HasCloseButton { get; set; } = true;

        public bool Fullscreen { get; set; } = false;

        public ImGuiWindowFlags WindowFlags { get; set; }

        public bool Closing { get; private set; }

        public bool IsVisible { get; private set; }

        internal override void Update(Context context)
        {
            if (Fullscreen)
            {
                var viewPort = ImGui.GetMainViewport();
                ImGui.SetNextWindowPos(viewPort.WorkPos);
                ImGui.SetNextWindowSize(viewPort.WorkSize);
            }

            if (HasCloseButton)
            {
                var open = true;
                IsVisible = ImGuiNET.ImGui.Begin(Name, ref open, WindowFlags);
                Closing = !open;
            }
            else
            {
                IsVisible = ImGuiNET.ImGui.Begin(Name, WindowFlags);
            }

            try
            {
                if (IsVisible)
                {
                    context.Update(Content);
                }
            }
            finally
            {
                ImGuiNET.ImGui.End();
            }
        }

        internal override void Reset()
        {
            Closing = false;
            IsVisible = false;
        }

        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {
            return factory.NewNodeDescription(nameof(Window), "ImGui", fragmented: true, _c =>
            {
                var _w = new Window();
                var _inputs = new[]
                {
                    _c.Input("Content", _w.Content),
                    _c.Input("Name", _w.Name),
                    _c.Input("Has Close Button", _w.HasCloseButton),
                    _c.Input("Fullscreen", _w.Fullscreen),
                    _c.Input("Window Flags", _w.WindowFlags),
                };
                var _outputs = new[]
                {
                    _c.Output<Widget>("Output"),
                    _c.Output("Closing", _w.Closing),
                    _c.Output("Is Visible", _w.IsVisible)
                };
                return _c.NewNode(_inputs, _outputs, c =>
                {
                    var s = new Window();
                    var inputs = new IVLPin[]
                    {
                        c.Input(v => s.Content = v, s.Content),
                        c.Input(v => s.Name = v, s.Name),
                        c.Input(v => s.HasCloseButton = v, s.HasCloseButton),
                        c.Input(v => s.Fullscreen = v, s.Fullscreen),
                        c.Input(v => s.WindowFlags = v, s.WindowFlags)
                    };
                    var outputs = new IVLPin[]
                    {
                        c.Output(() => s),
                        c.Output(() => s.Closing),
                        c.Output(() => s.IsVisible)
                    };
                    return c.Node(inputs, outputs);
                });
            });
        }
    }
}
