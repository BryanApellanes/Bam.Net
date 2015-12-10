/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using Naizari.Javascript;
using Naizari.Logging;

namespace Naizari.Javascript
{
    /// <summary>
    /// Used as a client side logger to be used on 
    /// JavascriptPages.  Method names are pascal cased
    /// instead of camel so client side Javascript can
    /// standardize around javascript style naming conventions.
    /// </summary>
    [JsonProxy("jsLogger")]
    public class JsLogger
    {
        public JsLogger()
            : base()
        {
        }

        public class JsLogEntry
        {
            public JsLogEntry()
            {
                this.msgSig = string.Empty;
                this.values = new string[] { };
            }

            public string msgSig { get; set; }
            public string[] values { get; set; }
        }

        [JsonMethod(Verbs.POST)]
        public void addInfoEntry(JsLogEntry logEntry)
        {
            LogManager.CurrentLog.AddEntry(formatJsMessage(logEntry.msgSig), LogEventType.Information, logEntry.values);
        }

        private static string formatJsMessage(string message)
        {
            message = "\r\n<jsLogger>\r\n\t" + message.Replace("\r\n", "\r\n\t") + "\r\n</jsLogger>";
            return message;
        }

        [JsonMethod(Verbs.POST)]
        public void addWarnEntry(JsLogEntry logEntry)
        {
            LogManager.CurrentLog.AddEntry(formatJsMessage(logEntry.msgSig), LogEventType.Warning, logEntry.values);
        }

        [JsonMethod(Verbs.POST)]
        public void addErrorEntry(JsLogEntry logEntry)
        {
            LogManager.CurrentLog.AddEntry(formatJsMessage(logEntry.msgSig), LogEventType.Error, logEntry.values);
        }
    }
}
