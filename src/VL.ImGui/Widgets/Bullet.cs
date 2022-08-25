using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class Bullet : Widget
    {

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.Bullet();
        }
    }
}
