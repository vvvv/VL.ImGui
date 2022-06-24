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

        public int Index { get; set; }

        public BehaviorSubject<int> Value { get; set; } = new BehaviorSubject<int>(-1);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.RadioButton(Label ?? string.Empty, ref value, Index))
                if (value == Index) // radio button not only changed but got turned on
                    Value.OnNext(value);
        }
    }
}
