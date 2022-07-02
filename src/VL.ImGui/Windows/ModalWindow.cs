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

    /// <summary>
    /// Modal Windows block normal mouse hovering detection (and therefore most mouse interactions) behind them. They can't be closed by clicking outside of them.
    /// </summary>
    [GenerateNode]
    public sealed partial class ModalWindow : Widget
    {
        int _openCloseCount;

        public Widget? Content { get; set; }

        public string? Label { get; set; }

        /// <summary>
        /// Display a regular close button.
        /// </summary>
        public bool HasCloseButton { get; set; } = true;

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

            bool isVisible;
            bool isOpen = true;

            if (_openCloseCount > 0)
            {
                ImGui.OpenPopup(Label ?? string.Empty);
            }

            ImGui.SetNextWindowPos(Bounds.TopLeft.ToImGui(), 0);
            ImGui.SetNextWindowSize(Bounds.Size.ToImGui());

            if (HasCloseButton)
            {
                isVisible = ImGui.BeginPopupModal(Label ?? string.Empty, ref isOpen, Flags);
            }
            else
            {
                isVisible = ImGui.BeginPopupModal(Label ?? string.Empty);
            }
                        
            
            if (isVisible)
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
