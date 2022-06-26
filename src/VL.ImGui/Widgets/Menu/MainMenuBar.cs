using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "MenuBar (Main)")]
    internal sealed partial class MainMenuBar : Widget
    {

        public IEnumerable<Widget> Items { get; set; } = Enumerable.Empty<Widget>();

        public bool IsVisible { get; private set; }

        internal override void Update(Context context)
        {
            var count = Items.Count(x => x != null);

            if (count > 0)
            {

                IsVisible = ImGuiNET.ImGui.BeginMainMenuBar();

                if (IsVisible)
                {
                    foreach (var item in Items)
                    {
                        if (item is null)
                            continue;
                        else
                            context.Update(item);
                    }
                    ImGuiNET.ImGui.EndMainMenuBar();
                }

            }
        }
    }
}
