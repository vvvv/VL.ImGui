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

    /// <summary>
    /// The style applies to TabItems in a TabBar.
    /// </summary>
    [GenerateNode(Fragmented = false, Category = "ImGui.Styling", Tags = "TabHovered TabActive TabUnfocused TabUnfocusedActive TabRounding")]
    internal partial class SetTabStyle : Widget
    {
        public Widget? Input { private get; set; }

        public Optional<Color4> Background { private get; set; }

        public Optional<Color4> Hovered { private get; set; }

        public Optional<Color4> Active { private get; set; }

        public Optional<Color4> Unfocused { private get; set; }

        public Optional<Color4> UnfocusedActive { private get; set; }

        /// <summary>
        /// Radius of upper corners of a tab. Set to 0.0 to have rectangular tabs.
        /// </summary>
        public Optional<float> Rounding { private get; set; }

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
                    ImGui.PushStyleColor(ImGuiCol.Tab, Background.Value.ToImGui());
                }
                if (Hovered.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TabHovered, Hovered.Value.ToImGui());
                }
                if (Active.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TabActive, Active.Value.ToImGui());
                }
                if (Unfocused.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TabUnfocused, Unfocused.Value.ToImGui());
                }
                if (UnfocusedActive.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TabUnfocusedActive, UnfocusedActive.Value.ToImGui());
                }

                if (Rounding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, Rounding.Value);
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
