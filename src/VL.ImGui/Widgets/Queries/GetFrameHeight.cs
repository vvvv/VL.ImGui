namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Approx. FontSize + style.FramePadding.y * 2
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetFrameHeight : Widget
    {

        public float Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.GetFrameHeight();
        }
    }
}
