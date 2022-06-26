using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode]
    internal sealed partial class OpenPopup : Widget
    {
        public string? Label { get; set; }

        public ImGuiNET.ImGuiPopupFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.OpenPopup(Label ?? String.Empty, Flags);
        }
    }
}
