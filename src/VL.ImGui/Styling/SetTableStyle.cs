﻿using Stride.Core.Mathematics;
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
    internal partial class SetTableStyle : Widget
    {
        public Widget? Input { private get; set; }

        public Optional<Color4> HeaderBackground { private get; set; }

        public Optional<Color4> RowBackground { private get; set; }

        public Optional<Color4> RowBackgroundAlt { private get; set; }

        public Optional<Color4> BorderStrongColor { private get; set; }

        public Optional<Color4> BorderLightColor { private get; set; }

        public Optional<Vector2> CellPadding { private get; set; }

        internal override void Update(Context context)
        {
            if (Input is null)
                return;

            var colorCount = 0;
            var valueCount = 0;
            try
            {
                if (HeaderBackground.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TableHeaderBg, HeaderBackground.Value.ToImGui());
                }
                if (RowBackground.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TableRowBg, RowBackground.Value.ToImGui());
                }
                if (RowBackgroundAlt.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TableRowBgAlt, RowBackgroundAlt.Value.ToImGui());
                }
                if (BorderStrongColor.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TableBorderStrong, BorderStrongColor.Value.ToImGui());
                }
                if (BorderLightColor.HasValue)
                {
                    colorCount++;
                    ImGui.PushStyleColor(ImGuiCol.TableBorderLight, BorderLightColor.Value.ToImGui());
                }

                if (CellPadding.HasValue)
                {
                    valueCount++;
                    ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, CellPadding.Value.ToImGui());
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