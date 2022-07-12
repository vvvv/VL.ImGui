﻿using VL.Lib.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Float Range)", Category = "ImGui.Widgets")]
    internal partial class DragFloatRange2 : ChannelWidget<Range<float>>
    {
        public string? Label { get; set; }

        public float Speed { private get; set; } = 0.01f;

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var value = Update();

            value.Split(out float from, out float to);
            if (ImGuiNET.ImGui.DragFloatRange2(Label ?? string.Empty, ref from, ref to, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value = new Range<float>(from, to);
        }
    }
}
