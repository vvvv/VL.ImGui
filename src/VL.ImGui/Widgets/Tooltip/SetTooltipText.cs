using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "SetTooltip (Text)", GenerateImmediate = false)]
    internal partial class SetTooltipText : Widget
    {
        public Widget? Input { private get; set; }

        public string? Text{ private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.SetTooltip(Text ?? string.Empty);
            context.Update(Input);
        }
    }
}
