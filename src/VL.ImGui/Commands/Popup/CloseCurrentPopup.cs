namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    /// <summary>
    /// Use inside the Popup Region to close it manually.
    /// </summary>
    internal partial class CloseCurrentPopup : Widget
    {
        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
               ImGuiNET.ImGui.CloseCurrentPopup();
        }
    }
}
