using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode(Category = "ImGui.Widgets", GenerateImmediate = false)]
    public sealed partial class Popup : Widget
    {

        int _openCloseCount;

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

        internal override void Update(Context context)
        {
            var bounds = BoundsFlange.Update(Bounds);
            var isOpen = IsOpenFlange.Update(IsOpen, out bool hasChanged);

            if (hasChanged)
            {
                if (isOpen)
                {
                    _openCloseCount++;
                }
                else
                {
                    _openCloseCount--;
                }
            }

            if (_openCloseCount > 0)
            {
                ImGui.OpenPopup(Label ?? string.Empty);
            }

            ImGui.SetNextWindowPos(bounds.TopLeft.ToImGui());
            ImGui.SetNextWindowSize(bounds.Size.ToImGui());

            var opened = ImGui.BeginPopup(Label ?? string.Empty, Flags);
            IsOpenFlange.Value = opened;

            if (opened)
            {
                try
                {
                    if (_openCloseCount < 0)
                    {
                        ImGui.CloseCurrentPopup();
                    }
                    else
                    {
                        context?.Update(Content);
                    }
                }
                finally
                {
                    ImGui.EndPopup();
                }

                _openCloseCount = 0;
            }

        }
    }
}
