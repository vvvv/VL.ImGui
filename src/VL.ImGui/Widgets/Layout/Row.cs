using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal sealed partial class Row : Widget
    {
        public IEnumerable<Widget> Children { get; set; } = Enumerable.Empty<Widget>();

        public float Width { private get; set; } = 0f;

        internal override void Update(Context context)
        {
            var count = Children.Count();
            if (count > 0)
            {
                if (Width != 0)
                {
                    Width = ImGuiNET.ImGui.GetContentRegionAvail().X / count;
                }

                ImGuiNET.ImGui.BeginGroup();
                ImGuiNET.ImGui.PushItemWidth(Width);
                try
                {
                    var i = 0;
                    foreach (var child in Children)
                    {
                        if (i++ > 0)
                            ImGuiNET.ImGui.SameLine();
                        if (child is null)
                            ImGuiNET.ImGui.Dummy(new System.Numerics.Vector2(Width, 0.1f));
                        else
                            context.Update(child);
                    }
                }
                finally
                {
                    ImGuiNET.ImGui.PopItemWidth();
                    ImGuiNET.ImGui.EndGroup();
                }
            }
        }
    }
}
