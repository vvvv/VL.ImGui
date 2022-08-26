using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    //[GenerateNode(GenerateImmediate = false)]
    internal partial class SetWidth : Widget
    {
        public Widget? Input { private get; set; }

        public float Width { private get; set; } = 100f;

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.PushItemWidth(Width);
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.PopItemWidth();
            }
        }
    }
}
