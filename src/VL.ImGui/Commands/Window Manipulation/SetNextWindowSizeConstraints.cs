using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands", GenerateRetained = false)]
    internal partial class SetNextWindowSizeConstraints : Widget
    {
        public Vector2 Min { private get; set; }

        public Vector2 Max { private get; set; }

        public bool Enabled { private get; set; } = true;

        internal override void Update(Context context)
        {
            if (Enabled)
                ImGuiNET.ImGui.SetNextWindowSizeConstraints(Min.ToImGui(), Max.ToImGui());
        }
    }
}
