using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Vector3)", Category = "ImGui.Widgets")]
    internal partial class InputVector3 : Widget
    {

        public string? Label { get; set; }

        /// <summary>
        /// Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "%.3f" -> 1.234; "%5.2f secs" -> 01.23 secs; "Biscuit: % .0f" -> Biscuit: 1; etc.
        /// </summary>
        public string? Format { private get; set; } = "%.3f";

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<Vector3> Value { get; } = new BehaviorSubject<Vector3>(Vector3.Zero);

        internal override void Update(Context context)
        {
            var value = Value.Value.ToImGui();
            if (ImGuiNET.ImGui.InputFloat3(Label ?? string.Empty, ref value, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value.ToVL());
        }
    }
}
