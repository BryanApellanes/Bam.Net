/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using System.IO;

namespace Bam.Net.UserAccounts
{
    public class UserManagerConfig
    {
        public UserManagerConfig()
        {
            this.EmailTemplateDirectoryPath = ".\\EmailTemplates";
            this.EmailComposerType = typeof(NamedFormatEmailComposer).AssemblyQualifiedName;
            this.ApplicationNameResolverType = typeof(DefaultConfigurationApplicationNameProvider).AssemblyQualifiedName;
            this.SmtpSettingsVaultPath = ".\\SmtpSettings.vault.sqlite";
        }

        public UserManager Create(ILogger logger = null)
        {
            if (logger == null)
            {
                logger = Log.Default;
            }

            UserManager mgr = new UserManager();
            SetAppNameProvider(mgr, logger);
            SetEmailComposer(mgr, logger);

            if (!string.IsNullOrEmpty(SmtpSettingsVaultPath))
            {
                mgr.SmtpSettingsVaultPath = SmtpSettingsVaultPath;
            }

            mgr.Subscribe(logger);
            return mgr;
        }
        
        public string ApplicationNameResolverType
        {
            get;
            set;
        }

        public string EmailComposerType
        {
            get;
            set;
        }

        public string EmailTemplateDirectoryPath
        {
            get;
            set;
        }

        public string SmtpSettingsVaultPath
        {
            get;
            set;
        }

        private void SetEmailComposer(UserManager mgr, ILogger logger)
        {
            Type emailComposerType = Type.GetType(EmailComposerType);
            if (emailComposerType == null)
            {
                logger.AddEntry("Specified EmailComposerType was not found, will use {0} instead: {1}", LogEventType.Warning, typeof(NamedFormatEmailComposer).Name, EmailComposerType);
            }
            else
            {
                EmailComposer composer = emailComposerType.Construct<EmailComposer>();
                composer.TemplateDirectory = new DirectoryInfo(EmailTemplateDirectoryPath);
                mgr.EmailComposer = composer;
                
                mgr.InitializeConfirmationEmail();
                mgr.InitializePasswordResetEmail();
            }
        }

        private void SetAppNameProvider(UserManager mgr, ILogger logger)
        {
            Type appNameResolverType = Type.GetType(ApplicationNameResolverType);
            Type defaultAppNameProvider = typeof(DefaultConfigurationApplicationNameProvider);
            if (appNameResolverType == null)
            {
                appNameResolverType = defaultAppNameProvider;
                logger.AddEntry("Specified ApplicationNameResolverType\r\n\r\n\t{0}\r\n\r\nwas not found, will use {1} instead", LogEventType.Warning, ApplicationNameResolverType, defaultAppNameProvider.FullName);
            }

            mgr.ApplicationNameProvider = appNameResolverType.Construct<IApplicationNameProvider>();            
        }
    }
}
