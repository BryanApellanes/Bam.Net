/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using Naizari.Configuration;
using Naizari.Helpers;
using Naizari.Logging.EventRegistration;

namespace Naizari.Logging
{
    public class Log: IHasRequiredProperties
    {
        public const string LoggingNamespace = "Naizari.Logging.";
        public const string NotificationFrom = "logs@noreply.cxm";
        public const string SubjectPrefix = "LOG NOTIFICATION";

        List<string> requiredProperties = new List<string>();

        XmlLogger fatalLogger;

        internal Log()
        {
            requiredProperties.Add("LogType");
            requiredProperties.Add("ApplicationName");
            DefaultConfiguration.SetProperties(this, false);
        }

        internal void Initialize()
        {
            Initialize(true);
        }

        internal void Initialize(bool throwIfRequiredPropertiesNotFoundInDefaultConfig)
        {
            if (currentLog == null || currentLog.IsNull)
            {
                DefaultConfiguration.SetProperties(this, false);
                currentLog = CreateLogger(this.logType);
                //if (this.logType == Naizari.Logging.LogType.MSSql)
                //{
                //    EventManager.Current.EventStoreType = EventStoreTypes.MSSql.ToString();
                //}
                //else
                //{
                //    EventManager.Current.EventStoreType = EventStoreTypes.SQLite.ToString();
                //}

                currentLog.Initialize();
                DefaultConfiguration.SetPropertiesByProxy(currentLog, this, throwIfRequiredPropertiesNotFoundInDefaultConfig);
            }

            SingletonHelper.SetApplicationProvider<ILogger>(currentLog, true);
            WireNotificationEvents(currentLog);
        }

        public void LogFatal(string message)
        {
            if (fatalLogger == null)
            {
                fatalLogger = (XmlLogger)CreateLogger(Naizari.Logging.LogType.Xml);
                string folder = FsUtil.GetCurrentUserAppDataFolder();
                fatalLogger.LogLocation = folder;
                fatalLogger.LogName = FsUtil.GetAssemblyName(this.GetType().Assembly);
            }

            fatalLogger.AddEntry(message);
        }

        List<ILogger> wired = new List<ILogger>();
        public void WireNotificationEvents(ILogger logger)
        {
            if (!wired.Contains(logger))
            {
                logger.FatalEventOccurred += new LogEntryAddedListener(Notify);
                logger.ErrorEventOccurred += new LogEntryAddedListener(Notify);
                logger.InfoEventOccurred += new LogEntryAddedListener(Notify);
                logger.WarnEventOccurred += new LogEntryAddedListener(Notify);
                wired.Add(logger);
            }
        }

        public static ILogger CreateLogger(LogType providerType)
        {
            if (providerType == Naizari.Logging.LogType.Null)
                return new NullLogger();

            Type loggerType = Type.GetType(LoggingNamespace + providerType.ToString() + "Logger");

            ConstructorInfo ctor = loggerType.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new InvalidOperationException("The specified LogType provider doesn't have a parameterless constructor.");
            }

            ILogger newLogger = (ILogger)ctor.Invoke(null);

            return newLogger;
        }

        volatile static Log logManager;

        private static ILogger GetLogger()
        {
            ILogger logger = SingletonHelper.GetApplicationProvider<ILogger>();
            if (logger != null)
            {
                return logger;
            }
            else
            {
                InitializeLogManager();

                return currentLog;
            }

        }

        private static void InitializeLogManager()
        {
            if (logManager == null)
            {
                logManager = new Log();
                logManager.Initialize(false);
            }

            logManager.Initialize();
        }

        volatile static ILogger currentLog;

        /// <summary>
        /// Gets the current ILogger implementation as configured in the default configuration file
        /// </summary>
        public static ILogger Default
        {
            get 
            {
                return GetLogger();
            }

            set
            {
                currentLog = value;
                InitializeLogManager();
            }
        }

        static Dictionary<LogType, ILogger> loggers = new Dictionary<LogType, ILogger>();

        static object currentManagerLock = new object();
        public static Log Current
        {
            get
            {
                lock (currentManagerLock)
                {
                    if (logManager == null)
                        logManager = new Log();
                }

                return logManager;
            }
        }

        /// <summary>
        /// Gets an ILogger implementation of the specified LoggingProviderType.
        /// The properties required by 
        /// the specified LoggingProviderType should be set in the configuration
        /// file on the LogManager class 
        /// (example, &lt;add key=\"LogManager.[propertyName]\" value=\"[validValue]\" /&gt;)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger this[LogType type]
        {
            get
            {
                lock (loggers)
                {
                    if (!loggers.ContainsKey(type))
                        loggers.Add(type, CreateLogger(type));
                }
                return loggers[type];
            }
        }

        public string IntegratedSecurity { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        static string notifyRecipients;
        public string NotifyRecipients 
        {
            get { return notifyRecipients; }
            set { notifyRecipients = value; }
        }

        public string NotifyOnFatal { get; set; }
        public string NotifyOnError { get; set; }
        public string NotifyOnInfo { get; set; }
        public string NotifyOnWarn { get; set; }

        static string smtpHost;
        public string SmtpHost 
        {
            get { return smtpHost; }

            set { smtpHost = value; }
        }

        public static void SendNotification(string messageBody)
        {
            SendNotification(messageBody, string.Format("{0}: Custom Log Message", SubjectPrefix), notifyRecipients);
        }

        public static void SendNotification(string messageBody, string subject, string commaOrSemiColonSeparatedRecipientEmails)
        {
            if (!string.IsNullOrEmpty(notifyRecipients))
            {
                string[] recipientList = commaOrSemiColonSeparatedRecipientEmails.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
                MailMessage message = new MailMessage();
                foreach (string recipient in recipientList)
                {
                    if (!recipient.Contains("@"))
                        Default.AddEntry("An invalid email address was specified", LogEventType.Warning);
                    else
                        message.To.Add(new MailAddress(recipient));
                }

                if (message.To.Count > 0)
                {
                    message.From = new MailAddress(NotificationFrom);
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = string.Format("<font face='arial'>{0}</font>", messageBody.Replace("\r\n", "<br />"));

                    SmtpClient client = new SmtpClient(smtpHost);
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        Default.AddEntry("An error occurred sending custom log message", ex);
                    }
                }
            }
        }

        private void Notify(string applicationName, LogEvent logEvent)
        {
            try
            {
                string csvList = string.Empty;
                switch (logEvent.Severity)
                {
                    case LogEventType.None:
                        break;
                    case LogEventType.Information:
                        csvList = NotifyOnInfo;
                        break;
                    case LogEventType.Warning:
                        csvList = NotifyOnWarn;
                        break;
                    case LogEventType.Error:
                        csvList = NotifyOnError;
                        break;
                    case LogEventType.Fatal:
                        csvList = NotifyOnFatal;
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(csvList))
                {
                    string[] notifyList = csvList.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (notifyList.Length > 0)
                    {
                        MailMessage emailMessage = new MailMessage();
                        emailMessage.From = new MailAddress(NotificationFrom);
                        emailMessage.Body = CreateMailBody(applicationName, logEvent);
                        emailMessage.Subject = string.Format("{0}: {1}", SubjectPrefix, logEvent.Severity.ToString());
                        emailMessage.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient(SmtpHost);

                        foreach (string email in notifyList)
                        {
                            string emailAddr = email.Trim();
                            if (!emailAddr.Contains("@"))
                            {
                                Default.AddEntry("An invalid email address ({0}) was entered for {1} log notification.", new string[] { emailAddr, logEvent.Severity.ToString() });
                            }
                            else
                            {
                                emailMessage.To.Add(new MailAddress(emailAddr));
                            }
                        }

                        if (emailMessage.To.Count > 0)
                        {
                            try
                            {
                                client.Send(emailMessage);
                            }
                            catch (Exception ex)
                            {
                                Default.AddEntry("An error occurred sending {0} log notification.", ex, new string[] { logEvent.Severity.ToString() });
                            }
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                // keep the app from locking up or crashing if logging isn't configured
            }
        }

        private string CreateMailBody(string applicationName, LogEvent logEvent)
        {
            string ret = string.Format("<font face='arial'>An event with severity level <b>{1}</b> occurred in <b>{0}</b><br /><br />", applicationName, logEvent.Severity.ToString());
            ret += string.Format("<b>Time</b>: {0}<br />", logEvent.TimeOccurred.ToLocalTime().ToString());
            ret += string.Format("<b>Computer</b>: {0}<br />", logEvent.Computer);
            ret += string.Format("<b>Message</b>:<br />{0}<br /></font>", logEvent.Message.Replace("\r\n", "<br />"));
            return ret;
        }

        LogType logType;
        public string LogType 
        {
            get
            {
                if (logType == Logging.LogType.Null)
                {
                    return "";
                }

                return logType.ToString();
            }
            set { logType = (LogType)Enum.Parse(typeof(LogType), value); }
        }

        string applicationName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(this.applicationName))
                    return "Unknown";

                return this.applicationName;
            }
            set
            {
                this.applicationName = value;
            }
        }


        #region IHasRequiredProperties Members

        public string[] RequiredProperties
        {
            get { return requiredProperties.ToArray(); }
        }

        #endregion
    }
}
