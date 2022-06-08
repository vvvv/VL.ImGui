using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode]

    internal partial class PushStyleColor : Widget
    {

        public ImGuiNET.ImGuiCol Type { private get; set; }
        public Color4 Color { private get; set; } = new Color4(Stride.Core.Mathematics.Color.White);

        internal override void Update(Context context)
        {
            var color = ImGuiConversion.FromColor4(Color);
            ImGuiNET.ImGui.PushStyleColor(Type, color);
        }
    }
}
