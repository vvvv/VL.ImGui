using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class SetAlignTextToFramePadding : Widget
    {
        public Widget? Input { private get; set; }

        public float Position { private get; set; } = 0f;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.AlignTextToFramePadding();
            context.Update(Input);
        }
    }
}
