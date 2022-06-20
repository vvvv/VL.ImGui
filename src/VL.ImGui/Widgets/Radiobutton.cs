using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class RadioButton : Widget
    {

        public string? Label { get; set; }

        public BehaviorSubject<Boolean> Value { get; } = new BehaviorSubject<Boolean>(false);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.RadioButton(Label ?? string.Empty, value))
                Value.OnNext(value);
        }
    }
}
