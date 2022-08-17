namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Return current column index.
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class TableGetColumnIndex : Widget
    {
        public int Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.TableGetColumnIndex();
        }
    }
}
