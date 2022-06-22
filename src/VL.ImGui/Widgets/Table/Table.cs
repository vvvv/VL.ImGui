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


        public IEnumerable<Widget> ColumnDescriptions { get; set; } = Enumerable.Empty<Widget>();

        public IEnumerable<Widget> Columns { get; set; } = Enumerable.Empty<Widget>();

        public string? Label { get; set; }

        public Vector2 Size { private get; set; }

        public float InnerWidth { private get; set; }

        public bool IsVisible { get; private set; }

        public bool ShowHeader { get; set; }

        public ImGuiNET.ImGuiTableFlags Flags { private get; set; }

        internal override void Update(Context context)
        {
            var count = ColumnDescriptions.Count(x => x != null);

            if (count > 0)
            {

                IsVisible = ImGuiNET.ImGui.BeginTable(Label ?? string.Empty, count, Flags, Size.ToImGui(), InnerWidth);

                try
                {
                    if (IsVisible)
                    {
                        foreach (var desc in ColumnDescriptions)
                        {
                            if (desc is null)
                                continue;
                            else
                                context.Update(desc);

                        }

                        if (ShowHeader)
                            ImGuiNET.ImGui.TableHeadersRow();

                        foreach (var col in Columns)
                        {
                            if (col is null)
                                continue;
                            else
                            {
                                ImGuiNET.ImGui.TableNextColumn();
                                context.Update(col);
                            }
                        }
                    }
                }
                finally
                {
                    ImGuiNET.ImGui.EndTable();
                }

            }
        }
    }
}
