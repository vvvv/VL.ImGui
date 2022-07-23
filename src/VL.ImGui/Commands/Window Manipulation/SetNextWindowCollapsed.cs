namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetNextWindowCollapsed : Widget
    {
        public bool Collapsed { private get; set; } = false;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowCollapsed(Collapsed);
        }
    }
}
