namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets.Internal", GenerateRetained = false)]
    internal sealed partial class DisabledCore : Widget
    {

        public Widget? Input { private get; set; }

        public bool Apply { private get; set; } = true;

        internal override void UpdateCore(Context context)
        {

            if (Apply)
            {
                ImGuiNET.ImGui.BeginDisabled();

                try
                {
                    context.Update(Input);
                }
                finally
                {
                    ImGuiNET.ImGui.EndDisabled();
                }
            }

        }
    }
}
