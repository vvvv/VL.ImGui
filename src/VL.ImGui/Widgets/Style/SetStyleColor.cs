using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "SetStyle (Color)")]
    internal partial class SetStyleColor : Widget
    {
        public Widget? Input { private get; set; }

        public ImGuiNET.ImGuiCol Type { private get; set; }
        public Color4 Color { private get; set; } = new Color4 (Stride.Core.Mathematics.Color.White);

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PushStyleColor(Type, Color.ToImGui());
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.PopStyleColor();
            }
        }
    }
}
