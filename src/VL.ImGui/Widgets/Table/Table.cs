using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode]
    internal sealed partial class Table : Widget
    {
        public Widget? Content { get; set; }

        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public float InnerWidth { private get; set; }

        public bool IsVisible { get; private set; }

        public int Columns { private get; set; }

        public ImGuiNET.ImGuiTableFlags Flags { private get; set; }


        internal override void Update(Context context)
        {

            IsVisible = ImGuiNET.ImGui.BeginTable(Label ?? string.Empty, Columns, Flags, Size.ToImGui(), InnerWidth);
            
            try
            {
                if (IsVisible)
                {
                    context.Update(Content);
                }
            }
            finally
            {
                ImGuiNET.ImGui.EndTable();
            }
        }
    }
}
