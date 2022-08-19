using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    [GenerateNode(Category = "ImGui.Widgets", GenerateImmediate = false)]
    internal sealed partial class ChildWindow : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public bool HasBorder { get; set; }

        public Vector2 Size { get; set; }

        public ImGuiNET.ImGuiWindowFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            
            var IsVisible = ImGui.BeginChild(Label ?? string.Empty, Size.ToImGui(), HasBorder, Flags);
            
            try
            {
                if (IsVisible)
                {
                    context?.Update(Content);
                }
            }
            finally
            {
                ImGui.EndChild();
            }
        }
    }
}
