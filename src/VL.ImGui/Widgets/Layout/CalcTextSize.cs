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
    internal partial class CalcTextSize : Widget
    {

        public Vector2 Value { get; private set; }

        public string? Text { private get; set; }


        internal override void Update(Context context)
        {
            var width = ImGuiNET.ImGui.CalcTextSize(Text ?? string.Empty);
            Value = ImGuiConversion.ToVector2(width);
        }
    }
}
