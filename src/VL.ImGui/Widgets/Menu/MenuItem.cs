using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal partial class MenuItem : Widget
    {

        public string? Label { get; set; }

        public string? Shortcut { get; set; }

        public bool Enabled { get; set; } = true;

        public BehaviorSubject<Boolean> Value { get; } = new BehaviorSubject<Boolean>(false);

        internal override void Update(Context context)
        {
            var selected = Value.Value;
            if (ImGuiNET.ImGui.MenuItem(Label ?? string.Empty, Shortcut, ref selected, Enabled ))
                Value.OnNext(selected);
        }
    }
}
