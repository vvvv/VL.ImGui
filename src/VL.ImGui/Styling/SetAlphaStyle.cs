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
    internal partial class SetAlphaStyle : Widget
    {
        public Widget? Input { private get; set; }

        public Optional<float> Alpha { private get; set; }

        public Optional<float> DisabledAlpha { private get; set; }

        internal override void Update(Context context)
        {
            if (Input is null)
                return;

            var valueCount = 0;
            try
            {
                if (Alpha.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.Alpha, Alpha.Value);
                }
                if (DisabledAlpha.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.DisabledAlpha, DisabledAlpha.Value);
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
