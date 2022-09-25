using VL.Lib.Reactive;

namespace VL.ImGui.Widgets
{
    internal abstract class ChannelWidget<T> : Widget//, IDisposable
    {
        private Channel<T>? channel;
        private T? value;
        //Channel<T> fallbackChannel = new Channel<T>();

        public Channel<T>? Channel 
        {
            protected get => channel; // ?? fallbackChannel;
            set { channel = value; }
        }

        public bool Bang 
        { 
            get; 
            private set; 
        }

        public T? Value
        {
            get => value;
            protected set
            {
                this.value = value;
                Bang = true;
                if (Channel != null)
                    Channel.Value = value!;
            }
        }

        protected T? Update()
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
