using System.Reactive.Subjects;

namespace VL.ImGui
{
    class ChannelFlange<T>
    {
        T value;
        Channel<T>? channel;

        internal T Update(Channel<T>? channel)
        {
            this.channel = channel;
            if (channel != null)
                value = channel.Value;
            return value;
        }

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                channel?.OnNext(value);
            }
        }
    }

}
