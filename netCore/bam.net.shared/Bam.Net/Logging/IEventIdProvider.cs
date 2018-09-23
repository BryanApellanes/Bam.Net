namespace Bam.Net.Logging
{
    public interface IEventIdProvider
    {
        int GetEventId(string applicationName, string messageSignature);
    }
}