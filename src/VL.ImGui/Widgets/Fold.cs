using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(GenerateImmediate = false)]
    internal sealed partial class Fold : Widget
    {
        public IEnumerable<Widget> Children { get; set; } = Enumerable.Empty<Widget>();

        internal override void Update(Context context)
        {
            var count = Children.Count(x => x != null);
            if (count > 0)
            {
                foreach (var child in Children)
                {
                    if (child is null)
                        continue;
                    else
                        context.Update(child);
                }
            }
        }
    }
}
