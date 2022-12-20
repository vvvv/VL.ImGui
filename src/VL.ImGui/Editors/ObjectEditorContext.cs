using VL.Core.EditorAttributes;

namespace VL.ImGui.Editors
{
    public record ObjectEditorContext(IObjectEditorFactory Factory, IReadOnlyList<Attribute> Attributes, string? Label = null);
}
