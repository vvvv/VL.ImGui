namespace VL.ImGui
{
    internal class DocumentationAttribute : Attribute
    {
        public DocumentationAttribute(string summary)
        {
            Summary = summary;
        }

        public string Summary { get; }
    }
}