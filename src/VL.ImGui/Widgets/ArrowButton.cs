using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "ArrowButton")]
    internal partial class ArrowButton : Widget
    {
        public string? Label { private get; set; }

        public ImGuiNET.ImGuiDir Direction { private get; set; }

        public BehaviorSubject<bool> Value { get; } = new BehaviorSubject<bool>(false);

        internal override void Update(Context context)
        {
            if (ImGuiNET.ImGui.ArrowButton(Label ?? string.Empty, Direction))
                Value.OnNext(true);
        }
    }
}
