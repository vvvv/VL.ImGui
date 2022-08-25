namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", Name = "TableSetupColumn", GenerateRetained = false)]
    internal partial class TableSetupColumnImmediate : Widget
    {

        public string? Label { get; set; }

        public ImGuiNET.ImGuiTableColumnFlags Flags { private get; set; }

        public float InitWidth { get; set; }

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.TableSetupColumn(Label ?? String.Empty, Flags, InitWidth);
        }
    }
}