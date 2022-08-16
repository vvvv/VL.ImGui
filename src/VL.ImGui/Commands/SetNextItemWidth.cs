namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    /// <summary>
    /// Set width of the next common large "item+label" widget. >0.0: width in pixels, <0.0 align xx pixels to the right of window.
    /// </summary>
    internal partial class SetNextItemWidth : Widget
    {
        public float Width { private get; set; }

        internal override void Update(Context context)
        {
                ImGuiNET.ImGui.SetNextItemWidth(Width);
        }
    }
}
