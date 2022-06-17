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
    internal partial class Dummy : Widget
    {
        public Vector2 Size { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.Dummy(Size.ToImGui());
        }
    }
}
