using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    //[GenerateNode(GenerateImmediate = false)]

    internal partial class SetTextWrapPosition : Widget
    {
        public Widget? Input { private get; set; }

        public float Position { private get; set; } = 0f;

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PushTextWrapPos(Position);
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.PopTextWrapPos();
            }
        }
    }
}
