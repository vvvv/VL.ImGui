using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Get maximum scrolling amount ~~ ContentSize - WindowSize - DecorationsSize
    /// </summary>

    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetScrollMax : Widget
    {

        public Vector2 Value { get; private set; }

        internal override void Update(Context context)
        {
            var x = ImGuiNET.ImGui.GetScrollMaxX();
            var y = ImGuiNET.ImGui.GetScrollMaxY();
            Value = new Vector2(x, y);
        }
    }
}
