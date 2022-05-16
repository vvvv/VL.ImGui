using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Core;
using VL.Core.CompilerServices;

[assembly:AssemblyInitializer(typeof(VL.ImGui.Skia.Initialization))]

namespace VL.ImGui.Skia
{
    public sealed class Initialization : AssemblyInitializer<Initialization>
    {
        protected override void RegisterServices(IVLFactory factory)
        {
            factory.RegisterNodeFactory(NodeBuilding.NewNodeFactory(factory, "VL.ImGUI.Skia.Nodes", f =>
            {
                var nodes = GetNodes(f).ToImmutableArray();
                return NodeBuilding.NewFactoryImpl(nodes);
            }));
        }

        static IEnumerable<IVLNodeDescription> GetNodes(IVLNodeDescriptionFactory factory)
        {
            yield return SkiaWidget.GetNodeDescription(factory);
        }
    }
}
