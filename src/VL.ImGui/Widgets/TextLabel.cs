namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text (Label Value)", Category = "ImGui.Widgets")]
    internal partial class TextLabel : Widget
    {
        public string? Label { private get; set; }
        public string? Value { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.LabelText(Label ?? String.Empty, Value ?? String.Empty);
        }
    }
}
