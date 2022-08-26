namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text (Wrapped)", Category = "ImGui.Widgets")]
    internal partial class TextWrapped : Widget
    {
        public string? Text { private get; set; }

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.TextWrapped(Text ?? String.Empty);
        }
    }
}
