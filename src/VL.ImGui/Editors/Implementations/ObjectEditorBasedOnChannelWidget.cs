using VL.ImGui.Widgets;

namespace VL.ImGui.Editors
{
    sealed class ObjectEditorBasedOnChannelWidget<T> : IObjectEditor
    {
        public ChannelWidget<T> Widget { get; }

        public ObjectEditorBasedOnChannelWidget(ChannelWidget<T> widget)
        {
            Widget = widget;
        }

        public void Draw(Context? context)
        {
            Widget?.Update(context);
        }
    }
}
