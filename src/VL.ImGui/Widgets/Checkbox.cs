namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class Checkbox : ChannelWidget<bool>
    {

        public string? Label { get; set; }

        internal override void Update(Context context)
        {
            var value = Update();
            if (ImGuiNET.ImGui.Checkbox(Label ?? string.Empty, ref value))
                Value = value;
        }
    }
}
