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
    internal sealed partial class TabBar : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public ImGuiNET.ImGuiTabBarFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            if (ImGuiNET.ImGui.BeginTabBar(Label ?? string.Empty, Flags))
            {
                try
                {
                    context.Update(Content);
                }
                finally
                {
                    ImGuiNET.ImGui.EndTabBar();
                }
            }
        }
    }
}
