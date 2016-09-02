using Bam.Net.Logging;

namespace Bam.Net.CoreServices
{
    public interface IDatabaseProvider
    {
        ILogger Logger { get; set; }
        void SetDatabases(params object[] instances);
    }
}