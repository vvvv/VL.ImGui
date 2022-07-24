namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Add vertical spacing.
    /// </summary>
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class Spacing : Widget
    {

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.Spacing();
        }
    }
}
