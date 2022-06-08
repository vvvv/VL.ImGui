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

    internal partial class SetNextWindowPos : Widget
    {
        public Vector2 Position { private get; set; }

        internal override void Update(Context context)
        {
            var pos = ImGuiConversion.FromVector2(Position);
            ImGuiNET.ImGui.SetNextWindowPos (pos);
        }
    }
}
