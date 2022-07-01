using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name ="Button (Small)", Category = "ImGui.Widgets")]
    internal partial class ButtonSmall : Widget
    {
        public string? Label { get; set; }

        public BehaviorSubject<bool> Value { get; } = new BehaviorSubject<bool>(false);

        internal override void Update(Context context)
        {
            if (ImGuiNET.ImGui.SmallButton(Label ?? string.Empty))
                Value.OnNext(true);
        }
    }
}
