using System.Reactive.Subjects;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal partial class Inspector : Widget
    {
        public object Value
        {
            get => ObservableValue.Value;
            set
            {
                if (!Equals(value, ObservableValue.Value))
                {
                    ObservableValue.OnNext(value);
                }
            }
        }

        public BehaviorSubject<object> ObservableValue { get; } = new BehaviorSubject<object>(null);

        internal override void Update(Context context)
        {
            var value = ObservableValue.Value;
            if (ImGuiUtils.InputObject(string.Empty, ref value))
                ObservableValue.OnNext(value);
        }
    }
}
