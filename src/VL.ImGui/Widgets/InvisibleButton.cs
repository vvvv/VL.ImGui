using Stride.Core.Mathematics;
using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Button = true)]
    internal partial class InvisibleButton : ChannelWidget<Unit>
    {
        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public ImGuiNET.ImGuiButtonFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.InvisibleButton(Label ?? string.Empty, Size.ToImGui(), Flags))
                Value = Unit.Default;
        }
    }
}
