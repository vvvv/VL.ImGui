namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetNextWindowFocus : Widget
    {
        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowFocus();
        }
    }
}
