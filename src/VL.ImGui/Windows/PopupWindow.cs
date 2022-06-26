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
    internal sealed partial class PopupWindow : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public bool Open { get; set; } = false;

        public RectangleF Bounds { get; set; }

        public bool IsVisible { get; private set; }

        public ImGuiNET.ImGuiWindowFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            if (Open)
                ImGuiNET.ImGui.OpenPopup(Label ?? String.Empty);

            ImGui.SetNextWindowPos(Bounds.TopLeft.ToImGui(), 0);
            ImGui.SetNextWindowSize(Bounds.Size.ToImGui());

            IsVisible = ImGuiNET.ImGui.BeginPopup (Label ?? string.Empty, Flags);

            if (IsVisible)
            {
                context.Update(Content);
                ImGuiNET.ImGui.EndPopup();
            }

            //try
            //{
            //    if (IsVisible)
            //    {
            //        context.Update(Content);    
            //    }
            //}
            //finally
            //{
            //    ImGuiNET.ImGui.EndPopup();
            //}
        }
    }
}
