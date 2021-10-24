namespace Bam.Net.Presentation.Html
{
    public class ScriptTag: Tag
    {
        public ScriptTag(string scriptPath, string type = "text/javascript") : base("script")
        {
            SetAttribute("type", type);
            SetAttribute("src", scriptPath);
        }

        public static Tag For(string scriptPath)
        {
            return new ScriptTag(scriptPath);
        }
    }
}