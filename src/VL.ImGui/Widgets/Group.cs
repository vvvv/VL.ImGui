using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    internal sealed class Group : Widget
    {
        public IEnumerable<Widget> Children { get; set; }

        internal override void Update(Context context)
        {
            var count = Children.Count();
            if (count > 0)
            {
                var width = ImGuiNET.ImGui.GetContentRegionAvail().X / count;

                ImGuiNET.ImGui.BeginGroup();
                ImGuiNET.ImGui.PushItemWidth(width);
                try
                {
                    var i = 0;
                    foreach (var child in Children)
                    {
                        if (i++ > 0)
                            ImGuiNET.ImGui.SameLine();
                        if (child is null)
                            ImGuiNET.ImGui.Dummy(new System.Numerics.Vector2(width, 0.1f));
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

        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {
            return factory.NewNodeDescription("Group", "ImGui", fragmented: true, _c =>
            {
                var _w = new Group();
                var _inputs = new[]
                {
                    _c.Input("Input", _w.Input),
                    _c.Input("Children", _w.Children)
                };
                var _outputs = new[]
                {
                    _c.Output<Widget>("Output"),
                };
                return _c.NewNode(_inputs, _outputs, c =>
                {
                    var s = new Group();
                    var inputs = new IVLPin[]
                    {
                        c.Input(v => s.Input = v, s.Input),
                        c.Input(v => s.Children = v, s.Children)
                    };
                    var outputs = new IVLPin[]
                    {
                        c.Output(() => s)
                    };
                    return c.Node(inputs, outputs);
                });
            });
        }
    }
}
