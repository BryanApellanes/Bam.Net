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
            EmailTemplateDirectoryPath = ".\\EmailTemplates";
            EmailComposerType = typeof(NamedFormatEmailComposer).AssemblyQualifiedName;
            ApplicationNameResolverType = typeof(DefaultConfigurationApplicationNameProvider).AssemblyQualifiedName;
            SmtpSettingsVaultPath = ".\\SmtpSettings.vault.sqlite";
            ApplicationName = DefaultConfiguration.DefaultApplicationName;
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
        public string ApplicationName { get; set; }
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

        /// <summary>
        /// If true don't log failure to locate EmailComposer and 
        /// ApplicationNameProvider by type.
        /// </summary>
        public bool SuppressMessages
        {
            get;
            set;
        }

        private void SetEmailComposer(UserManager mgr, ILogger logger)
        {
            Type emailComposerType = Type.GetType(EmailComposerType);
            Type defaultEmailComposerType = typeof(NamedFormatEmailComposer);
            if (emailComposerType == null)
            {
                emailComposerType = defaultEmailComposerType;
                if (!SuppressMessages)
                {
                    logger.AddEntry("Specified EmailComposerType was not found, will use {0} instead: {1}", LogEventType.Warning, typeof(NamedFormatEmailComposer).Name, EmailComposerType);
                }
            }

            EmailComposer composer = emailComposerType.Construct<EmailComposer>();
            composer.TemplateDirectory = new DirectoryInfo(EmailTemplateDirectoryPath);
            mgr.EmailComposer = composer;

            mgr.InitializeConfirmationEmail();
            mgr.InitializePasswordResetEmail();
        }

        private void SetAppNameProvider(UserManager mgr, ILogger logger)
        {
            if (string.IsNullOrEmpty(ApplicationName) || ApplicationName.Equals(DefaultConfiguration.DefaultApplicationName))
            {
                Type appNameResolverType = Type.GetType(ApplicationNameResolverType);
                Type defaultAppNameProvider = typeof(DefaultConfigurationApplicationNameProvider);
                if (appNameResolverType == null)
                {
                    appNameResolverType = defaultAppNameProvider;
                    if (!SuppressMessages)
                    {
                        logger.AddEntry("Specified ApplicationNameResolverType\r\n\r\n\t{0}\r\n\r\nwas not found, will use {1} instead", LogEventType.Warning, ApplicationNameResolverType, defaultAppNameProvider.FullName);
                    }
                }
                mgr.ApplicationNameProvider = appNameResolverType.Construct<IApplicationNameProvider>();
            }
            else
            {
                mgr.ApplicationNameProvider = new StaticApplicationNameProvider(ApplicationName);
            }
        }
    }
}
