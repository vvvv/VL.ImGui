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

    internal partial class PopStyleColor : Widget
    {
        public int Count { private get; set; } = 1;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PopStyleColor(Count);
        }
    }
}
