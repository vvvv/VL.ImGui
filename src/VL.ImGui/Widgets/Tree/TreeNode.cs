namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", Tags = "tree", GenerateImmediate = false)]
    internal sealed partial class TreeNode : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public ImGuiNET.ImGuiTreeNodeFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            if (ImGuiNET.ImGui.TreeNodeEx(Label ?? string.Empty, Flags))
            {
                try
                {
                    context?.Update(Content);
                }
                finally
                {
                    ImGuiNET.ImGui.TreePop();
                }
            }
        }
    }
}
