using VL.Core.EditorAttributes;
using VL.Lib.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Float Range)", Category = "ImGui.Widgets", Tags = "number")]
    [WidgetType(WidgetType.Drag)]
    internal partial class DragFloatRange2 : ChannelWidget<Range<float>>
    {
        public string? Label { get; set; }

        public float Speed { private get; set; } = 0.01f;

        public float Min { private get; set; }

        public float Max { private get; set; }

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update();

            value.Split(out float from, out float to);
            if (ImGuiNET.ImGui.DragFloatRange2(Context.GetLabel(this, Label), ref from, ref to, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value = new Range<float>(from, to);
        }
    }
}
