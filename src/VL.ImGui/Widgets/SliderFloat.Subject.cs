using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using ImGuiNET;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    /// <summary>
    /// The user needs to provide the channel to operate on.
    /// </summary>
    [GenerateNode(Name = "Slider (Float Subject)")]
    internal partial class SliderFloatSubject : Widget
    {
        public BehaviorSubject<float>? Channel { private get; set; }

        public string? Label { private get; set; }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        public string? Format { private get; set; }

        public ImGuiSliderFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var value = Channel?.Value ?? default;
            if (ImGui.SliderFloat(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Channel?.OnNext(value);
        }
    }
}
