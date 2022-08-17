namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Is the last item visible? (items may be out of sight because of clipping/scrolling)
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class IsItemVisible : Widget
    {

        public bool Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.IsItemVisible();
        }
    }
}
