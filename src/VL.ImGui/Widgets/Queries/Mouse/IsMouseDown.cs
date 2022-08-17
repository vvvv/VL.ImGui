namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Is mouse button held?
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class IsMouseDown : Widget
    {

        public ImGuiNET.ImGuiMouseButton Flags { private get; set; }

        public bool Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.IsMouseDown(Flags);
        }
    }
}
