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
    internal partial class SetChildWindowStyle : Widget
    {
        public Widget? Input { private get; set; }

        public Optional<Color4> Background { private get; set; }

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
                    ImGui.PushStyleColor(ImGuiCol.ChildBg, Background.Value.ToImGui());
                }
                if (Rounding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, Rounding.Value);
                }
                if (BorderSize.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.ChildBorderSize, BorderSize.Value);
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
