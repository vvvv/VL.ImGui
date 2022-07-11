namespace VL.ImGui.Widgets
{
    internal abstract class ChannelWidget<T> : Widget
    {
        Channel<T>? channel;
        Channel<T> fallbackChannel = new Channel<T>();

        public Channel<T> Channel 
        {
            get => channel ?? fallbackChannel;
            set
            {
                channel = value;
            }
        }

        protected T value;
        public T Value
        {
            get => value;
            protected set
            {
                this.value = value;
                Channel?.OnNext(value);
            }
        }

        protected T Update()
        {
            value = Channel.Value;
            return value;
        }
    }
}
