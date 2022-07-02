﻿using VL.Core;

namespace VL.ImGui.Windows
{
    [GenerateNode(Category = "ImGui.Debug")]
    public sealed partial class StackToolWindow : Widget
    {
        public bool HasCloseButton { get; set; } = true;

        public bool Closing { get; private set; }

        internal override void Reset()
        {
            Closing = false;
        }

        internal override void Update(Context context)
        {
            if (HasCloseButton)
            {
                var open = true;
                ImGuiNET.ImGui.ShowStackToolWindow(ref open);
                Closing = !open;
            }
            else
            {
                ImGuiNET.ImGui.ShowStackToolWindow();
            }
        }
    }
}