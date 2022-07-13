namespace VL.ImGui.Widgets
{
    //[GenerateNode(Category = "ImGui.Widgets")]
    internal partial class RadioButton : ChannelWidget<int>
    {
        public string? Label { get; set; }

        public int Index { get; set; }

        internal override void Update(Context context)
        {
            var value = Update();
            if (ImGuiNET.ImGui.RadioButton(Label ?? string.Empty, ref value, Index))
                Value = value;
        }
    }
}
