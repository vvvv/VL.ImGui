namespace VL.ImGui.Widgets
{
    internal abstract class ChannelWidget<T> : Widget
    {
        public Channel<T>? Channel { get; set; }

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
            if (Channel != null)
                value = Channel.Value;
            return value;
        }
    }
}
