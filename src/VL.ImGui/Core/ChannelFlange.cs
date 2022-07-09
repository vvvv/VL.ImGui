using System.Reactive.Subjects;

namespace VL.ImGui
{
    class ChannelFlange<T>
    {
        T value;
        IObservable<T>? channel;

        internal T Update(IObservable<T>? channel)
        {
            this.channel = channel;
            if (channel is BehaviorSubject<T> s)
                value = s.Value;
            if (channel is Channel<T> c)
                value = c.Value;
            return value;
        }

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                if (channel is IObserver<T> o)
                    o.OnNext(value);
            }
        }
    }

}
