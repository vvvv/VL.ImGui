using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    /// <summary>
    /// Calculate unique ID (hash of whole ID stack + given parameter). e.g. if you want to query into ImGuiStorage yourself
    /// </summary>

    [GenerateNode(Category = "ImGui.Queries")]
    internal partial class GetID : Widget
    {

        public string? Label { private get; set; }

        public uint Value { get; private set; }

        internal override void Update(Context context)
        {
            if (Label != null)
                Value = ImGuiNET.ImGui.GetID(Label);
        }
    }
}
