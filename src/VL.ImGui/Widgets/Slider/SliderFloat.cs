using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Slider (Float)")]
    internal partial class SliderFloat : Widget
    {
        public string? Label { get; set; }

        public float Min { private get; set; } = 0f;

        public float Max { private get; set; } = 1f;

        [Documentation(@"Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "" % .3f"" -> 1.234; "" % 5.2f secs"" -> 01.23 secs; ""Biscuit: % .0f"" -> Biscuit: 1; etc.")]
        public string? Format { private get; set; }

        public ImGuiNET.ImGuiSliderFlags Flags { private get; set; }

        public BehaviorSubject<float> Value { get; } = new BehaviorSubject<float>(0f);

        internal override void Update(Context context)
        {
            var value = Value.Value;
            if (ImGuiNET.ImGui.SliderFloat(Label ?? string.Empty, ref value, Min, Max, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value.OnNext(value);
        }

        //internal static IVLNodeDescription GetNodeDescription_Immediate(IVLNodeDescriptionFactory factory)
        //{
        //    return factory.NewNodeDescription("Slider (Float)", "ImGui.Immediate", fragmented: false, invalidated: default, init: _c =>
        //    {
        //        var _w = new SliderFloat();
        //        var _inputs = new IVLPinDescription[]
        //        {
        //            _c.Input("Context", default(Context)),
        //            _c.Input("Label", _w.Label),
        //            _c.Input("Min", _w.Min),
        //            _c.Input("Max", _w.Max),
        //            _c.Input("Format", _w.Format, summary: "Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. \" % .3f\" -> 1.234; \" % 5.2f secs\" -> 01.23 secs; \"Biscuit: % .0f\" -> Biscuit: 1; etc."),
        //            _c.Input("Flags", _w.Flags),
        //        };
        //        var _outputs = new[]
        //        {
        //            _c.Output<Context>("Context"),
        //            _c.Output("Value", _w.Value),
        //        };
        //        return _c.NewNode(_inputs, _outputs, c =>
        //        {
        //            var s = new SliderFloat();
        //            var ctx = default(Context);
        //            var inputs = new IVLPin[]
        //            {
        //                c.Input(v => ctx = v, ctx),
        //                c.Input(v => s.Label = v, s.Label),
        //                c.Input(v => s.Min = v, s.Min),
        //                c.Input(v => s.Max = v, s.Max),
        //                c.Input(v => s.Format = v, s.Format),
        //                c.Input(v => s.Flags = v, s.Flags),
        //            };
        //            var outputs = new IVLPin[]
        //            {
        //                c.Output(() => ctx),
        //                c.Output(() => s.Value),
        //            };
        //        return c.Node(inputs, outputs, () => { if (ctx != null) s.Update(ctx); });
        //        }, summary: "");
        //    }, tags: "");
        //}
    }
}
