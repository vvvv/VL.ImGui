﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class GetContentRegionAvail : Widget
    {

        public Vector2 Value { get; private set; }


        internal override void Update(Context context)
        {
            var size = ImGuiNET.ImGui.GetContentRegionAvail();
            Value = ImGuiConversion.ToVector2(size);
        }
    }
}
