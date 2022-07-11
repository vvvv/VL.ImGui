using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(GenerateImmediate = false)]
    internal partial class SetID : Widget
    {
        public Widget? Input { private get; set; }

        public string? Label { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.PushID(Label ?? string.Empty);
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.PopID();
            }
        }
    }
}
