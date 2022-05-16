using VL.Core;

namespace VL.ImGui.Windows
{
    public sealed class DemoWindow : Widget
    {
        public bool HasCloseButton { get; set; } = true;

        public bool Closing { get; private set; }

        internal override void Reset()
        {
            Closing = false;
        }

        internal override void Update(Context context)
        {
            if (HasCloseButton)
            {
                var open = true;
                ImGuiNET.ImGui.ShowDemoWindow(ref open);
                Closing = !open;
            }
            else
            {
                ImGuiNET.ImGui.ShowDemoWindow();
            }
        }

        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {
            return factory.NewNodeDescription(nameof(DemoWindow), "ImGui", fragmented: true, _c =>
            {
                var _w = new DemoWindow();
                var _inputs = new[]
                {
                    _c.Input("Has Close Button", _w.HasCloseButton)
                };
                var _outputs = new[]
                {
                    _c.Output<Widget>("Output"),
                    _c.Output("Closing", _w.Closing)
                };
                return _c.NewNode(_inputs, _outputs, c =>
                {
                    var s = new DemoWindow();
                    var inputs = new IVLPin[]
                    {
                        c.Input(v => s.HasCloseButton = v, s.HasCloseButton)
                    };
                    var outputs = new IVLPin[]
                    {
                        c.Output(() => s),
                        c.Output(() => s.Closing)
                    };
                    return c.Node(inputs, outputs);
                });
            });
        }
    }
}
