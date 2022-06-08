using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class SameLine : Widget
    {

        public float Offset { private get; set; } = 0f;
        public float Spacing { private get; set; } = -1f;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SameLine(Offset, Spacing);
        }
    }
}
