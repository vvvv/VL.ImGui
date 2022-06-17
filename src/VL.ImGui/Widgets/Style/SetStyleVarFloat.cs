using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "SetStyle (Float)")]
    internal partial class SetStyleVarFloat : Widget
    {
        public Widget? Input { private get; set; }

        public ImGuiNET.ImGuiStyleVar Key { private get; set; }

        public float Value { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PushStyleVar(Key, Value);
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.PopStyleVar();
            }
        }
    }
}
