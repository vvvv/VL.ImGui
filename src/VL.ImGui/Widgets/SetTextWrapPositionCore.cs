namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal partial class SetTextWrapPositionCore : Widget
    {
        public Widget? Content { private get; set; }

        public float Position { private get; set; } = 0f;

        internal override void Update(Context context)
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
