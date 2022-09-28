namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Tags = "toggle")]
    internal partial class Checkbox : ChannelWidget<bool>
    {

        public string? Label { get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update();
            if (ImGuiNET.ImGui.Checkbox(Label ?? string.Empty, ref value))
                Value = value;
        }
    }
}
