namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Create a MenuBar at the top of the screen.
    /// </summary>
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal partial class MainMenuBarCore : Widget
    {
        public Widget? Content { private get; set; }

        internal override void UpdateCore(Context context)
        {
            if (ImGuiNET.ImGui.BeginMainMenuBar())
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
