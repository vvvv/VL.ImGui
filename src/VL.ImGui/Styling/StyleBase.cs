﻿namespace VL.ImGui.Styling
{
    using ImGui = ImGuiNET.ImGui;

    internal abstract class StyleBase : IStyle
    {
        public IStyle? Input { protected get; set; }

        protected int colorCount;
        protected int valueCount;
        
        public void Set()
        {
            Input?.Set();
            colorCount = 0;
            valueCount = 0;
            SetCore();
        }

        public void Reset()
        {
            if (colorCount > 0)
                ImGui.PopStyleColor(colorCount);
            if (valueCount > 0)
                ImGui.PopStyleVar(valueCount);
            Input?.Reset();
        }

        internal abstract void SetCore();


        // not really used
        internal void Update(Context context)
        { }
    }
}
