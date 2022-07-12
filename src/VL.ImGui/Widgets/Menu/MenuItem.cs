namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class MenuItem : ChannelWidget<bool>
    {

        public string? Label { get; set; }

        public string? Shortcut { get; set; }

        public bool Enabled { get; set; } = true;

        internal override void Update(Context context)
        {
            var value = Update();
            if (ImGuiNET.ImGui.MenuItem(Label ?? string.Empty, Shortcut, ref value, Enabled))
                Value = value;
        }
    }
}
