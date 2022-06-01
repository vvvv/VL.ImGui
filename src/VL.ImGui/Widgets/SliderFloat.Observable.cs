using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using ImGuiNET;

namespace VL.ImGui.Widgets
{
    using ImGui = ImGuiNET.ImGui;

    /// <summary>
    /// The widget provides the channel it operates on. The user can provide an optional input stream the channel will subscribe to.
    /// </summary>
    [GenerateNode(Name = "Slider (Float Observable)")]
    internal partial class SliderFloatObservable : Widget, IDisposable
    {
        private readonly SerialDisposable _subscription = new SerialDisposable();
        private IObservable<float>? _lastValueIn;

        public IObservable<float>? Value
        {
            private get => _lastValueIn; // Needed for generator
            set
            {
                if (_lastValueIn != value)
                {
                    _lastValueIn = value;
                    _subscription.Disposable = value?.Subscribe(Channel);
                }
            }
        }

        public string? Label { private get; set; }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        public string? Format { private get; set; }

        public ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<float> Channel { get; } = new BehaviorSubject<float>(0f);

        internal override void Update(Context context)
        {
            var value = Channel.Value;
            if (ImGui.SliderFloat(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Channel.OnNext(value);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
