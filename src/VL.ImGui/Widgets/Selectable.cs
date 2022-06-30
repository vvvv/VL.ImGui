using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// A selectable highlights when hovered, and can display another color when selected. Neighbors selectable extend their highlight bounds in order to leave no gap between them. This is so a series of selected Selectable appear contiguous.
    /// </summary>
    [GenerateNode]
    internal partial class Selectable : Widget
    {

        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public ImGuiNET.ImGuiSelectableFlags Flags { private get; set; }

        public BehaviorSubject<bool> Value { get; } = new BehaviorSubject<bool>(false);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.Selectable(Label ?? string.Empty, ref value, Flags, Size.ToImGui()))
                Value.OnNext(value);
        }
    }
}
