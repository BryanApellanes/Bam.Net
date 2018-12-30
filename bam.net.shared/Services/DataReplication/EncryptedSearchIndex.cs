using Bam.Net.Analytics;
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
    public class EncryptedSearchIndex
    {
        public EncryptedSearchIndex(IKeyValueStore publicKeyValueStore, KeySet keySet = null, ILogger logger = null)
           : this(publicKeyValueStore, new FileSystemKeyValueStore(logger), keySet, logger)
        { }

        public EncryptedSearchIndex(IKeyValueStore publicKeyValueStore, IKeyValueStore privateKeyValueStore, KeySet keySet = null, ILogger logger = null)
        {
            PublicKeyValueStore = publicKeyValueStore;
            PrivateKeyValueStore = privateKeyValueStore;
            KeySet = keySet ?? KeySet.ForApplication;
            HmacAlgorithm = HashAlgorithms.SHA256;
            Logger = logger ?? Log.Default;
        }

        public HashAlgorithms HmacAlgorithm { get; set; }
        public KeySet KeySet { get; set; }
        public Encoding Encoding { get; set; }
        public ILogger Logger { get; set; }

        IFeatureExtractor _featureExtractor;
        object _featureExtractorLock = new object();
        public IFeatureExtractor FeatureExtractor
        {
            get
            {
                return _featureExtractorLock.DoubleCheckLock(ref _featureExtractor, () => new WordFeatureExtractor());
            }
            set
            {
                _featureExtractor = value;
            }
        }

        /// <summary>
        /// Gets or sets the public key value store.  This should be considered
        /// less secure/not secure; all values are stored encrypted.
        /// </summary>
        /// <value>
        /// The public key value store.
        /// </value>
        public IKeyValueStore PublicKeyValueStore { get; set; }

        /// <summary>
        /// Gets or sets the private key value store.  This should be a local secure
        /// key value store.  Used to store key lookups.
        /// </summary>
        /// <value>
        /// The private key value store.
        /// </value>
        public IKeyValueStore PrivateKeyValueStore { get; set; }

        public string Retrieve(string key)
        {
            string secret = KeySet.GetAesKey();
            string keyHmac = key.Hmac(secret, HmacAlgorithm, Encoding);
            string kvpCipher = PublicKeyValueStore.Get(keyHmac);
            KeyValuePair<string, string> kvp = DecipherKeyValuePair(kvpCipher);
            if (kvp.Key.Equals(key))
            {
                return kvp.Value;
            }
            else
            {
                Logger.AddEntry("Key mismatch when retrieving encrypted value from keyvalue store. Expected ({0}), Actual ({1})", key, kvp.Key);
                return string.Empty;
            }
        }

        public bool IndexValue(KeyValuePair<string, string> keyValuePair)
        {
            return IndexValue(keyValuePair.Key, keyValuePair.Value);
        }

        public bool IndexValue(string key, string value)
        {
            try
            {
                string secret = KeySet.GetAesKey();
                string keyHmac = key.Hmac(secret, HmacAlgorithm, Encoding);
                string valueHmac = value.Hmac(secret, HmacAlgorithm, Encoding);
                // localstore(hmac(key), hmac(value))
                PrivateKeyValueStore.Set(keyHmac, valueHmac);
                // remotestore(hmac(key):aes(value))
                string publicValueCipher = GetKeyValueCipher(key, value);
                PublicKeyValueStore.Set(keyHmac, publicValueCipher.ToString());

                // extract features from value
                string[] features = FeatureExtractor.ExtractFeatures(value);
                //    foreach(feature) localstorekeys(hmac(feature), hmac(key))
                Parallel.ForEach(features, (feature) =>
                {
                    string featureHmac = feature.Hmac(secret, HmacAlgorithm, Encoding);
                    HashSet<string> hmacKeys = new HashSet<string>();
                    PrivateKeyValueStore.Get(featureHmac).DelimitSplit("\r\n").Each(hmacKey => hmacKeys.Add(hmacKey));
                    hmacKeys.Add(keyHmac);
                    PrivateKeyValueStore.Set(featureHmac, string.Join("\r\n", hmacKeys.ToArray()));
                });
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception indexing key value pair: key={0}, value={1}: {2}", ex, key, value, ex.Message);
                return false;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> Find(string feature)
        {
            string[] keys = Search(feature);
            return Retrieve(keys);
        }

        /// <summary>
        /// Search for values where the specified value is a feature and return the hmac keys.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected string[] Search(string feature)
        {
            string secret = KeySet.GetAesKey();
            string featureHmac = feature.Hmac(secret, HmacAlgorithm, Encoding);            
            return PrivateKeyValueStore.Get(featureHmac).DelimitSplit("\r\n"); // should be a new line delimited list of key hmacs            
        }

        /// <summary>
        /// Retrieves the remote values for the specified keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected IEnumerable<KeyValuePair<string, string>> Retrieve(params string[] hmacKeys)
        {
            // remotelookupkeys(remotekeys)
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            HashSet<string> keys = new HashSet<string>(hmacKeys);
            Parallel.ForEach(keys, (hmacKey) =>
            {
                string valueCipher = PublicKeyValueStore.Get(hmacKey);
                results.Add(DecipherKeyValuePair(valueCipher));
            });
            return results;
        }

        private string GetKeyValueCipher(string key, string value)
        {
            KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
            return KeySet.Encrypt(keyValuePair.ToJson());
        }

        private KeyValuePair<string, string> DecipherKeyValuePair(string cipher)
        {
            string json = KeySet.Decrypt(cipher);
            return json.FromJson<KeyValuePair<string, string>>();
        }

    }
}
