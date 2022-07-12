using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;
using System.Reactive;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Button = true)]
    internal partial class Button : ChannelWidget<Unit>
    {
        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        internal override void Update(Context context)
        {
            Update();
            if (ImGuiNET.ImGui.Button(Label ?? string.Empty, Size.ToImGui()))
                Value = Unit.Default;
        }
    }
}
