namespace Bam.Net.Server.Renderers
{
    public interface ICompiledTemplate
    {
        string Compiled { get; set; }
        string Name { get; set; }
        string Source { get; set; }
        string SourceFilePath { get; set; }
        string SourceHash { get; }
    }
}