using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]

    internal partial class PushItemWidth : Widget
    {
        public float Width { private get; set; } = 0.2f;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PushItemWidth (Width);
        }
    }
}
