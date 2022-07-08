namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", GenerateRetained = false)]
    internal partial class TableHeadersRow : Widget
    {
        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.TableHeadersRow();
        }
    }
}
