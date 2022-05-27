using VL.Core;

namespace VL.ImGui.Windows
{
    [GenerateNode]
    public sealed partial class DemoWindow : Widget
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
                ImGuiNET.ImGui.ShowDemoWindow(ref open);
                Closing = !open;
            }
            else
            {
                ImGuiNET.ImGui.ShowDemoWindow();
            }
        }
    }
}
