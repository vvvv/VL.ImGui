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
    internal sealed partial class TabBar : Widget
    {
        public IEnumerable<Widget> Items { get; set; } = Enumerable.Empty<Widget>();

        public string? Label { get; set; }

        public bool IsVisible { get; private set; }

        public ImGuiNET.ImGuiTabBarFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var count = Items.Count(x => x != null);

            if (count > 0)
            {
                IsVisible = ImGuiNET.ImGui.BeginTabBar(Label ?? string.Empty, Flags);

                if (IsVisible)
                {
                    try
                    {
                        foreach (var item in Items)
                        {
                            if (item is null)
                                continue;
                            else
                                context.Update(item);

                        }
                    }
                    finally
                    {
                        ImGuiNET.ImGui.EndTabBar();
                    }
                    
                }
            }
        }
    }
}
