using VL.ImGui.Widgets;

namespace VL.ImGui
{
    internal abstract class StylableWidget : Widget
    {
        [Pin(Priority = 10)]
        public IStyle? Style { set; protected get; }

        internal override void Update(Context context)
        {
            try
            {
                Style?.Set();
                UpdateCore(context);
            }
            finally
            {
                Style?.Reset();
            }
        }
    }

    internal abstract class StylableChannelWidget<T> : ChannelWidget<T>
    {
        [Pin(Priority = 10)]
        public IStyle? Style { set; protected get; }

        internal override void Update(Context context)
        {
            try
            {
                Style?.Set();
                UpdateCore(context);
            }
            finally
            {
                Style?.Reset();
            }
        }
    }
}
