using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name ="Button (Small)", Category = "ImGui.Widgets", Button = true, Tags = "bang")]
    internal partial class ButtonSmall : ChannelWidget<Unit>
    {
        public string? Label { get; set; }

        internal override void UpdateCore(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.SmallButton(Label ?? string.Empty))
                Value = Unit.Default;
        }
    }
}
