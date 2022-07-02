﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Name = "Tooltip (Widget)")]
    internal partial class TooltipWidget : Widget
    {
        public Widget? Content { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.BeginTooltip();
            try
            {
                context.Update(Content);
            }
            finally
            {
                ImGuiNET.ImGui.EndTooltip();
            }
        }
    }
}