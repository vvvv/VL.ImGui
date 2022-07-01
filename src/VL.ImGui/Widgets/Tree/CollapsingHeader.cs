using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Tags = "tree")]
    internal sealed partial class CollapsingHeader : Widget
    {
        public IEnumerable<Widget> Items { get; set; } = Enumerable.Empty<Widget>();

        public string? Label { get; set; }

        /// <summary>
        /// Display an additional small close button on upper right of the header
        /// </summary>
        public bool HasCloseButton { get; set; } = true;

        /// <summary>
        /// Returns true if the header is displayed. Set to true to display the header.
        /// </summary>
        public BehaviorSubject<bool> IsVisible { get; } = new BehaviorSubject<bool>(true);

        /// <summary>
        /// Returns true if the Header is open (not collapsed). Set to true to open the header.
        /// </summary>
        public BehaviorSubject<bool> IsOpen { get; } = new BehaviorSubject<bool>(false);

        public ImGuiNET.ImGuiTreeNodeFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var value = IsVisible.Value;
            bool isOpen;

            ImGuiNET.ImGui.SetNextItemOpen(IsOpen.Value);

            if (HasCloseButton)
            {
                isOpen = ImGuiNET.ImGui.CollapsingHeader(Label ?? string.Empty, ref value, Flags);
                if (IsVisible.Value != value)
                    IsVisible.OnNext(value);
            }
            else
            {
                isOpen = ImGuiNET.ImGui.CollapsingHeader(Label ?? string.Empty, Flags);
            }

            if (isOpen != IsOpen.Value)
                IsOpen.OnNext(isOpen);

            if (isOpen)
            {
                var count = Items.Count(x => x != null);

                if (count > 0)
                {
                    foreach (var item in Items)
                    {
                        if (item is null)
                            continue;
                        else
                            context.Update(item);
                    }
                }
            }
        }
    }
}
