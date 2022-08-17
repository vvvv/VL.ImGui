namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Return '' if column didn't have a name declared by TableSetupColumn().
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class TableGetColumnName : Widget
    {
        /// <summary>
        /// Pass -1 to use current column.
        /// </summary>
        public int Index { private get; set; } = -1;

        public string? Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.TableGetColumnName(Index);
        }
    }
}
