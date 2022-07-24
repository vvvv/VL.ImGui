namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Undo a SameLine or force a new line when in an horizontal-layout context.
    /// </summary>
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class NewLine : Widget
    {

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.NewLine();
        }
    }
}
