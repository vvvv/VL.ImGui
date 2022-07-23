using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands")]
    internal partial class SetNextWindowSizeConstraints : Widget
    {
        public Vector2 Min { private get; set; }

        public Vector2 Max { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowSizeConstraints(Min.ToImGui(), Max.ToImGui());
        }
    }
}
