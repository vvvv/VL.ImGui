namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SaveIniSettingsToMemory : ChannelWidget<string>
    {

        public bool Enabled { private get; set; } = false;

        internal override void Update(Context context)
        {
            if (Enabled)
                Value = ImGuiNET.ImGui.SaveIniSettingsToMemory();
        }
    }
}
