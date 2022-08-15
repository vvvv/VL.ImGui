namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    public sealed partial class PopupCore : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public ImGuiNET.ImGuiWindowFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            if (ImGui.BeginPopup(Label ?? string.Empty, Flags))
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
