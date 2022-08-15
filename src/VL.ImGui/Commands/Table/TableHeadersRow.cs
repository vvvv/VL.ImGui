namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    /// <summary>
    /// Submit all headers cells based on data provided to TableSetupColumn() + submit context menu
    /// </summary>
    internal partial class TableHeadersRow : Widget
    {

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.TableHeadersRow();
        }
    }
}