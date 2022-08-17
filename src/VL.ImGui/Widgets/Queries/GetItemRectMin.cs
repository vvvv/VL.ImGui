using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Get upper-left bounding rectangle of the last item (screen space)
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetItemRectMin : Widget
    {

        public Vector2 Value { get; private set; }

        internal override void Update(Context context)
        {
            var size = ImGuiNET.ImGui.GetItemRectMin();
            Value = ImGuiConversion.ToVL(size);
        }
    }
}
