using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode]

    internal partial class SetPosition : Widget
    {
        public Widget? Input { private get; set; }

        public Vector2 Position { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetCursorPos(Position.ToImGui());
            try
            {
                context.Update(Input);
            }
            finally
            {
                // TODO: Finally?
            }
        }
    }
}
