namespace Bam.Net.Presentation.Html
{
    public class StyleSheetLinkTag : Tag
    {
        public StyleSheetLinkTag(string cssPath) : base("link")
        {
            SetAttribute("rel", "stylesheet");
            SetAttribute("href", cssPath);
        }

        public override string Render(bool indented = false)
        {
            return RenderStartTag();
        }

        public static Tag For(string cssPath)
        {
            return new StyleSheetLinkTag(cssPath);
        }
    }
}