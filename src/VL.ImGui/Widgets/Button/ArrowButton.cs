using Stride.Core.Mathematics;
using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Button = true)]
    internal partial class ArrowButton : ChannelWidget<Unit>
    {
        public string? Label { private get; set; }

        public ImGuiNET.ImGuiDir Direction { private get; set; }

        internal override void UpdateCore(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.ArrowButton(Label ?? string.Empty, Direction))
                Value = Unit.Default;
        }
    }
}
