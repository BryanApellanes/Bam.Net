using Bam.Net.Encryption;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class EncryptedSearchIndexJournalEntryValueLoader : IJournalEntryValueLoader
    {        
        public EncryptedSearchIndexJournalEntryValueLoader(IKeyValueStore publicKeyValueStore, KeySet keyset = null, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            KeySet = keyset ?? KeySet.ForApplication;
            PrivateKeyValueStore = new FileSystemKeyValueStore(Logger);
            EncryptedSearchIndex = new EncryptedSearchIndex(publicKeyValueStore, PrivateKeyValueStore, KeySet, logger);
        }

        public ILogger Logger { get; set; }
        public KeySet KeySet { get; }
        public FileSystemKeyValueStore PrivateKeyValueStore { get; internal set; }
        public EncryptedSearchIndex EncryptedSearchIndex { get; }

        EncryptedSearchIndexJournalEntryValueFlusher _flusher;
        /// <summary>
        /// Gets a flusher configured to work with this loader.
        /// </summary>
        /// <returns></returns>
        public EncryptedSearchIndexJournalEntryValueFlusher GetFlusher()
        {
            if(_flusher == null)
            {
                _flusher = new EncryptedSearchIndexJournalEntryValueFlusher(EncryptedSearchIndex.PublicKeyValueStore, KeySet, Logger);
            }
            return _flusher;
        }

        public string LoadValue(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            string key = Path.GetFileNameWithoutExtension(file.Name);
            return EncryptedSearchIndex.Retrieve(key);
        }
    }
}
