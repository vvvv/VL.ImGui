namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands")]
    internal partial class SetNextWindowBgAlpha : Widget
    {
        public float Alpha { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowBgAlpha(Alpha);
        }
    }
}
