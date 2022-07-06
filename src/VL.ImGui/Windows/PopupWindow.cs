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
    public sealed partial class PopupWindow : Widget
    {
        int _openCloseCount;

        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public RectangleF Bounds { get; set; }

        public ImGuiNET.ImGuiWindowFlags Flags { private get; set; }

        public void Open()
        {
            _openCloseCount++;
        }

        public void Close()
        {
            _openCloseCount--;
        }

        internal override void Update(Context context)
        {
            if (_openCloseCount > 0)
            {
                ImGui.OpenPopup(Label ?? string.Empty);
            }

            ImGui.SetNextWindowPos(Bounds.TopLeft.ToImGui(), 0);
            ImGui.SetNextWindowSize(Bounds.Size.ToImGui());

            if (ImGui.BeginPopup(Label ?? string.Empty, Flags))
            {
                try
                {
                    if (_openCloseCount < 0)
                    {
                        ImGui.CloseCurrentPopup();
                    }
                    else
                    {
                        context.Update(Content);
                    }
                }
                finally
                {
                    ImGui.EndPopup();
                }

            }

            _openCloseCount = 0;
        }
    }
}
