using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal sealed partial class Disabled : Widget
    {

        public Widget? Input { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.BeginDisabled();

            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.EndDisabled();
            }
        }
    }
}
