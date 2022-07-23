using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands")]
    internal partial class SetNextWindowPosition : Widget
    {
        public Vector2 Position { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowPos(Position.ToImGui());
        }
    }
}
