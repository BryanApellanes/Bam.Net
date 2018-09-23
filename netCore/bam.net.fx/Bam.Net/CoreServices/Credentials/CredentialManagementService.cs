using Bam.Net.Encryption;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    [ApiKeyRequired]
    [Proxy("credentialSvc")]
    [ServiceSubdomain("creds")]
    public class CredentialManagementService : ApplicationProxyableService, ICredentialManager
    {
        public CredentialManagementService()
        {
            CredentialManager = CredentialManager.Local;
        }

        protected CredentialManager CredentialManager { get; }

        public override object Clone()
        {
            CredentialManagementService svc = new CredentialManagementService();
            svc.CopyProperties(this);
            svc.CopyEventHandlers(this);
            return svc;
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public bool SetSmtpSettings(SmtpSettings smtpSettings)
        {
            try
            {
                NotificationService.SetDefaultSmtpSettings(smtpSettings);
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Failed to set smtp settings: {0}", ex, ex.Message);
                return false;
            }
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public string GetPassword()
        {
            return CredentialManager.GetPasswordFor(GetPrefixedValue(ClientApplicationName));
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public string GetPasswordFor(string targetIdentifier)
        {
            return CredentialManager.GetPasswordFor(GetPrefixedValue(targetIdentifier));
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public string GetPasswordFor(string machineName, string serviceName)
        {
            return CredentialManager.GetPasswordFor(GetPrefixedValue(machineName), GetPrefixedValue(serviceName));
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public string GetUserName()
        {
            return CredentialManager.GetUserNameFor(GetPrefixedValue(ClientApplicationName));
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public string GetUserNameFor(string targetIdentifier)
        {
            return CredentialManager.GetUserNameFor(GetPrefixedValue(targetIdentifier));
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public string GetUserNameFor(string machineName, string serviceName)
        {
            return CredentialManager.GetUserNameFor(GetPrefixedValue(machineName), GetPrefixedValue(serviceName));
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public void SetPassword(string password)
        {
            CredentialManager.SetPasswordFor(GetPrefixedValue(ClientApplicationName), password);
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public void SetPasswordFor(string targetIdentifier, string password)
        {
            CredentialManager.SetPasswordFor(GetPrefixedValue(targetIdentifier), password);
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public void SetPasswordFor(string machineName, string serviceName, string password)
        {
            CredentialManager.SetPasswordFor(GetPrefixedValue(machineName), GetPrefixedValue(serviceName), password);
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public void SetUserName(string userName)
        {
            CredentialManager.SetUserNameFor(GetPrefixedValue(ClientApplicationName), userName);
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public void SetUserNameFor(string targetIdentifier, string userName)
        {
            CredentialManager.SetUserNameFor(GetPrefixedValue(targetIdentifier), userName);
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public void SetUserNameFor(string machineName, string serviceName, string userName)
        {
            CredentialManager.SetUserNameFor(GetPrefixedValue(machineName), GetPrefixedValue(serviceName), userName);
        }

        private string GetPrefixedValue(string value)
        {
            return $"{ServerApplicationName}.{ClientApplicationName}.{value}";
        }

    }
}
