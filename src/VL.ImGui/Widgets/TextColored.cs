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
    [GenerateNode(Name = "Text (Colored)")]
    internal partial class TextColored : Widget
    {
        public Color4 Color { private get; set; }
        
        public string? Text { private get; set; }

        internal override void Update(Context context)
        {
            var color = ImGuiConversion.FromColor4 (Color);
            ImGuiNET.ImGui.TextColored(color, Text ?? String.Empty);
        }
    }
}
