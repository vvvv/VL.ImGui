namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal partial class TooltipCore : Widget
    {
        public Widget? Content { private get; set; }

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.BeginTooltip();
            try
            {
                context.Update(Content);
            }
            finally
            {
                ImGuiNET.ImGui.EndTooltip();
            }
        }
    }
}
