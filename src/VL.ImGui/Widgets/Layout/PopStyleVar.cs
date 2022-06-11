using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class PopStyleVar : Widget
    {
        public int Count { private get; set; } = 1;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PopStyleVar(Count);
        }
    }
}
