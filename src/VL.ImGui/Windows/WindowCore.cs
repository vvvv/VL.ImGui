using Stride.Core.Mathematics;
using ImGuiNET;

namespace VL.ImGui.Windows
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal sealed partial class WindowCore : Widget
    {
        public Widget? Content { get; set; }

        public string Name { get; set; } = "Window";

        public bool HasCloseButton { get; set; } = true;

        /// <summary>
        /// Bounds of the Window.
        /// </summary>
        public Channel<RectangleF>? Bounds { private get; set; }
        ChannelFlange<RectangleF> BoundsFlange = new ChannelFlange<RectangleF>(new RectangleF(0f, 0f, 100f, 100f));

        /// <summary>
        /// Returns true if the Window is open (not collapsed or clipped). Set to true to open the window.
        /// </summary>
        public Channel<bool>? IsOpen { private get; set; }
        ChannelFlange<bool> IsOpenFlange = new ChannelFlange<bool>(true);
        /// <summary>
        /// Returns true if the Window is open (not collapsed or clipped). 
        /// </summary>
        public bool _IsOpen => IsOpenFlange.Value;

        public ImGuiWindowFlags WindowFlags { get; set; }

        internal override void Update(Context context)
        {
            var isOpen = IsOpenFlange.Update(IsOpen);

            bool boundsChanged;
            var bounds = BoundsFlange.Update(Bounds, out boundsChanged);

            if (boundsChanged)
            {
                ImGui.SetNextWindowPos (bounds.TopLeft.ToImGui());
                ImGui.SetNextWindowSize (bounds.Size.ToImGui());
            }

            ImGui.SetNextWindowCollapsed(!isOpen);

            if (HasCloseButton)
            {
                var closing = true;
                isOpen = ImGui.Begin(Name, ref closing, WindowFlags);
            }
            else
            {
                isOpen = ImGui.Begin(Name, WindowFlags);
            }

            IsOpenFlange.Value = isOpen;

            try
            {
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
