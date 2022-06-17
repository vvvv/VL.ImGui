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

        public RectangleF Bounds { get; set; }

        public bool SetBounds { get; set; }

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
