using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class TableNextColumn : Widget
    {

        public bool IsVisible { get; private set; }
        internal override void Update(Context context)
        {
            IsVisible = ImGuiNET.ImGui.TableNextColumn();
        }
    }
}
