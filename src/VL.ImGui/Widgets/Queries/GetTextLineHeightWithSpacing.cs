namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Distance in pixels between 2 consecutive lines of text. Approx. FontSize + style.ItemSpacing.y
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetTextLineHeightWithSpacing : Widget
    {

        public float Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.GetTextLineHeightWithSpacing();
        }
    }
}
