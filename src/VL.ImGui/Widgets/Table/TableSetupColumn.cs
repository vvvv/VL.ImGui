using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class TableSetupColumn : Widget
    {

        public string? Label { get; set; }

        public ImGuiNET.ImGuiTableColumnFlags Flags { private get; set; }

        public float InitWidth { get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.TableSetupColumn(Label ?? String.Empty, Flags, InitWidth);
        }
    }
}
