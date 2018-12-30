using Bam.Net.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalEntryValueLoader : IJournalEntryValueLoader
    {
        public JournalEntryValueLoader(ILogger logger = null)
        {
            Values = new ConcurrentDictionary<string, string>();
            Logger = logger ?? Log.Default;
        }

        public ILogger Logger { get; set; }
        public bool Reload { get; set; }

        public ConcurrentDictionary<string, string> Values { get; set; }

        public virtual string LoadValue(string filePath)
        {
            if (Values.ContainsKey(filePath))
            {
                return Values[filePath];
            }
            return ReadFile(filePath);
        }

        public virtual string ReadFile(string filePath)
        {
            string result = string.Empty;
            bool readFromFile = false;
            if (!Values.ContainsKey(filePath))
            {
                if (File.Exists(filePath))
                {
                    result = SetValue(filePath, File.ReadAllText(filePath));
                    readFromFile = true;
                }
                else
                {
                    WarnFileNotFound(filePath);
                }
            }

            if (Reload && !readFromFile)
            {
                if (File.Exists(filePath))
                {
                    result = SetValue(filePath, File.ReadAllText(filePath));
                    Reload = false;
                }
                else
                {
                    WarnFileNotFound(filePath);
                }
            }
            return result;
        }

        protected virtual string SetValue(string filePath, string value)
        {
            if (!Values.ContainsKey(filePath))
            {
                Values.TryAdd(filePath, value);
            }
            else
            {
                Values[filePath] = value;
            }
            return value;
        }

        private void WarnFileNotFound(string filePath)
        {
            Logger.AddEntry("{0}.{1}: File not found: {2}", LogEventType.Warning, this.GetType().Name, nameof(LoadValue), filePath);
        }
    }
}
