namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Return primary/default viewport.
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetMainViewport : Widget
    {
        public ImGuiNET.ImGuiViewportPtr Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.GetMainViewport();
        }
    }
}
