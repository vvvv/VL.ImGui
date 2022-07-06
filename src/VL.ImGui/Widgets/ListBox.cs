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
    internal partial class ListBox : Widget
    {

        public string? Label { get; set; }

        public Vector2 Size { get; set; } = Vector2.Zero;

        public IEnumerable<string> Items { get; set; } = Enumerable.Empty<string>();

        public BehaviorSubject<string> Value { get; } = new BehaviorSubject<string>("");

        internal override void Update(Context context)
        {
            var value = Value.Value;

            var count = Items.Count();
            if (count > 0)
            {
                if (ImGuiNET.ImGui.BeginListBox(Label ?? string.Empty, Size.ToImGui()))
                {
                    try
                    {
                        foreach (var item in Items)
                        {
                            bool is_selected = value == item;
                            if (ImGuiNET.ImGui.Selectable(item, is_selected))
                            {
                                Value.OnNext(item);
                            }
                            if (is_selected)
                            {
                                ImGuiNET.ImGui.SetItemDefaultFocus();
                            }
                        }
                    }
                    finally
                    {
                        ImGuiNET.ImGui.EndListBox();
                    }
                }
            }
        }
    }
}
