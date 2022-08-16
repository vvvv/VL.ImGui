namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    /// <summary>
    /// Retrieve text data from the clipboard
    /// </summary>
    internal partial class GetClipboardText : ChannelWidget<string>
    {

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                Value = ImGuiNET.ImGui.GetClipboardText();
        }
    }
}
