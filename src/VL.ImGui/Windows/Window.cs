using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Stride.Core.Mathematics;
using VL.Core;

namespace VL.ImGui.Windows
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode]
    internal sealed partial class Window : Widget
    {
        public Widget? Content { get; set; }

        public string Name { get; set; } = "Window";

        public bool HasCloseButton { get; set; } = true;

        public bool Fullscreen { get; set; } = false;

        public RectangleF Bounds { get; set; } // TODO: will be done with setters below the window

        public bool SetBounds { get; set; } // TODO: will be done with setters below the window

        public IEnumerable<Widget> MenuBar { get; set; } = Enumerable.Empty<Widget>();

        public ImGuiWindowFlags WindowFlags { get; set; }

        public bool Closing { get; private set; }

        public bool IsVisible { get; private set; }

        internal override void Update(Context context)
        {

            var menuBarCount = MenuBar.Count(x => x != null);

            if (menuBarCount > 0)
            {
                WindowFlags |= ImGuiWindowFlags.MenuBar;
            }

            if (Fullscreen)
            {
                var viewPort = ImGui.GetMainViewport();
                ImGui.SetNextWindowPos(viewPort.WorkPos);
                ImGui.SetNextWindowSize(viewPort.WorkSize);
            } 
            else if (SetBounds)
            {
                ImGui.SetNextWindowPos(Bounds.TopLeft.ToImGui());
                ImGui.SetNextWindowSize(Bounds.Size.ToImGui());
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

                    // Add Menu Bar
                    if (menuBarCount > 0)
                    {
                        if (ImGui.BeginMenuBar())
                        {
                            try
                            {
                                foreach (var item in MenuBar)
                                {
                                    if (item is null)
                                        continue;
                                    else
                                        context.Update(item);
                                }
                            }
                            finally
                            {
                                ImGuiNET.ImGui.EndMenuBar();
                            }
                        }

                    }
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
    }
}
