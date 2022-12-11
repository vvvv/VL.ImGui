namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets.Experimental")]
    internal partial class ObjectEditor : ChannelWidget<object>
    {
        public string? Label { private get; set; } = string.Empty;

        internal override void UpdateCore(Context context)
        {
            var value = Update();
            if (ImGuiUtils.InputObject(Context.GetLabel(this, Label), ref value))
                Value = value;
        }
    }
}
