namespace Bam.Net.Presentation.Html
{
    public class TagDescriptor
    {
        private string _tagName;

        public string TagName
        {
            get => _tagName;
            set => _tagName = value?.Trim()?.PascalCase();
        }

        public string TagNameLowercased => TagName.ToLowerInvariant();

        public string Description { get; set; }
    }
}