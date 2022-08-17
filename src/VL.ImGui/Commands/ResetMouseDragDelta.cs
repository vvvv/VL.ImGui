namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class ResetMouseDragDelta : Widget
    {

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                ImGuiNET.ImGui.ResetMouseDragDelta();
        }
    }
}
