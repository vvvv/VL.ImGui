﻿namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Return number of columns (value passed to BeginTable)
    /// </summary>
    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class TableGetColumnCount : Widget
    {
        public int Value { get; private set; }

        internal override void Update(Context context)
        {
            Value = ImGuiNET.ImGui.TableGetColumnCount();
        }
    }
}
