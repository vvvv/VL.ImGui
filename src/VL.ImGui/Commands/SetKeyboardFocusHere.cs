namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    /// <summary>
    /// Focus keyboard on the next widget. Use positive 'offset' to access sub components of a multiple component widget. Use -1 to access previous widget.
    /// </summary>
    internal partial class SetKeyboardFocusHere : Widget
    {
        public int Offset { private get; set; }

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                ImGuiNET.ImGui.SetKeyboardFocusHere(Offset);
        }
    }
}
