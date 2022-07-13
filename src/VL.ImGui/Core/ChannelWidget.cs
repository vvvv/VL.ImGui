namespace VL.ImGui.Widgets
{
    internal abstract class ChannelWidget<T> : Widget//, IDisposable
    {
        Channel<T>? channel;
        //Channel<T> fallbackChannel = new Channel<T>();

        public Channel<T>? Channel 
        {
            protected get => channel; // ?? fallbackChannel;
            set
            {
                channel = value;
            }
        }
        public bool Bang { private set; get; }

        protected T value;
        public T Value
        {
            get => value;
            protected set
            {
                this.value = value;
                Bang = true;
                Channel?.OnNext(value);
            }
        }

        protected T Update()
        {
            Bang = false;
            if (channel != null)
                value = channel.Value;
            return value;
        }

        //public void Dispose()
        //{
        //    fallbackChannel.Dispose();
        //}
    }
}
