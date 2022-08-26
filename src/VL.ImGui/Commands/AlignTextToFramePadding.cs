namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands")]
    internal partial class AlignTextToFramePadding : Widget
    {

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.AlignTextToFramePadding();
        }
    }
}
