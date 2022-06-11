using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "PushStyleVar (Vector)")]
    internal partial class PushStyleVarVector : Widget
    {

        public ImGuiNET.ImGuiStyleVar Key { private get; set; }

        public Vector2 Value { private get; set; }

        internal override void Update(Context context)
        {
            var val = ImGuiConversion.FromVector2(Value);
            ImGuiNET.ImGui.PushStyleVar(Key, val);
        }
    }
}
