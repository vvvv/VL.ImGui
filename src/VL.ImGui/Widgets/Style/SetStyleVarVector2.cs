using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "SetStyle (Vector2)")]
    internal partial class SetStyleVarVector : Widget
    {
        public Widget? Input { private get; set; }

        public ImGuiNET.ImGuiStyleVar Key { private get; set; }

        public Vector2 Value { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PushStyleVar(Key, Value.ToImGui());
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
