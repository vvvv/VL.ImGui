using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Drag (Vector3)", Category = "ImGui.Widgets")]
    internal partial class DragVector3 : Widget
    {
        public string? Label { get; set; }

        public float Speed { private get; set; } = 0.01f;

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        /// <summary>
        /// Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "%.3f" -> 1.234; "%5.2f secs" -> 01.23 secs; "Biscuit: % .0f" -> Biscuit: 1; etc.
        /// </summary>
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<Vector3> Value { get; } = new BehaviorSubject<Vector3>(Vector3.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value.ToImGui();
            if (ImGuiNET.ImGui.DragFloat3(Label ?? string.Empty, ref value, Speed, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value.ToVL());
        }
    }
}
