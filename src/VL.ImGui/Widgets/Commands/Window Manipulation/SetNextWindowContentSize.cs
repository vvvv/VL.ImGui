using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Commands")]
    internal partial class SetNextWindowContentSize : Widget
    {
        public Vector2 Size {private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetNextWindowContentSize (Size.ToImGui());
        }
    }
}
