﻿using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Input (String Multiline)", Category = "ImGui.Widgets")]
    internal partial class InputTextMultiline : ChannelWidget<string>
    {

        public string? Label { get; set; }

        public int MaxLength { get; set; } = 100;

        public Vector2 Size { get; set; }

        public ImGuiNET.ImGuiInputTextFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var value = Update();
            if (ImGuiNET.ImGui.InputTextMultiline(Label ?? string.Empty, ref value, (uint)MaxLength, Size.ToImGui(), Flags))
                Value = value;
        }
    }
}
