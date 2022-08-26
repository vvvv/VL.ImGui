using System.Reactive.Subjects;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class ObjectEditor : Widget
    {
        object? _value;

        public object? Value
        {
            get => ObservableValue.Value;
            set
            {
                if (!Equals(value, _value))
                {
                    _value = value;
                    ObservableValue.OnNext(value);
                }
            }
        }

        public BehaviorSubject<object?> ObservableValue { get; } = new BehaviorSubject<object?>(null);

        internal override void UpdateCore(Context context)
        {
            var value = ObservableValue.Value;
            if (ImGuiUtils.InputObject(string.Empty, ref value))
                ObservableValue.OnNext(value);
        }
    }
}
