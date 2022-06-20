﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using VL.ImGui;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text")]
    internal partial class TextWidget : Widget
    {
        public string? Text { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.Text(Text ?? String.Empty);
        }
    }
}