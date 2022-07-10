using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Slider (Float Vertical)", Category = "ImGui.Widgets")]
    internal partial class SliderFloatVertical : Widget
    {
        public string? Label { get; set; }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        public Vector2 Size { get; set; } = new Vector2 (40, 100);

        /// <summary>
        /// Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "%.3f" -> 1.234; "%5.2f secs" -> 01.23 secs; "Biscuit: % .0f" -> Biscuit: 1; etc.
        /// </summary>
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public Channel<float>? Channel { private get; set; }
        ChannelFlange<float> channelFlange = new ChannelFlange<float>();

        internal override void Update(Context context)
        {
            var value = channelFlange.Update(Channel);
            if (ImGuiNET.ImGui.VSliderFloat(Label ?? string.Empty, Size.ToImGui(), ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                channelFlange.Value = value;
        }
    }
}
