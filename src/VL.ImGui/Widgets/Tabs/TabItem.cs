namespace VL.ImGui.Widgets
{

    [GenerateNode(Category = "ImGui.Widgets")]
    internal sealed partial class TabItem : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        /// <summary>
        /// Display an additional small close button 
        /// </summary>
        public bool HasCloseButton { get; set; } = true;

        /// <summary>
        /// Returns true if the header is displayed. Set to true to display the header.
        /// </summary>
        public Channel<bool>? IsVisible { private get; set; }
        ChannelFlange<bool> IsVisibleFlange = new ChannelFlange<bool>(true);
        /// <summary>
        /// Returns true if the header is displayed.
        /// </summary>
        public bool _IsVisible => IsVisibleFlange.Value;

        /// <summary>
        /// Returns true if the Header is activated/selected. Set to true to activate the tab.
        /// </summary>
        public Channel<bool>? IsActive { private get; set; }
        ChannelFlange<bool> IsActiveFlange = new ChannelFlange<bool>(false);
        /// <summary>
        /// Returns true if the Tab is activated/selected. 
        /// </summary>
        public bool _IsActive => IsActiveFlange.Value;


        public ImGuiNET.ImGuiTabItemFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var isVisible = IsVisibleFlange.Update(IsVisible);
            var isActive = IsActiveFlange.Update(IsActive, out var activateHasChanged);
            var gotActivated = activateHasChanged && isActive;
            var flags = Flags;

            if (isVisible && gotActivated)
                flags |= ImGuiNET.ImGuiTabItemFlags.SetSelected;

            if (HasCloseButton || flags != ImGuiNET.ImGuiTabItemFlags.None)
            {
                isActive = ImGuiNET.ImGui.BeginTabItem(Label ?? string.Empty, ref isVisible, Flags);
                IsVisibleFlange.Value = isVisible; // close button might have been pressed
            }
            else
                isActive = ImGuiNET.ImGui.BeginTabItem(Label ?? string.Empty);

            IsActiveFlange.Value = isActive;

            if (isActive)
            {
                try
                {
                    context.Update(Content);
                }
                finally
                {
                    ImGuiNET.ImGui.EndTabItem();
                }
            }
        }
    }
}
