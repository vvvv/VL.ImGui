namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Adjust scrolling amount to make current cursor position visible.
    /// </summary>
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetScrollHereY : Widget
    {

        /// <summary>
        /// 0.0 - Top, 0.5 - Center, 1.0 - Bottom.
        /// </summary>
        public float Ratio { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetScrollHereY(Ratio);
        }
    }
}
