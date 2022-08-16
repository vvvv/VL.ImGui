namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class LoadIniSettingsFromDisk : Widget
    {

        public string? Filename { private get; set; }

        public bool Enabled { private get; set; } = false;

        internal override void Update(Context context)
        {
            if (Enabled && Filename != null)
                ImGuiNET.ImGui.LoadIniSettingsFromDisk(Filename);
        }
    }
}
