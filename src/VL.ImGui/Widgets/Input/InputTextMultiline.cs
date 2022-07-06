using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (String Multiline)", Category = "ImGui.Widgets")]
    internal partial class InputTextMultiline : Widget
    {

        public string? Label { get; set; }

        public int MaxLength { get; set; } = 100;

        public Vector2 Size { get; set; }

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<string> Text { get; } = new BehaviorSubject<string>(String.Empty);

        internal override void Update(Context context)
        {
            var value = Text.Value;
            if (ImGuiNET.ImGui.InputTextMultiline(Label ?? string.Empty, ref value, (uint)MaxLength, Size.ToImGui(), Flags))
                Text.OnNext(value);
        }
    }
}
