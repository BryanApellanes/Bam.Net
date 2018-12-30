namespace Bam.Net.Logging
{
    public interface ILogEventCommitter
    {
        void CommitLogEvent(LogEvent logEvent);
    }
}