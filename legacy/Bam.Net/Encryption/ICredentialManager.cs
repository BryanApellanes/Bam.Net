namespace Bam.Net.Encryption
{
    /// <summary>
    /// Defines a read/write interface to credential management.
    /// </summary>
    /// <seealso cref="Bam.Net.Encryption.ICredentialProvider" />
    /// <seealso cref="Bam.Net.Encryption.INamedCredentialProvider" />
    /// <seealso cref="Bam.Net.Encryption.IServiceCredentialProvider" />
    public interface ICredentialManager: ICredentialProvider, INamedCredentialProvider, IServiceCredentialProvider
    {
        void SetPassword(string password);
        void SetPasswordFor(string targetIdentifier, string password);
        void SetPasswordFor(string machineName, string serviceName, string password);
        void SetUserName(string userName);
        void SetUserNameFor(string targetIdentifier, string userName);
        void SetUserNameFor(string machineName, string serviceName, string userName);
    }
}