using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VL.Core;
using VL.Core.CompilerServices;
using VL.ImGui.Widgets;
using VL.ImGui.Windows;

[assembly: AssemblyInitializer(typeof(VL.ImGui.Initialization))]
[assembly: InternalsVisibleTo("VL.ImGui.Skia")]

namespace VL.ImGui
{
    public sealed class Initialization : AssemblyInitializer<Initialization>
    {
        protected override void RegisterServices(IVLFactory factory)
        {
            factory.RegisterNodeFactory(NodeBuilding.NewNodeFactory(factory, "VL.ImGUI.Nodes", f =>
            {
                var nodes = GetNodes(f).ToImmutableArray();
                return NodeBuilding.NewFactoryImpl(nodes);
            }));
        }

        static IEnumerable<IVLNodeDescription> GetNodes(IVLNodeDescriptionFactory factory)
        {
            yield return DemoWindow.GetNodeDescription(factory);
            yield return Window.GetNodeDescription(factory);

            yield return Group.GetNodeDescription(factory);

            yield return SliderFloat.GetNodeDescription(factory);
            yield return Inspector.GetNodeDescription(factory);
        }
    }

    internal static class NodeBuildingUtils
    {
        public static IVLPinDescription Input<T>(this NodeBuilding.NodeDescriptionBuildContext c, string name, T defaultValue, string summary = null, string remarks = null)
        {
            return c.Pin(name, typeof(T), defaultValue, summary, remarks);
        }

        public static IVLPinDescription Output<T>(this NodeBuilding.NodeDescriptionBuildContext c, string name, T witness = default, string summary = null, string remarks = null)
        {
            return c.Pin(name, typeof(T), summary, remarks);
        }
    }
}
