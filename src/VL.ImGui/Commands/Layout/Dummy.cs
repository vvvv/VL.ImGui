using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Add a dummy item of given size. Unlike InvisibleButton, Dummy won't take the mouse click or be navigable into.
    /// </summary>
    [GenerateNode(Name = "Dummy", Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class DummyImmediate : Widget
    {
        public Vector2 Size { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.Dummy(Size.ToImGui());
        }
    }
}
