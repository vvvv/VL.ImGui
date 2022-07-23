namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Set scrolling amount
    /// </summary>
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetScrollX : Widget
    {
        public float Value { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetScrollX(Value);
        }
    }
}
