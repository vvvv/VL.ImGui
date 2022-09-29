namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetNextWindowCollapsed : Widget
    {
        public bool Collapsed { private get; set; }

        internal override void UpdateCore(Context context)
        {
                ImGuiNET.ImGui.SetNextWindowCollapsed (Collapsed);
        }
    }
}
