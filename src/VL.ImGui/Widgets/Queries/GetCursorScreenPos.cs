using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Initial cursor position in window coordinates
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetCursorStartPos : Widget
    {
        public Vector2 Value { get; private set; }

        internal override void Update(Context context)
        {
            var pos = ImGuiNET.ImGui.GetCursorStartPos();
            Value = ImGuiConversion.ToVL(pos);
        }
    }
}
