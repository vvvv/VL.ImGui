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

        /// <summary>
        /// Returns true if the Window is open (not fully clipped). 
        /// </summary>
        public bool IsOpen { get;  private set; }

        internal override void UpdateCore(Context context)
        {

            IsOpen = ImGui.BeginChild(Label ?? string.Empty, Size.ToImGui(), HasBorder, Flags);
            
            try
            {
                if (IsOpen)
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
