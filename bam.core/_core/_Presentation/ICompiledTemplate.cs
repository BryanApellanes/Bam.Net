namespace Bam.Net.Presentation
{
    public interface ICompiledTemplate
    {
        string UnescapedCompiled { get; }
        string Compiled { get; set; }
        string Name { get; set; }
        string Source { get; set; }
        string SourceFilePath { get; set; }
        string SourceHash { get; }
    }
}