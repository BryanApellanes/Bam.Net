namespace Bam.Net.Data.Schema
{
    public partial class XrefInfo
    {
        protected string Render(string templateName)
        {
            return Bam.Net.Handlebars.Render(templateName, this);
        }
    }
}
