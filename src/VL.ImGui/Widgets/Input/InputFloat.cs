namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (Float)", Category = "ImGui.Widgets", Tags = "number, updown")]
    internal partial class InputFloat : ChannelWidget<float>
    {

        public string? Label { get; set; }

        public float Step { private get; set; } = 0f;

        public float StepFast { private get; set; } = 0f;

        /// <summary>
        /// Adjust format string to decorate the value with a prefix, a suffix, or adapt the editing and display precision e.g. "%.3f" -> 1.234; "%5.2f secs" -> 01.23 secs; "Biscuit: % .0f" -> Biscuit: 1; etc.
        /// </summary>
        public string? Format { private get; set; } = "%.3f";

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        internal override void UpdateCore(Context context)
        {
            var value = Update();
            if (ImGuiNET.ImGui.InputFloat(Label ?? string.Empty, ref value, Step, StepFast, string.IsNullOrWhiteSpace(Format) ? null : Format, Flags))
                Value = value;
        }
    }
}
