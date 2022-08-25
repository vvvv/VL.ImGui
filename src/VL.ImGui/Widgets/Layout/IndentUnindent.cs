using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(GenerateImmediate = false)]
    internal partial class IndentUnindent : Widget
    {
        public Widget? Input { private get; set; }

        public float Value { private get; set; }

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.Indent(Value);
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.Unindent(Value);
            }
        }
    }
}
