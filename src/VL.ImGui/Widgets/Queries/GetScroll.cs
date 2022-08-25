using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Get scrolling amount [0 .. GetScrollMax]
    /// </summary>

    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetScroll : Widget
    {

        public Vector2 Value { get; private set; }

        internal override void UpdateCore(Context context)
        {
            var x = ImGuiNET.ImGui.GetScrollX();
            var y = ImGuiNET.ImGui.GetScrollY();
            Value = new Vector2(x, y);
        }
    }
}
