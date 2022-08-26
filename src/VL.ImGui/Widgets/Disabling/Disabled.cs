namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal sealed partial class Disabled : Widget
    {

        public Widget? Input { private get; set; }

        internal override void UpdateCore(Context context)
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
