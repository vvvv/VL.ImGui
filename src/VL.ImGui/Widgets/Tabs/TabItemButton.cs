using System.Reactive;

namespace VL.ImGui.Widgets
{

    [GenerateNode(Category = "ImGui.Widgets", Button = true)]
    internal partial class TabItemButton : ChannelWidget<Unit>
    {
        public string? Label { get; set; }

        public ImGuiNET.ImGuiTabItemFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.TabItemButton(Label ?? string.Empty, Flags))
                Value = Unit.Default;
        }
    }
}
