using Stride.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Core;
using ImGuiNET;

namespace VL.ImGui.Styling
{
    using ImGui = ImGuiNET.ImGui;

    // We decided that the style nodes shall take all the relevant values in one go (= disable fragments).
    [GenerateNode(Fragmented = false)]
    internal partial class SetFrameStyle : Widget
    {
        public Widget? Input { private get; set; }

        public Optional<Color4> Background { private get; set; }

        public Optional<Color4> Active { private get; set; }

        public Optional<Color4> Hovered { private get; set; }

        public Optional<Vector2> Padding { private get; set; }

        public Optional<float> Rounding { private get; set; }

        public Optional<float> BorderSize { private get; set; }

        internal override void Update(Context context)
        {
            if (Input is null)
                return;

            var colorCount = 0;
            var valueCount = 0;
            try
            {
                if (Background.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.FrameBg, Background.Value.ToImGui());
                }
                if (Active.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.FrameBgActive, Active.Value.ToImGui());
                }
                if (Hovered.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, Hovered.Value.ToImGui());
                }

                if (Padding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, Padding.Value.ToImGui());
                }
                if (Rounding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, Rounding.Value);
                }
                if (BorderSize.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, BorderSize.Value);
                }

                context.Update(Input);
            }
            finally
            {
                if (colorCount > 0)
                    ImGui.PopStyleColor(colorCount);
                if (valueCount > 0)
                    ImGui.PopStyleVar(valueCount);
            }
        }
    }
}
