using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class PopID : Widget
    {

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PopID();
        }
    }
}
