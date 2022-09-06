using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Get the compiled version string e.g. '1.80 WIP' (essentially the value for IMGUI_VERSION from the compiled version of imgui.cpp)
    /// </summary>

    [GenerateNode(Category = "ImGui.Queries", IsStylable = false)]
    internal partial class GetVersion : Widget
    {

        public string? Value { get; private set; }

        internal override void UpdateCore(Context context)
        {
            Value = ImGuiNET.ImGui.GetVersion();
        }
    }
}
