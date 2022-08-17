namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Was the last item just made active (item was previously inactive).
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class IsItemActivated : Widget
    {

        public bool Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.IsItemActivated();
        }
    }
}
