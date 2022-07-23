using System.Reactive.Subjects;

namespace VL.ImGui
{
    class ChannelFlange<T> where T: struct
    {
        T value;
        Channel<T>? channel;

        public ChannelFlange(T initialValue)
        {
            value = initialValue;
        }

        internal T Update(Channel<T>? channel)
        {
            this.channel = channel;
            if (channel != null)
                value = channel.Value;
            return value;
        }
        internal T Update(Channel<T>? channel, out bool hasChanged)
        {
            hasChanged = false;
            this.channel = channel;
            hasChanged = CopyFromUpstream();
            return value;
        }

        bool CopyFromUpstream()
        {
            if (channel != null && !channel.Value.Equals(value))
            {
                value = channel.Value;
                return true;
            }
            return false;
        }

        public T Value
        {
            get => value;
            set
            {
                if (!value.Equals(this.value))
                {
                    this.value = value;
                    channel?.OnNext(value);
                }
            }
        }
    }

}
