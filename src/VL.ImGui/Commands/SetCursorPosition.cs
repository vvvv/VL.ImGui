using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetCursorPosition : Widget
    {
        public Vector2 Position { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetCursorPos(Position.ToImGui());
        }
    }
}
