namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Create a MenuBar of current window.
    /// </summary>
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal partial class MenuBarCore : Widget
    {
        public Widget? Content { private get; set; }

        internal override void UpdateCore(Context context)
        {
            if (ImGuiNET.ImGui.BeginMenuBar())
            {
                try
                {
                    context?.Update(Content);
                }
                finally
                {
                    ImGuiNET.ImGui.EndMenuBar();
                }
            }
        }
    }
}
