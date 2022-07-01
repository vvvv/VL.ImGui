using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Tags = "tree")]
    internal sealed partial class TreeNode : Widget
    {

        public IEnumerable<Widget> Items { get; set; } = Enumerable.Empty<Widget>();

        public string? Label { get; set; }

        public ImGuiNET.ImGuiTreeNodeFlags Flags { private get; set; }

        public bool IsVisible { get; private set; }

        internal override void Update(Context context)
        {
            var count = Items.Count(x => x != null);

            if (count > 0)
            {

                IsVisible = ImGuiNET.ImGui.TreeNodeEx(Label ?? string.Empty, Flags);

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
                        ImGuiNET.ImGui.TreePop();
                    }
                    
                }
            }
        }
    }
}
