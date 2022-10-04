using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Is mouse hovering given bounding rect (in screen space). clipped by current clipping settings, but disregarding of other consideration of focus/window ordering/popup-block.
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class IsMouseHoveringRect : Widget
    {

        public Vector2 Min { private get; set; }
        public Vector2 Max { private get; set; }
        public bool Clip { private get; set; }

        public bool Value { get; private set; }

        internal override void UpdateCore(Context context)
        {
            Value = ImGuiNET.ImGui.IsMouseHoveringRect(Min.FromHectoToImGui(), Max.FromHectoToImGui(), Clip);
        }
    }
}
