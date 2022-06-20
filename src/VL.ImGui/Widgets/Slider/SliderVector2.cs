using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Slider (Vector2)")]
    internal partial class SliderVector2 : Widget
    {
        public string? Label { get; set; }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        [Documentation(@"Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "" % .3f"" -> 1.234; "" % 5.2f secs"" -> 01.23 secs; ""Biscuit: % .0f"" -> Biscuit: 1; etc.")]
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<Vector2> Value { get; } = new BehaviorSubject<Vector2>(Vector2.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value.ToImGui();
            if (ImGuiNET.ImGui.SliderFloat2(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value.ToVL());
        }
    }
}
