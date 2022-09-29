namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Push word-wrapping position for Text commands. < 0.0f: no wrapping; 0.0f: wrap to end of window (or column); > 0.0f: wrap at 'wrap_pos_x' position in window local space
    /// </summary>
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false, IsStylable = false)]
    internal partial class SetTextWrapPositionCore : Widget
    {
        public Widget? Content { private get; set; }

        public float Position { private get; set; } = 0.1f;

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.PushTextWrapPos(Position);
            try
            {
                context?.Update(Content);
            }
            finally
            {
                ImGuiNET.ImGui.PopTextWrapPos();
            }
        }
    }
}
