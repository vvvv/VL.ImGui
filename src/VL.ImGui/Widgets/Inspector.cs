using Stride.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    internal class Inspector : Widget
    {
        private object _value, _cachedValue;

        public object Value
        {
            get => _value;
            set
            {
                if (!Equals(value, _cachedValue))
                {
                    _cachedValue = _value = value;
                }
            }
        }

        public bool IsModified { get; set; }

        internal override void Reset()
        {
            IsModified = false;
        }

        internal override void Update(Context context)
        {
            IsModified = ImGuiUtils.InputObject(string.Empty, ref _value);
        }

        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {
            return factory.NewNodeDescription("Inspector", "ImGui", fragmented: true, _c =>
            {
                var _w = new Inspector();
                var _inputs = new[]
                {
                    _c.Input("Input", _w.Input),
                    //_c.Input("Label", _w.Label),
                    _c.Input("Value", _w.Value)
                };
                var _outputs = new[]
                {
                    _c.Output<Widget>("Output"),
                    _c.Output("Value", _w.Value),
                    _c.Output("Is Modified", _w.IsModified)
                };
                return _c.NewNode(_inputs, _outputs, c =>
                {
                    var s = new Inspector();
                    var inputs = new IVLPin[]
                    {
                        c.Input(v => s.Input = v, s.Input),
                        //c.Input(v => s.Label = v, s.Label),
                        c.Input(v => s.Value = v, s.Value)
                    };
                    var outputs = new IVLPin[]
                    {
                        c.Output(() => s),
                        c.Output(() => s.Value),
                        c.Output(() => s.IsModified)
                    };
                    return c.Node(inputs, outputs);
                });
            });
        }
    }
}
