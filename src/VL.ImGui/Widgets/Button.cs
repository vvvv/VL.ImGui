﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Button")]
    internal partial class Button : Widget
    {

        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public BehaviorSubject<bool> ObservableValue { get; } = new BehaviorSubject<bool>(false);

        internal override void Update(Context context)
        {
            var size = ImGuiConversion.FromVector2(Size);
            if (ImGuiNET.ImGui.Button(Label ?? string.Empty, size))
                ObservableValue.OnNext(true);
        }
    }
}
