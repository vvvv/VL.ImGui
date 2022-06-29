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
    /// This style applies to popups, menus, tooltips windows.
    /// </summary>
    [GenerateNode(Fragmented = false, Category = "ImGui.Styling", Tags = "Menu Tooltip PopupBg PopupRounding PopupBorderSize")]
    internal partial class SetPopupStyle : Widget
    {
        public Widget? Input { private get; set; }

        /// <summary>
        /// Background of popups, menus, tooltips windows
        /// </summary>
        public Optional<Color4> Background { private get; set; }

        /// <summary>
        /// Radius of popup window corners rounding.
        /// </summary>
        public Optional<float> Rounding { private get; set; }

        /// <summary>
        /// Thickness of border around popup/tooltip windows. Generally set to 0.0 or 1.0. (Other values are not well tested and more CPU/GPU costly).
        /// </summary>
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
                    ImGui.PushStyleColor(ImGuiCol.PopupBg, Background.Value.ToImGui());
                }
                if (Rounding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.PopupRounding, Rounding.Value);
                }
                if (BorderSize.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, BorderSize.Value);
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
