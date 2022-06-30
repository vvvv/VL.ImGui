using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Tags = "tree")]
    internal sealed partial class CollapsingHeader : Widget
    {

        public IEnumerable<Widget> Items { get; set; } = Enumerable.Empty<Widget>();

        public string? Label { get; set; }

        /// <summary>
        /// If Visible is true: display an additional small close button on upper right of the header which will set the bool to false when clicked, if Value is false don't display the header.
        /// </summary>
        public BehaviorSubject<bool> Value { get; } = new BehaviorSubject<bool>(false);

        public ImGuiNET.ImGuiTreeNodeFlags Flags { private get; set; }

        public bool IsVisible { get; private set; }

        internal override void Update(Context context)
        {
            var count = Items.Count(x => x != null);

            if (count > 0)
            {
                var value = Value.Value;
                IsVisible = ImGuiNET.ImGui.CollapsingHeader(Label ?? string.Empty, ref value, Flags);

                if (value != Value.Value)
                    Value.OnNext(value);

                if (IsVisible)
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
