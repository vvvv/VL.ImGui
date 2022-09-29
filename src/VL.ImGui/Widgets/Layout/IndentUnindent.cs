﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets", GenerateImmediate = false, IsStylable = false)]
    internal partial class IndentUnindent : Widget
    {
        public Widget? Input { private get; set; }

        public float Value { private get; set; } = 0.5f;

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.Indent(Value.FromHectoToImGui());
            try
            {
                context.Update(Input);
            }
            finally
            {
                ImGuiNET.ImGui.Unindent(Value.FromHectoToImGui());
            }
        }
    }
}
