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
    [GenerateNode(Fragmented = false, Category = "ImGui.Styling", Tags = "Color DisabledColor SelectedTextBg")]
    internal partial class SetTextStyle : Widget
    {
        public Widget? Input { private get; set; }

        public Optional<Color4> Color { private get; set; }

        public Optional<Color4> DisabledColor { private get; set; }

        public Optional<Color4> SelectedTextBackground { private get; set; }


        internal override void Update(Context context)
        {
            if (Input is null)
                return;

            var colorCount = 0;
            try
            {
                if (Color.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.Text, Color.Value.ToImGui());
                }
                if (DisabledColor.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TextDisabled, DisabledColor.Value.ToImGui());
                }
                if (SelectedTextBackground.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TextSelectedBg, SelectedTextBackground.Value.ToImGui());
                }

                context.Update(Input);
            }
            finally
            {
                if (colorCount > 0)
                    ImGui.PopStyleColor(colorCount);
            }
        }
    }
}
