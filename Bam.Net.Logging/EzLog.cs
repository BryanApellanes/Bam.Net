using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    /// <summary>
    /// A convenience entry point to log information level events
    /// </summary>
    public static class Info
    {
        /// <summary>
        /// Log an Information level event
        /// </summary>
        /// <param name="messageSignature"></param>
        /// <param name="args"></param>
        public static void Log(string messageSignature, params object[] args)
        {
            Logging.Log.Info(messageSignature, args);
        }
    }

    /// <summary>
    /// A convenience entry point to log Warning level events
    /// </summary>
    public static class Warn
    {
        /// <summary>
        /// Log a Warning level event
        /// </summary>
        /// <param name="messageSignature"></param>
        /// <param name="args"></param>
        public static void Log(string messageSignature, params object[] args)
        {
            Logging.Log.Warn(messageSignature, args);
        }
    }

    /// <summary>
    /// A convenience entry point to log Error level events
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// Log an Error level event
        /// </summary>
        /// <param name="messageSignature"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public static void Log(string messageSignature, Exception ex, params object[] args)
        {
            Logging.Log.Error(messageSignature, ex, args);
        }
        /// <summary>
        /// Log an Error level event
        /// </summary>
        /// <param name="messageSignature"></param>
        /// <param name="args"></param>
        public static void Log(string messageSignature, params object[] args)
        {
            Logging.Log.Error(messageSignature, args);
        }
    }
}
