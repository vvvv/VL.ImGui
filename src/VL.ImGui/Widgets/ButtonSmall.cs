using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name ="Button (Small)", Category = "ImGui.Widgets", Button = true)]
    internal partial class ButtonSmall : ChannelWidget<Unit>
    {
        public string? Label { get; set; }

        internal override void Update(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.SmallButton(Label ?? string.Empty))
                Value = Unit.Default;
        }
    }
}
