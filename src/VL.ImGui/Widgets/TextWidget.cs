namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text", Category = "ImGui.Widgets")]
    internal partial class TextWidget : Widget
    {
        public string? Text { private get; set; }

        public bool Disabled { private get; set; } = false;

        internal override void UpdateCore(Context context)
        {
            if (!Disabled)
            {
                ImGuiNET.ImGui.Text(Text ?? String.Empty);
            }
            else
            {
                ImGuiNET.ImGui.TextDisabled(Text ?? String.Empty);
            }
        }
    }
}
