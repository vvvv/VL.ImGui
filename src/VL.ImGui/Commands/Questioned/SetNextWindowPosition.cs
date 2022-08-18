using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    //[GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    //TODO: This functionality is part of the window. Remove the Command?
    internal partial class SetNextWindowPosition : Widget
    {
        public Vector2 Position { private get; set; }

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                ImGuiNET.ImGui.SetNextWindowPos(Position.ToImGui());
        }
    }
}
