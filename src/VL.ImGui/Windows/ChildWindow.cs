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
    internal sealed partial class ChildWindow : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public bool HasBorder { get; set; }

        public RectangleF Bounds { get; set; }

        public Vector2 Pivot { get; set; }

        public bool IsVisible { get; private set; }

        public ImGuiNET.ImGuiWindowFlags Flags { private get; set; }


        internal override void Update(Context context)
        {

            ImGui.SetNextWindowPos(Bounds.TopLeft.ToImGui(), 0, Pivot.ToImGui());

            IsVisible = ImGuiNET.ImGui.BeginChild(Label ?? string.Empty, Bounds.Size.ToImGui(), HasBorder, Flags);
            
            try
            {
                if (IsVisible)
                {
                    context.Update(Content);
                }
            }
            finally
            {
                ImGuiNET.ImGui.EndChild();
            }
        }
    }
}
