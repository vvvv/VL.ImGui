namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Separator, generally horizontal. Inside a menu bar or in horizontal layout mode, this becomes a vertical separator.
    /// </summary>
    [GenerateNode(Name = "Separator", Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SeparatorImmediate : Widget
    {

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.Separator();
        }
    }
}
