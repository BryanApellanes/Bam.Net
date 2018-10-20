namespace Bam.Net.Services.DataReplication
{
    public interface ISequenceProvider
    {
        ulong Next();
    }
}