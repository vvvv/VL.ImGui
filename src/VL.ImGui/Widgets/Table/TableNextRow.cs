using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class TableNextRow : Widget
    {

        public float MinHeight { private get; set; } = 0f;

        public ImGuiNET.ImGuiTableRowFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.TableNextRow(Flags, MinHeight);
        }
    }
}
