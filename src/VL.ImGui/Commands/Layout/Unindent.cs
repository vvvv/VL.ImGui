namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Move content position back to the Left, by Value, or style.IndentSpacing if Value <= 0.
    /// </summary>
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class Unindent : Widget
    {

        public float Value { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.Unindent(Value);
        }
    }
}
