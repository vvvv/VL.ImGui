namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (String)", Category = "ImGui.Widgets", Tags = "edit")]
    internal partial class InputText : ChannelWidget<string>
    {

        public string? Label { get; set; }

        public int MaxLength { get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update() ?? string.Empty;
            if (ImGuiNET.ImGui.InputText(Label ?? string.Empty, ref value, (uint)MaxLength, Flags))
                Value = value;
        }
    }
}
