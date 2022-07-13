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
    internal sealed partial class TabItem : ChannelWidget<bool>
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public ImGuiNET.ImGuiTabItemFlags Flags { private get; set; }

        public TabItem()
        {
            Value = true;
        }

        internal override void Update(Context context)
        {

            var value = Update();

            if (ImGuiNET.ImGui.BeginTabItem(Label ?? string.Empty, ref value, Flags))
            {
                try
                {
                    context.Update(Content);
                    if (Value != value) 
                        Value = value;
                }
                finally
                {
                    ImGuiNET.ImGui.EndTabItem();
                }

            }
        }
    }
}
