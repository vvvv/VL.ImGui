using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Retrieve available space from a given point. == GetContentRegionMax() - GetCursorPos()
    /// </summary>

    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetContentRegionAvail : Widget
    {

        public Vector2 Value { get; private set; }

        internal override void Update(Context context)
        {
            var size = ImGuiNET.ImGui.GetContentRegionAvail();
            Value = ImGuiConversion.ToVL(size);
        }
    }
}
