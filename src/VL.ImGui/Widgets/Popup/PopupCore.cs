using Stride.Core.Mathematics;
using VL.Lib.Reactive;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    public sealed partial class PopupCore : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        /// <summary>
        /// Bounds of the Window.
        /// </summary>
        public Channel<RectangleF>? Bounds { private get; set; }
        ChannelFlange<RectangleF> BoundsFlange = new ChannelFlange<RectangleF>(new RectangleF(0f, 0f, 100f, 100f));

        /// <summary>
        /// Returns true if the Popup is open. Set to true to open the Popup.
        /// </summary>
        public Channel<bool>? IsOpen { private get; set; }
        ChannelFlange<bool> IsOpenFlange = new ChannelFlange<bool>(false);
        /// <summary>
        /// Returns true if the Popup is open. 
        /// </summary>
        public bool _IsOpen => IsOpenFlange.Value;

        public ImGuiNET.ImGuiWindowFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var bounds = BoundsFlange.Update(Bounds);
            var isOpen = IsOpenFlange.Update(IsOpen, out bool hasChanged);

            ImGui.SetNextWindowPos(bounds.TopLeft.ToImGui());
            ImGui.SetNextWindowSize(bounds.Size.ToImGui());

            if (isOpen && hasChanged && Label != null)
                ImGui.OpenPopup(Label);

            isOpen = ImGui.BeginPopup(Label ?? string.Empty, Flags);
            IsOpenFlange.Value = isOpen;

            if (isOpen)
            {
                try
                {
                    context?.Update(Content);
                }
                finally
                {
                    ImGui.EndPopup();
                }
            }

        }
    }
}
