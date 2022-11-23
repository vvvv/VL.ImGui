using Stride.Core.Mathematics;
using ImGuiNET;
using VL.Lib.Reactive;
using System.Reactive;

namespace VL.ImGui.Windows
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal sealed partial class WindowCore : Widget
    {
        public Widget? Content { get; set; }

        public string Name { get; set; } = "Window";

        /// <summary>
        /// If set the window will have a close button which will push to the channel once clicked.
        /// </summary>
        public Channel<Unit> Closing { get; set; } = DummyChannel<Unit>.Instance;

        /// <summary>
        /// Bounds of the Window.
        /// </summary>
        public Channel<RectangleF>? Bounds { private get; set; }
        ChannelFlange<RectangleF> BoundsFlange = new ChannelFlange<RectangleF>(new RectangleF(0f, 0f, 1f, 1f));

        /// <summary>
        /// Returns true if the Window is open (not collapsed or clipped). Set to true to open the window.
        /// </summary>
        public Channel<bool>? IsOpen { private get; set; }
        ChannelFlange<bool> IsOpenFlange = new ChannelFlange<bool>(true);
        /// <summary>
        /// Returns true if the Window is open (not collapsed or clipped). 
        /// </summary>
        public bool _IsOpen => IsOpenFlange.Value;

        public ImGuiWindowFlags Flags { get; set; }

        internal override void UpdateCore(Context context)
        {
            var isOpen = IsOpenFlange.Update(IsOpen);

            var bounds = BoundsFlange.Update(Bounds, out bool boundsChanged);

            if (boundsChanged)
            {
                ImGui.SetNextWindowPos (bounds.TopLeft.FromHectoToImGui());
                ImGui.SetNextWindowSize (bounds.Size.FromHectoToImGui());
            }

            ImGui.SetNextWindowCollapsed(!isOpen);

            if (Closing.IsValid())
            {
                var visible = true;
                isOpen = ImGui.Begin(Name, ref visible, Flags);
                if (!visible)
                    Closing.Value = default;
            }
            else
            {
                isOpen = ImGui.Begin(Name, Flags);
            }

            try
            {
                IsOpenFlange.Value = isOpen;

                if (isOpen)
                {
                    context.Update(Content);
                }     
            }
            finally
            {
                ImGui.End();
            }
        }
    }
}
