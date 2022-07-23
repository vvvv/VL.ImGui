namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands")]
    internal partial class SetNextWindowCollapsed : Widget
    {
        public bool Collapsed { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowCollapsed(Collapsed);
        }
    }
}
