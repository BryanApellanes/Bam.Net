using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.Messaging;

namespace Bam.Net.CoreServices
{
    public class DataSettingsSmtpSettingsProvider: SmtpSettingsProvider
    {
        public const string SmtpSettingsFileName = "bam.smtp.settings.json";
        static DataSettingsSmtpSettingsProvider()
        {
            DefaultSender = $"no-reply@{Environment.MachineName}";
        }

        public DataSettingsSmtpSettingsProvider()
        {
            DataSettings = DefaultDataDirectoryProvider.Current;
        }

        public DataSettingsSmtpSettingsProvider(SmtpSettings smtpSettings, DefaultDataDirectoryProvider dataSettings = null)
        {
            DataSettings = dataSettings ?? DefaultDataDirectoryProvider.Current;
            SmtpSettings = smtpSettings;
            SmtpSettings.Save(SmtpSettingsVault);            
        }

        static DataSettingsSmtpSettingsProvider _default;
        static object _defaultLock = new object();
        public static DataSettingsSmtpSettingsProvider Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, LoadSmtpSettings);
            }
            set
            {
                _default = value;
            }
        }

        public static string DefaultSender { get; set; }

        public DefaultDataDirectoryProvider DataSettings { get; set; }

        SmtpSettings _smtpSettings;
        public SmtpSettings SmtpSettings
        {
            get
            {
                return _smtpSettings;
            }
            set
            {
                _smtpSettings = value;
                _smtpSettings.Save(SmtpSettingsVault);
            }
        }

        public override string SmtpSettingsVaultPath
        {
            get
            {
                return DataSettings.GetSysDatabasePathFor(typeof(SmtpSettings), "CoreServices");                
            }
            set
            {
                // dissallow overwriting
            }
        }
        
        public static void SetDefaultSmtpSettings(SmtpSettings settings)
        {
            FileInfo smtpSettingsFile = new FileInfo(Path.Combine(Paths.Local, SmtpSettingsFileName));
            settings.Password = Aes.Encrypt(settings.Password);
            settings.ToJsonFile(smtpSettingsFile);
        }

        private static DataSettingsSmtpSettingsProvider LoadSmtpSettings()
        {
            FileInfo smtpSettingsFile = new FileInfo(Path.Combine(Paths.Local, SmtpSettingsFileName));
            Console.WriteLine("Trying to load smtp settings from file: {0}", smtpSettingsFile.FullName);
            if (smtpSettingsFile.Exists)
            {
                try
                {
                    SmtpSettings smtpSettings = smtpSettingsFile.FromJsonFile<SmtpSettings>();
                    smtpSettings.Password = Aes.Decrypt(smtpSettings.Password);
                    DefaultSender = smtpSettings.From;
                    return new DataSettingsSmtpSettingsProvider(smtpSettings);
                }
                catch (Exception ex)
                {
                    Log.Warn("Failed to load smtp settings file {0}: {1}", smtpSettingsFile.FullName, ex.Message);
                    Console.WriteLine("failed to load smtp settings: {0}", ex.Message);
                }
            }
            else
            {
                Log.Warn("The system smtp settings file was not present, notifications may not send correctly: {0}", smtpSettingsFile.FullName);
                Console.WriteLine("failed to load smtp settings, settings file not present: {0}", smtpSettingsFile.FullName);
            }
            return new DataSettingsSmtpSettingsProvider();
        }
    }
}
