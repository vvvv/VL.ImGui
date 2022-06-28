using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using VL.ImGui;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text (Label Value)")]
    internal partial class TextLabel : Widget
    {
        public string? Label { private get; set; }
        public string? Value { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.LabelText(Label ?? String.Empty, Value ?? String.Empty);
        }
    }
}
