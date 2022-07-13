namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class RadioButtons : ChannelWidget<int>
    {
        public IEnumerable<string> Labels { get; set; } = new List<string>();

        internal override void Update(Context context)
        {
            var value = Update();
            int i = 0;
            foreach (var label in Labels)
            {
                if (ImGuiNET.ImGui.RadioButton(label ?? string.Empty, ref value, i++))
                    Value = value;
            }
        }
    }
}
