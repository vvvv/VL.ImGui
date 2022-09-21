using ImGuiNET;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets.Primitives
{
    internal abstract class PrimitiveWidget : Widget
    {
        [Pin(Priority = 9)] // Before Style
        public DrawList DrawList { protected get; set; }

        internal override sealed void UpdateCore(Context context)
        {
            var drawList = DrawList switch
            {
                DrawList.Window => ImGuiNET.ImGui.GetWindowDrawList(),
                DrawList.Foreground => ImGuiNET.ImGui.GetForegroundDrawList(),
                DrawList.Background => ImGuiNET.ImGui.GetBackgroundDrawList(),
                _ => throw new NotImplementedException()
            };

            // TODO: All points are drawn in the main viewport. In order to have them drawn inside the window without having to transform them manually
            // we should look into the drawList.AddCallback(..., ...) method. It should allow us to modify the transformation matrix and clipping rects.

            var offset = DrawList == DrawList.Window ? ImGuiNET.ImGui.GetCursorScreenPos() : default;
            Draw(context, in drawList, in offset);
        }

        protected abstract void Draw(Context context, in ImDrawListPtr drawList, in System.Numerics.Vector2 offset);
    }
}
