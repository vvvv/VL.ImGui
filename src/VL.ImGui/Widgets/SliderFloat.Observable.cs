using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    internal class SliderFloat : Widget, IDisposable
    {
        private readonly BehaviorSubject<float> _value = new BehaviorSubject<float>(default);
        private readonly SerialDisposable _subscription = new SerialDisposable();
        private object _lastValue;

        public string Label { get; set; }

        public IObservable<float> Value
        {
            get => _value;
            set
            {
                if (_lastValue != value)
                {
                    _lastValue = value;
                    _subscription.Disposable = value?.Subscribe(_value);
                }
            }
        }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        public string Format { private get; set; } = null;

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        //public bool IsModified { get; private set; }

        internal override void Update(Context context)
        {
            var value = _value.Value;
            if (ImGuiNET.ImGui.SliderFloat(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                _value.OnNext(value);
        }

        internal override void Reset()
        {
            //IsModified = false;
        }

        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {
            return factory.NewNodeDescription("Slider (Float)", "ImGui", fragmented: true, _c =>
            {
                var _w = new SliderFloat();
                var _inputs = new[]
                {
                    _c.Input("Label", _w.Label),
                    _c.Input("Value", _w.Value),
                    _c.Input("Min", _w.Min),
                    _c.Input("Max", _w.Max),
                    _c.Input("Format", _w.Format, "Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. \"%.3f\" -> 1.234; \"%5.2f secs\" -> 01.23 secs; \"Biscuit: %.0f\" -> Biscuit: 1; etc."),
                    _c.Input("Flags", _w.Flags),
                };
                var _outputs = new[]
                {
                    _c.Output<Widget>("Output"),
                    _c.Output("Value", _w.Value),
                    //_c.Output("Is Modified", _w.IsModified)
                };
                return _c.NewNode(_inputs, _outputs, c =>
                {
                    var s = new SliderFloat();
                    var inputs = new IVLPin[]
                    {
                        c.Input(v => s.Label = v, s.Label),
                        c.Input(v => s.Value = v, s.Value),
                        c.Input(v => s.Min = v, s.Min),
                        c.Input(v => s.Max = v, s.Max),
                        c.Input(v => s.Format = v, s.Format),
                        c.Input(v => s.Flags = v, s.Flags)
                    };
                    var outputs = new IVLPin[]
                    {
                        c.Output(() => s),
                        c.Output(() => s.Value),
                        //c.Output(() => s.IsModified)
                    };
                    return c.Node(inputs, outputs);
                });
            });
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
