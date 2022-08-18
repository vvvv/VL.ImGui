namespace VL.ImGui.Widgets
{
    //[GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    //TODO: This functionality is part of the window. Remove the Command?
    internal partial class SetNextWindowCollapsed : Widget
    {
        public bool Collapsed { private get; set; }

        internal override void Update(Context context)
        {
                ImGuiNET.ImGui.SetNextWindowCollapsed (Collapsed);
        }
    }
}
