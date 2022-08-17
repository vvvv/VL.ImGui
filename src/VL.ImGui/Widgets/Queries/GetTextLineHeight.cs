namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Approx. = FontSize
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetTextLineHeight : Widget
    {

        public float Value { get; private set; }


        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.GetTextLineHeight();
        }
    }
}
