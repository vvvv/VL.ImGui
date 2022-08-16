namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class LoadIniSettingsFromMemory : ChannelWidget<string>
    {

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
            {
                var value = Update();
                if (value != null)
                    ImGuiNET.ImGui.LoadIniSettingsFromMemory(value);
            }
        }
    }
}
