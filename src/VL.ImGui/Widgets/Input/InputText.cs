using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "InputText")]
    internal partial class InputText : Widget
    {

        public string? Label { get; set; }

        public int MaxLength { get; set; } = 100;

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        public BehaviorSubject<string> Text { get; } = new BehaviorSubject<string>(String.Empty);

        internal override void Update(Context context)
        {
            var value = Text.Value;
            if (ImGuiNET.ImGui.InputText(Label ?? string.Empty, ref value, (uint)MaxLength, Flags))
                Text.OnNext(value);
        }
    }
}
