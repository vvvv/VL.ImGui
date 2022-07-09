using System.Reactive.Subjects;
using VL.Core;

namespace VL.ImGui
{
    [Monadic(typeof(Monadic.ChannelFactory<>))]
    public class Channel<T> : IDisposable, ISubject<T>
    {
        Subject<T> Subject = new Subject<T>();
        IDisposable Subscription;

        public Channel()
        {
            Subscription = Subject.Subscribe(v => Value = v);
        }

        public void Dispose()
        {
            Subscription.Dispose();
            Subject.Dispose();
        }

        public void OnCompleted()
        {
            ((IObserver<T>)Subject).OnCompleted();
        }

        public void OnError(Exception error)
        {
            ((IObserver<T>)Subject).OnError(error);
        }

        public void OnNext(T value)
        {
            ((IObserver<T>)Subject).OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return ((IObservable<T>)Subject).Subscribe(observer);
        }

        const int maxStack = 1;
        int stack;

        public IObservable<T> Observable => Subject;

        public bool IsBusy => stack > 0;

        T value = default;
        public T Value 
        { 
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                if (stack < maxStack)
                {
                    stack++;
                    try
                    {
                        Subject.OnNext(value);
                    }
                    finally
                    {
                        stack--;
                    }
                }
            } 
        }
    }
}
