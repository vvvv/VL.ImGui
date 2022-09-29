namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Push width of items for common large "item+label" widgets. >0.0f: width in pixels, <0.0f align xx pixels to the right of window (so -FLT_MIN always align width to the right side).
    /// </summary>
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false, IsStylable = false)]
    internal partial class SetItemWidthCore : Widget
    {
        public Widget? Content { private get; set; }

        public float Width { private get; set; } = 0.1f;

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.PushItemWidth(Width);
            try
            {
                context?.Update(Content);
            }
            finally
            {
                ImGuiNET.ImGui.PopItemWidth();
            }
        }
    }
}
