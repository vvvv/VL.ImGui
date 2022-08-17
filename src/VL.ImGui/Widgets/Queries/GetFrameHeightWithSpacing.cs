namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Distance in pixels between 2 consecutive lines of framed widgets.
    /// Approx. FontSize + style.FramePadding.y * 2 + style.ItemSpacing.y
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetFrameHeightWithSpacing : Widget
    {

        public float Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.GetFrameHeightWithSpacing();
        }
    }
}
