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
    [GenerateNode(Fragmented = false, Category = "ImGui.Styling", Tags = "GrabMinSize GrabRounding")]
    internal partial class SetGrabStyle : Widget
    {
        public Widget? Input { private get; set; }

        /// <summary>
        /// Minimum width/height of a grab box for slider/scrollbar.
        /// </summary>
        public Optional<float> MinSize { private get; set; }

        /// <summary>
        /// Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.
        /// </summary>
        public Optional<float> Rounding { private get; set; }

        internal override void Update(Context context)
        {
            if (Input is null)
                return;

            var valueCount = 0;
            try
            {
                if (MinSize.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.GrabMinSize, MinSize.Value);
                }
                if (Rounding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, Rounding.Value);
                }

                context.Update(Input);
            }
            finally
            {
                if (valueCount > 0)
                    ImGui.PopStyleVar(valueCount);
            }
        }
    }
}
