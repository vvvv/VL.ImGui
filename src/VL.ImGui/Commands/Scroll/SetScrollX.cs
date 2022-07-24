namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Set scrolling amount
    /// </summary>
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetScrollX : Widget
    {
        public float Value { private get; set; }

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                ImGuiNET.ImGui.SetScrollX(Value);
        }
    }
}
