using Stride.Core.Mathematics;
using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Button = true)]
    internal partial class Button : StylableChannelWidget<Unit>
    {
        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        internal override void UpdateCore(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.Button(Label ?? string.Empty, Size.ToImGui()))
                Value = Unit.Default;
        }
    }
}
