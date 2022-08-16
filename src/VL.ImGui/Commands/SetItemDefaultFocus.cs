namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    /// <summary>
    /// Make last item the default focused item of a window.
    /// </summary>
    internal partial class SetItemDefaultFocus : Widget
    {

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                ImGuiNET.ImGui.SetItemDefaultFocus();
        }
    }
}
