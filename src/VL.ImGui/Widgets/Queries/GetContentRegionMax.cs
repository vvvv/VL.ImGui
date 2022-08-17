using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Current content boundaries (typically window boundaries including scrolling, or current column boundaries), in windows coordinates
    /// </summary>

    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetContentRegionMax : Widget
    {

        public Vector2 Value { get; private set; }

        internal override void Update(Context context)
        {
            var size = ImGuiNET.ImGui.GetContentRegionMax();
            Value = ImGuiConversion.ToVL(size);
        }
    }
}
