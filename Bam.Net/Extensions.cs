/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Bam.Net.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bam.Net
{
    public static class Extensions
    {
        static Dictionary<string, SerializationFormat> _serializationFormats;
        static object _serializationFormatsLock = new object();
        public static Dictionary<string, SerializationFormat> SerializationFormats
        {
            get
            {
                return _serializationFormatsLock.DoubleCheckLock(ref _serializationFormats, () => new Dictionary<string, SerializationFormat>
                {
                    { ".yaml", SerializationFormat.Yaml },
                    { ".yml", SerializationFormat.Yaml },
                    { ".json", SerializationFormat.Json },
                    { ".xml", SerializationFormat.Xml },
                    { ".dat", SerializationFormat.Binary },
                    { ".bin", SerializationFormat.Binary }
                });
            }
        }

        static Dictionary<SerializationFormat, Func<Stream, Type, object>> _deserializers;
        static object _deserializersLock = new object();
        public static Dictionary<SerializationFormat, Func<Stream, Type, object>> Deserializers
        {
            get
            {
                return _deserializersLock.DoubleCheckLock(ref _deserializers, () => new Dictionary<SerializationFormat, Func<Stream, Type, object>>
                {
                    { SerializationFormat.Invalid, (stream, type) => {
                            Args.Throw<InvalidOperationException>("Invalid SerializationFormat specified");
                            return null;
                        }
                    },
                    { SerializationFormat.Xml, (stream, type)=> stream.FromXmlStream(type) },
                    { SerializationFormat.Json, (stream, type) => stream.FromJsonStream(type) }, // this might not work; should be tested
                    { SerializationFormat.Yaml, (stream, type) => stream.FromYamlStream(type) }, // this might not work; should be tested
                    { SerializationFormat.Binary, (stream, type) => stream.FromBinaryStream() } // this might not work; should be tested
                });
            }
        }

        static Dictionary<SerializationFormat, Action<Stream, object>> _serializeActions;
        static object _serializeActionsLock = new object();
        public static Dictionary<SerializationFormat, Action<Stream, object>> SerializeActions
        {
            get
            {
                return _serializeActionsLock.DoubleCheckLock(ref _serializeActions, () => new Dictionary<SerializationFormat, Action<Stream, object>>
                {
                    { SerializationFormat.Invalid, (stream, obj) => Args.Throw<InvalidOperationException>("Invalid SerializationFormat specified") },
                    { SerializationFormat.Xml, (stream, obj) => obj.ToXmlStream(stream) },
                    { SerializationFormat.Json, (stream, obj) => obj.ToJsonStream(stream) },
                    { SerializationFormat.Yaml, (stream, obj) => obj.ToYamlStream(stream) },
                    { SerializationFormat.Binary, (stream, obj) => obj.ToBinaryStream(stream) }
                });
            }
        }

        static Dictionary<ExistingFileAction, Action<Stream, FileInfo>> _writeResourceActions;
        static object _writeResourceActionsLock = new object();
        public static Dictionary<ExistingFileAction, Action<Stream, FileInfo>> WriteResourceActions
        {
            get
            {
                return _writeResourceActionsLock.DoubleCheckLock(ref _writeResourceActions, () => new Dictionary<ExistingFileAction, Action<Stream, FileInfo>>
                {
                    { ExistingFileAction.Throw, (resource, output) => Args.Throw<InvalidOperationException>("File exists, can't write resource to {0}", output.FullName) },
                    { ExistingFileAction.OverwriteSilently, (resource, output) => resource.CopyTo(output.Create()) },
                    { ExistingFileAction.DoNotOverwrite, (resource, output) => Logging.Log.Warn("File exists, can't write resource to {0}", output.FullName) }
                });
            }
        }

        static Dictionary<ExistingFileAction, Action<ZipArchiveEntry, string>> _extractActions;
        static object _extractActionsLock = new object();
        public static Dictionary<ExistingFileAction, Action<ZipArchiveEntry, string>> ExtractActions
        {
            get
            {
                return _extractActionsLock.DoubleCheckLock(ref _extractActions, () => new Dictionary<ExistingFileAction, Action<ZipArchiveEntry, string>>
                {
                    { ExistingFileAction.Throw, (zip, dest) => Args.Throw<InvalidOperationException>("File exists, can't extract {0}", dest) },
                    { ExistingFileAction.OverwriteSilently, (zip, dest) => zip.ExtractToFile(dest, true) },
                    { ExistingFileAction.DoNotOverwrite, (zip, dest) => Logging.Log.Warn("File exists, can't extract {0}", dest) }
                });
            }
        }

        static Dictionary<HashAlgorithms, Func<byte[], HMAC>> _hmacs;
        static object _hmacsLock = new object();
        public static Dictionary<HashAlgorithms, Func<byte[], HMAC>> Hmacs
        {
            get
            {
                return _hmacsLock.DoubleCheckLock(ref _hmacs, () => new Dictionary<HashAlgorithms, Func<byte[], HMAC>>
                {
                    {Net.HashAlgorithms.MD5, (byte[] key) => new HMACMD5(key) },
                    {Net.HashAlgorithms.RIPEMD160, (byte[] key) => new HMACRIPEMD160(key) },
                    {Net.HashAlgorithms.SHA1, (byte[] key) => new HMACSHA1(key) },
                    {Net.HashAlgorithms.SHA256, (byte[] key) => new HMACSHA256(key) },
                    {Net.HashAlgorithms.SHA384, (byte[] key) => new HMACSHA384(key) },
                    {Net.HashAlgorithms.SHA512, (byte[] key) => new HMACSHA512(key) }
                });
            }
        }

        static Dictionary<HashAlgorithms, Func<HashAlgorithm>> _hashAlgorithms;
        static object _hashAlgorithmLock = new object();
        public static Dictionary<HashAlgorithms, Func<HashAlgorithm>> HashAlgorithms
        {
            get
            {
                return _hashAlgorithmLock.DoubleCheckLock(ref _hashAlgorithms, () => new Dictionary<HashAlgorithms, Func<HashAlgorithm>>
                {
                    { Net.HashAlgorithms.MD5, () => MD5.Create() },
                    { Net.HashAlgorithms.RIPEMD160, () => RIPEMD160.Create() },
                    { Net.HashAlgorithms.SHA1, () => SHA1.Create() },
                    { Net.HashAlgorithms.SHA256, () => SHA256.Create() },
                    { Net.HashAlgorithms.SHA384, () => SHA384.Create() },
                    { Net.HashAlgorithms.SHA512, () => SHA512.Create() }
                });
            }
        }

        public static T FromFile<T>(this FileInfo file)
        {
            return Deserialize<T>(file);
        }

        public static T Deserialize<T>(this FileInfo file)
        {
            return (T)Deserialize(file, typeof(T));
        }

        public static object Deserialize(this FileInfo file, Type type)
        {
            string fileExtension = file.Extension;
            if (!SerializationFormats.ContainsKey(fileExtension))
            {
                throw new ArgumentException($"File extension ({fileExtension}) not supported for deserialization, use one of ({string.Join(",", SerializationFormats.Keys.ToArray())})");
            }
            using (FileStream fs = file.OpenRead())
            {
                return Deserializers[SerializationFormats[fileExtension]](fs, type);
            }
        }

        public static bool DatesAreEqual(this DateTime instance, DateTime other)
        {
            return instance.Year == other.Year && instance.Day == other.Day && instance.Month == other.Month;
        }

        public static bool TimesAreEqual(this DateTime instance, DateTime other, bool includeMilliseconds = false)
        {
            return (instance.Hour == other.Hour && instance.Minute == other.Minute && instance.Second == other.Second) &&
                (includeMilliseconds ? instance.Millisecond == other.Millisecond : true);
        }

        public static string ReadToEnd(this Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        public static T Try<T>(this Func<T> toTry)
        {
            return Try<T>(toTry, out Exception ignore);
        }

        public static T Try<T>(this Func<T> toTry, out Exception ex)
        {
            try
            {
                ex = null;
                return toTry();
            }
            catch (Exception e)
            {
                ex = e;
                return default(T);
            }
        }

        public static bool Try(this Action toTry)
        {
            return Try(toTry, out Exception ignore);

        }
        public static bool Try(this Action toTry, out Exception ex)
        {
            ex = null;
            try
            {
                toTry();
                return true;
            }
            catch (Exception e)
            {
                ex = e;
                return false;
            }
        }

        public static int GetHashCode(this object instance, params string[] propertiesToInclude)
        {
            return GetHashCode(instance, propertiesToInclude.Select(p => instance.Property(p)).ToArray());
        }

        public static int GetHashCode(this object instance, params object[] propertiesToInclude)
        {
            unchecked
            {
                int hash = (int)2166136261;
                foreach (object property in propertiesToInclude)
                {
                    if (property != null)
                    {
                        hash = (hash * 16777619) ^ property.GetHashCode();
                    }
                }
                return hash;
            }
        }

        /// <summary>
        /// Returns true if the string equals "true", "t", "yes", "y" or "1" using a case
        /// insensitive comparison
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAffirmative(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return false;
            }
            return value.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("yes", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("t", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("y", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("1");
        }

        /// <summary>
        /// Returns true if the string equals "false", "f", "no", "n" or 0 using a case
        /// insensitive comparison
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNegative(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.Equals("false", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("no", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("f", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("n", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("0");
        }

        /// <summary>
        /// Returns true if the string equals "q", "quit", "exit" or "bye" using a 
        /// case insensitvie comparison
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsExitRequest(this string value)
        {
            return value.Equals("q", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("quit", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("exit", StringComparison.InvariantCultureIgnoreCase) ||
                value.Equals("bye", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// If the specified file exists, a new path with 
        /// an underscore and a number appended will be 
        /// returned where the new path does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetNextFileName(this string path)
        {
            return GetNextFileName(path, out int num);
        }

        /// <summary>
        /// If the specified file exists, a new path with 
        /// an underscore and a number appended will be 
        /// returned where the new path does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetNextFileName(this string path, out int num)
        {
            FileInfo file = new FileInfo(path);
            DirectoryInfo dir = file.Directory;
            string extension = Path.GetExtension(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            int i = 0;
            num = 0;
            string currentPath = path;
            while (File.Exists(currentPath))
            {
                i++;
                string nextFile = $"{fileName}_{i}{extension}";
                currentPath = Path.Combine(dir.FullName, nextFile);
                num = i;
            }
            return currentPath;
        }

        /// <summary>
        /// If the specified directory exists a new path with 
        /// a number appended will be returned where the 
        /// new path does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetNextDirectoryName(this string path)
        {
            int num;
            return GetNextDirectoryName(path, out num);
        }
        /// <summary>
        /// If the specified directory exists a new path with 
        /// a number appended will be returned where the 
        /// new path does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetNextDirectoryName(this string path, out int num)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            num = 0;
            string currentPath = path;
            while (Directory.Exists(currentPath))
            {
                num++;
                currentPath = $"{path}_{num}";
            }
            return currentPath;
        }

        public static string ReadUntil(this string toRead, char charToFind)
        {
            return ReadUntil(toRead, charToFind, out string ignore);
        }

        /// <summary>
        /// Read the specified string up to the first instance of the specified charToFind
        /// returning the characters read and producing remainder as an out parameter.  Discards
        /// the specified charToFind returning only values on either side
        /// </summary>
        /// <param name="toRead"></param>
        /// <param name="charToFind"></param>
        /// <param name="remainder"></param>
        /// <returns></returns>
        public static string ReadUntil(this string toRead, char charToFind, out string remainder)
        {
            StringBuilder result = new StringBuilder();
            int pos = 0;
            remainder = string.Empty;
            foreach (char c in toRead)
            {
                if (c.Equals(charToFind))
                {
                    remainder = toRead.Substring(pos + 1);
                    break;
                }
                ++pos;
                result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// Return a copy of the specified DateTime with milliseconds
        /// set to 0
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime Trim(this DateTime dateTime)
        {
            Instant copy = new Instant(dateTime)
            {
                Millisecond = 0
            };
            return copy.ToDateTime();
        }

        public static bool In<T>(this T obj, IEnumerable<T> options)
        {
            return new List<T>(options).Contains(obj);
        }

        public static bool In<T>(this T obj, params T[] options)
        {
            return new List<T>(options).Contains(obj);
        }

        public static T Clone<T>(this ICloneable clonable)
        {
            return (T)clonable.Clone();
        }

        public static string ToBase64(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] FromBase64(this string data)
        {
            return Convert.FromBase64String(data);
        }

        public static string ReadAllText(this FileInfo file)
        {
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                return reader.ReadToEnd();
            }
        }

        public static FileInfo GetFileInfo(this Assembly assembly)
        {
            return new FileInfo(assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"));
        }

        public static string GetFilePath(this Assembly assembly)
        {
            return assembly.GetFileInfo().FullName;
        }

        public static bool HasAdminRights(this WindowsIdentity identity)
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static IEnumerable<T> CopyAs<T>(this IEnumerable enumerable) where T : new()
        {
            foreach (object o in enumerable)
            {
                yield return o.CopyAs<T>();
            }
        }

        /// <summary>
        /// Copy the current source instance as the specified generic
        /// type T copying all properties that match in name and type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T CopyAs<T>(this object source) where T : new()
        {
            T result = new T();
            result.CopyProperties(source);
            return result;
        }

        public static IEnumerable<object> CopyAs(this IEnumerable enumerable, Type type, params object[] ctorParams)
        {
            foreach (object o in enumerable)
            {
                yield return o.CopyAs(type, ctorParams);
            }
        }
        
        public static object CopyAs<T>(this object source, params object[] ctorParams)
        {
            return CopyAs(source, typeof(T), ctorParams);
        }
        public static T ToInstance<T>(this Dictionary<string, string> dictionary) where T : class, new()
        {
            return CopyAs<T>(dictionary);
        }
        public static T CopyAs<T>(this Dictionary<string, string> dictionary) where T: class, new()
        {
            T result = new T();
            foreach(string key in dictionary.Keys)
            {
                result.Property(key, dictionary[key]);
            }
            return result;
        }

        public static object ToInstance(this Dictionary<string, string> dictionary, Type type, params object[] ctorParams)
        {
            return CopyAs(dictionary, type, ctorParams);
        }

        public static object CopyAs(this Dictionary<string, string> dictionary, Type type, params object[] ctorParams)
        {
            object result = type.Construct(ctorParams);
            foreach(string key in dictionary.Keys)
            {
                result.Property(key, dictionary[key]);
            }
            return result;
        }

        /// <summary>
        /// Copy the current sourcce instance as the spcified type
        /// copying all properties that match in name and type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CopyAs(this object source, Type type, params object[] ctorParams)
        {
            if (source == null)
            {
                return source;
            }
            object result = type.Construct(ctorParams);
            result.CopyProperties(source);
            return result;
        }

        /// <summary>
        /// Copy the specified instance to a dynamic instance where the new
        /// instance only has the properties addorned with the specified 
        /// custom attribute T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static dynamic CopyAsDynamic<T>(this object instance) where T : Attribute
        {
            return instance.CopyAsDynamic(p => p.HasCustomAttributeOfType<T>());
        }

        public static dynamic CopyAsDynamic(this object instance, Func<PropertyInfo, bool> propertySelector)
        {
            return instance.ToDynamicType(instance.GetType().Name, propertySelector).Construct();
        }

        public static T BinaryClone<T>(this T from) where T : class, new()
        {
            byte[] bytes = from.ToBinaryBytes();
            return bytes.FromBinaryBytes<T>();
        }

        public static T2 CopyTo<T1, T2>(this T1 from) where T2 : class, new()
        {
            T2 result = new T2();
            result.CopyProperties(from);
            return result;
        }

        public static Task<byte[]> GZipAsync(this string value, Encoding enc = null)
        {
            return Task.Run(() => value.GZip(enc));
        }

        public static byte[] GZip(this string value, Encoding enc = null)
        {
            enc = enc ?? Encoding.UTF8;
            return enc.GetBytes(value).GZip();
        }

        public static Task<byte[]> GZipAsync(this byte[] data)
        {
            return Task.Run(() => data.GZip());
        }

        public static byte[] GZip(this byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zipStream = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zipStream.Write(data, 0, data.Length);
                }
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Similar to File.CopyTo but keeps the file extension.
        /// Example, "this.txt".CopyFile("that"); will return
        /// a FileInfo representing "that.txt".
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newFileNameWithoutExtension"></param>
        /// <returns></returns>
        public static FileInfo CopyFile(this FileInfo file, string newFileNameWithoutExtension)
        {
            return CopyFile(file, file.Directory.FullName, newFileNameWithoutExtension);
        }

        /// <summary>
        /// Similar to File.CopyTo but keeps the file extension.
        /// Example, "this.txt".CopyFile("that"); will return
        /// a FileInfo representing "that.txt".
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newFileNameWithoutExtension"></param>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static FileInfo CopyFile(this FileInfo file, string directoryPath, string newFileNameWithoutExtension)
        {
            string newFileName = $"{newFileNameWithoutExtension}{file.Extension}";
            string newFilePath = Path.Combine(directoryPath, newFileName);
            file.CopyTo(newFilePath);
            return new FileInfo(newFilePath);
        }

        public static byte[] GUnzip(this byte[] data)
        {
            using (MemoryStream readStream = new MemoryStream(data))
            {
                using(MemoryStream writeStream = new MemoryStream())
                {
                    using (GZipStream zipStream = new GZipStream(readStream, CompressionMode.Decompress))
                    {
                        byte[] readBuffer = new byte[1024];
                        int countRead;
                        while((countRead = zipStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            writeStream.Write(readBuffer, 0, countRead);
                        }
                    }
                    return writeStream.ToArray();
                }
            }
        }

        public static bool WriteResource(this Assembly assembly, Type siblingOfResource, string resourceName, FileInfo writeTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            return WriteResource(assembly, $"{siblingOfResource.Namespace}.{resourceName}", writeTo, existingFileAction);
        }

        public static bool WriteResource(this Assembly assembly, Type siblingOfResource, string resourceName, string writeTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            return WriteResource(assembly, $"{siblingOfResource.Namespace}.{resourceName}", writeTo, existingFileAction);
        }

        public static bool WriteResource(this Assembly assembly, string resourceName, string writeTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            return WriteResource(assembly, resourceName, new FileInfo(writeTo), existingFileAction);
        }

        public static bool WriteResource(this Assembly assembly, string resourceName, FileInfo writeTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            string[] resourceNames = assembly.GetManifestResourceNames();
            bool found = false;
            resourceNames.Each(rn =>
            {
                bool thisIsTheOne = Path.GetFileName(rn).Equals(resourceName);
                if (thisIsTheOne)
                {
                    found = true;
                    using (Stream resource = assembly.GetManifestResourceStream(rn))
                    {
                        if (File.Exists(writeTo.FullName))
                        {
                            WriteResourceActions[existingFileAction](resource, writeTo);
                        }
                        else
                        {
                            using (Stream writeStream = writeTo.Create())
                            {
                                resource.CopyTo(writeStream);
                            }
                        }
                    }
                }
            });

            return found;
        }
        public static bool UnzipResource(this Assembly assembly, Type siblingOfResource, string resourceName, string extractTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            return UnzipResource(assembly, Path.Combine(siblingOfResource.Namespace, resourceName).Replace("\\", "."), extractTo, existingFileAction);
        }

        public static bool UnzipResource(this Assembly assembly, string resourceName, string extractTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            return UnzipResource(assembly, resourceName, new DirectoryInfo(extractTo));
        }

        public static bool UnzipResource(this Assembly assembly, string resourceName, DirectoryInfo extractTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            string[] resourceNames = assembly.GetManifestResourceNames();
            bool found = false;
            resourceNames.Each(rn =>
            {
                bool thisIsTheOne = Path.GetFileName(rn).Equals(resourceName);
                if (thisIsTheOne)
                {
                    found = true;

                    Stream zipStream = assembly.GetManifestResourceStream(rn);
                    UnzipStream(zipStream, extractTo, existingFileAction);
                }
            });

            return found;
        }

        public static void UnzipStream(this Stream zipStream, DirectoryInfo extractTo, ExistingFileAction existingFileAction)
        {
            ZipArchive zipArchive = new ZipArchive(zipStream);
            zipArchive.Entries.Each(zipFile =>
            {
                FileInfo destinationFile = new FileInfo(Path.Combine(extractTo.FullName, zipFile.FullName));
                if (destinationFile.Exists)
                {
                    ExtractActions[existingFileAction](zipFile, destinationFile.FullName);
                }
                else
                {
                    if (!destinationFile.Directory.Exists)
                    {
                        destinationFile.Directory.Create();
                    }
                    zipFile.ExtractToFile(destinationFile.FullName);
                }
            });
        }

        public static void UnzipTo(this string zipFilePath, string extractToDirectory)
        {
            new FileInfo(zipFilePath).UnzipTo(new DirectoryInfo(extractToDirectory));
        }

        public static void UnzipTo(this FileInfo file, DirectoryInfo extractTo, ExistingFileAction existingFileAction = ExistingFileAction.DoNotOverwrite)
        {
            using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
            {
                fs.UnzipStream(extractTo, existingFileAction);
            }
        }

        /// <summary>
        /// Parse the string as the specified generic enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T TryToEnum<T>(this string value) where T : struct
        {
            Enum.TryParse<T>(value, out T result);
            return result;
        }

        public static T Cast<T>(this object instance)
        {
            return (T)instance;
        }

        public static bool TryCast<T>(this object instance, out T instanceAs)
        {
            bool result = true;
            instanceAs = default(T);
            try
            {
                instanceAs = (T)instance;
            }
            catch //(Exception ex)
            {
                result = false;
            }

            return result;
        }

        public static string ToCsv(this object data)
        {
            return ToCsv(new object[] { data }, false, false);
        }

        public static string ToCsvLine(this object data, Func<Type, PropertyInfo[]> propertyGetter = null)
        {
            propertyGetter = propertyGetter ?? ((t) => t.GetProperties());
            return ToCsv(new object[] { data }, propertyGetter, false, true);
        }

        public static string ToCsv(this object[] dataArr, bool includeHeader = false, bool newLine = true)
        {
            return ToCsv(dataArr, (t) => t.GetProperties(), includeHeader, newLine);
        }

        public static string ToCsv(this object[] dataArr, Func<Type, PropertyInfo[]> propertyGetter, bool includeHeader = false, bool newLine = true)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (TextWriter writer = new StreamWriter(stream))
                {
                    if (includeHeader && dataArr.Length > 0)
                    {
                        dataArr[0].WriteCsvHeader(writer);
                    }

                    dataArr.Each(data =>
                    {
                        WriteCsv(data, propertyGetter, writer);
                        if (newLine)
                        {
                            writer.WriteLine();
                        }
                    });

                    writer.Flush();
                    stream.Flush();
                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        public static void WriteCsvHeader(this object data, TextWriter writer)
        {
            Type type = data.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            List<string> values = new List<string>();
            foreach (PropertyInfo prop in propertyInfos)
            {
                values.Add(prop.Name.PascalSplit(" "));
            }
            writer.Write(string.Join(",", values));
            writer.WriteLine();
        }

        public static void WriteCsv(this object data, TextWriter writer)
        {
            WriteCsv(data, (t) => t.GetProperties(), writer);
        }

        public static void WriteCsv(this object data, Func<Type, PropertyInfo[]> propertyGetter, TextWriter writer)
        {
            Type type = data.GetType();
            PropertyInfo[] propertyInfos = propertyGetter(type);
            List<string> values = new List<string>();
            foreach (PropertyInfo prop in propertyInfos)
            {
                object value = prop.GetValue(data, null);
                string stringValue = value == null ? string.Empty : value.ToString();
                string format = stringValue.Contains(',') ? "\"{0}\"" : "{0}";
                bool replaceQuotes = stringValue.Contains('"');
                if (replaceQuotes)
                {
                    stringValue = stringValue.Replace("\"", "\"\"");
                }
                values.Add(string.Format(format, stringValue));
            }
            writer.Write(string.Join(",", values.ToArray()));
        }

        public static string XmlToHumanReadable(this string xml, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.Unicode;
            string result = "";

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(ms, encoding))
                {
                    XmlDocument doc = new XmlDocument();

                    try
                    {
                        // Load the XmlDocument with the XML.
                        doc.LoadXml(xml);

                        xmlWriter.Formatting = System.Xml.Formatting.Indented;

                        // Write the XML into a formatting XmlTextWriter
                        doc.WriteContentTo(xmlWriter);
                        xmlWriter.Flush();
                        ms.Flush();

                        // Have to rewind the MemoryStream in order to read
                        // its contents.
                        ms.Position = 0;

                        // Read MemoryStream contents into a StreamReader.
                        StreamReader sr = new StreamReader(ms);

                        // Extract the text from the StreamReader.
                        result = sr.ReadToEnd();
                    }
                    catch (XmlException ex)
                    {
                        result = ex.ToString();
                    }

                    xmlWriter.Close();
                }
                ms.Close();
            }
            return result;
        }

        public static bool ImplementsInterface<T>(this object instance)
        {
            Args.ThrowIfNull(instance);

            Type type = instance.GetType();
            return type.ImplementsInterface<T>();
        }

        public static bool ImplementsInterface<T>(this Type type)
        {
            Args.ThrowIf<InvalidOperationException>(!typeof(T).IsInterface, "{0} is not an interface", typeof(T).Name);
            return type.GetInterface(typeof(T).Name) != null;
        }

        public static void Times(this int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
            {
                action(i);
            }
        }

        /// <summary>
        /// Execute the specified Func this 
        /// many times
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T[] Times<T>(this int count, Func<int, T> func)
        {
            T[] results = new T[count];
            for (int i = 0; i < count; i++)
            {
                results[i] = func(i);
            }

            return results;
        }

        public static Dictionary<string, string> FromQueryString(this string queryString)
        {
            return QueryStringToDictionary(queryString);
        }

        public static Dictionary<string, string> QueryStringToDictionary(this string queryString)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            queryString.DelimitSplit("?", "&").Each(nvp =>
            {
                string[] nvps = nvp.DelimitSplit("=");
                result.Add(nvps[0], nvps[1]);
            });
            return result;
        }

        /// <summary>
        /// Parses key value pairs from the string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="pascalCasify">if set to <c>true</c> [pascal casify].</param>
        /// <param name="keyValueSeparator">The key value separator.</param>
        /// <param name="elementSeparator">The element separator.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ParseKeyValuePairs(this string input, bool pascalCasify = true, string keyValueSeparator = ":", string elementSeparator = ";")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string[] elements = input.DelimitSplit(elementSeparator);
            elements.Each(element =>
            {
                GetKeyValue(pascalCasify, keyValueSeparator, element, out string key, out string value);

                if (result.ContainsKey(key))
                {
                    Args.Throw<InvalidOperationException>("The key {0} exists more than once in the specified string: {1}", key, input);
                }
                result.Add(key, value);
            });

            return result;
        }

        public static T ParseKeyValuePairs<T>(this string input, bool pascalCasify = true, string keyValueSeparator = ":", string elementSeparator = ";") where T : class, new()
        {
            T result = new T();
            string[] elements = input.DelimitSplit(elementSeparator);
            elements.Each(element =>
            {
                GetKeyValue(pascalCasify, keyValueSeparator, element, out string key, out string value);

                result.Property(key, value);
            });

            return result;
        }
        
        private static void GetKeyValue(bool pascalCasify, string keyValueSeparator, string element, out string key, out string value)
        {
            string[] kvp = element.DelimitSplit(keyValueSeparator);
            Args.ThrowIf<ArgumentException>(kvp.Length < 1 || kvp.Length > 2, "Unrecognized Key Value pair format: ({0})", element);

            key = pascalCasify ? kvp[0].PascalCase() : kvp[0];
            value = string.Empty;
            if (kvp.Length == 2)
            {
                value = pascalCasify ? kvp[1].PascalCase() : kvp[1];
            }
        }

        /// <summary>
        /// Iterate over the current IEnumerable passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> arr, Action<T> action)
        {
            foreach (T item in arr)
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterate over the current array passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Each<T>(this T[] arr, Action<T> action)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = 0; i < l; i++)
                {
                    action(arr[i]);
                }
            }
        }

        /// <summary>
        /// Iterate over the current IEnumerable passing
        /// each element to the specified function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="function"></param>
        public static void Each<T>(this IEnumerable<T> arr, Func<T, bool> function)
        {
            foreach (T item in arr)
            {
                function(item);
            }
        }

        /// <summary>
        /// Iterate over the current array passing
        /// each element to the specified function.  
        /// Return true to continue the loop return 
        /// false to stop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="function"></param>
        public static void Each<T>(this T[] arr, Func<T, bool> function)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = 0; i < l; i++)
                {
                    if (!function(arr[i]))
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Iterate over the current IEnumerable passing
        /// each element to the specified function.  
        /// Return true to continue the loop return 
        /// false to stop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="function"></param>
        public static void Each<T>(this IEnumerable<T> arr, Func<T, int, bool> function)
        {
            arr.ToArray().Each(function);
        }

        /// <summary>
        /// Iterate over the current array passing
        /// each element to the specified function.  
        /// Return true to continue the loop return 
        /// false to stop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="function"></param>
        public static void Each<T>(this T[] arr, Func<T, int, bool> function)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = 0; i < l; i++)
                {
                    if (!function(arr[i], i))
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Iterate over the current IEnumerable passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> arr, Action<T, int> action)
        {
            arr.ToArray().Each(action);
        }

        /// <summary>
        /// Iterate over the current array passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Each<T>(this T[] arr, Action<T, int> action)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = 0; i < l; i++)
                {
                    action(arr[i], i);
                }
            }
        }

        public static void Each<T>(this IEnumerable<T> arr, dynamic context, Action<dynamic, T> action)
        {
            Each<T>(arr.ToArray(), context, action);
        }

        public static void Each<T>(this T[] arr, dynamic context, Action<dynamic, T> action)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = 0; i < l; i++)
                {
                    action(context, arr[i]);
                }
            }
        }

        /// <summary>
        /// Iterate over the current IEnumerable 
        /// from the specified index passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Rest<T>(this IEnumerable<T> arr, int startIndex, Action<T, int> action)
        {
            arr.ToArray().Rest(startIndex, action);
        }

        /// <summary>
        /// Iterate over the current array from the 
        /// specified index passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Rest<T>(this T[] arr, int startIndex, Action<T, int> action)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = startIndex; i < l; i++)
                {
                    action(arr[i], i);
                }
            }
        }
        /// <summary>
        /// Iterate over the current IEnumerable starting from the specified index
        /// passing each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Rest<T>(this IEnumerable<T> arr, int startIndex, Action<T> action)
        {
            arr.ToArray().Rest(startIndex, action);
        }

        /// <summary>
        /// Iterate over the current array from the specified
        /// startIndex passing
        /// each element to the specified action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void Rest<T>(this T[] arr, int startIndex, Action<T> action)
        {
            if (arr != null && startIndex <= arr.Length - 1)
            {
                int l = arr.Length;
                for (int i = startIndex; i < l; i++)
                {
                    action(arr[i]);
                }
            }
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void BackwardsEach<T>(this IEnumerable<T> arr, Action<T> action)
        {
            arr.ToArray().BackwardsEach(action);
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void BackwardsEach<T>(this T[] arr, Action<T> action)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = l - 1; i >= 0; i--)
                {
                    action(arr[i]);
                }
            }
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void BackwardsEach<T>(this IEnumerable<T> arr, Func<T, bool> function)
        {
            arr.ToArray().BackwardsEach(function);
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void BackwardsEach<T>(this T[] arr, Func<T, bool> function)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = l - 1; i >= 0; i--)
                {
                    bool result = function(arr[i]);
                    if (!result)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        public static void BackwardsEach<T>(this IEnumerable<T> arr, Func<T, int, bool> function)
        {
            arr.ToArray().BackwardsEach(function);
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        public static void BackwardsEach<T>(this T[] arr, Func<T, int, bool> function)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = l - 1; i >= 0; i--)
                {
                    bool result = function(arr[i], i);
                    if (!result)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void BackwardsEach<T>(this IEnumerable<T> arr, Action<T, int> action)
        {
            arr.ToArray().BackwardsEach(action);
        }

        /// <summary>
        /// Iterate backwards over the specified array (IEnumerable).
        /// This will allow one to remove the current element without
        /// causing an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void BackwardsEach<T>(this T[] arr, Action<T, int> action)
        {
            if (arr != null)
            {
                int l = arr.Length;
                for (int i = l - 1; i >= 0; i--)
                {
                    action(arr[i], i);
                }
            }
        }

        /// <summary>
        /// Iterate over the current array passing 
        /// each element to the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="func"></param>
        /// <returns>The result of each call to the specified function</returns>
        public static T[] Each<T>(this object[] arr, Func<object, T> func)
        {
            int l = arr.Length;
            T[] result = new T[l];
            for (int i = 0; i < l; i++)
            {
                result[i] = func(arr[i]);
            }
            return result;
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> arr)
        {
            if (arr == null)
            {
                return new T[] { };
            }
            return arr;
        }

        public static IEnumerable<T> OrEmpty<T>(this T[] arr)
        {
            if (arr == null)
            {
                return new T[] { };
            }
            return arr;
        }

        public static bool TryConstruct(this Type type, out object constructed, params object[] ctorParams)
        {
            return type.TryConstruct(out constructed, ex => { }, ctorParams);
        }

        public static bool TryConstruct(this Type type, out object constructed, Action<Exception> catcher, params object[] ctorParams)
        {
            bool result = false;
            constructed = null;
            try
            {
                constructed = Construct(type, ctorParams);
                result = constructed != null;
            }
            catch (Exception ex)
            {
                catcher(ex);
                result = false;
            }

            return result;
        }

        public static bool TryConstruct<T>(this Type type, out T constructed, params object[] ctorParams)
        {
            return type.TryConstruct(out constructed, ex => { }, ctorParams);
        }

        public static bool TryConstruct<T>(this Type type, out T constructed, Action<Exception> catcher, params object[] ctorParams)
        {
            bool result = true;
            constructed = default(T);
            try
            {
                constructed = Construct<T>(type, ctorParams);
            }
            catch (Exception ex)
            {
                catcher(ex);
                result = false;
            }

            return result;
        }


        private delegate T CompiledLambdaCtor<T>(params object[] ctorParams);
        /// <summary>
        /// Construct an instance of the type using a dynamically defined and
        /// compiled lambda.  This "should" replace existing Construct&lt;T&gt;
        /// implementation after benchmarks prove this one is faster
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="ctorParams"></param>
        /// <returns></returns>
        public static T DynamicConstruct<T>(this Type type, params object[] ctorParams)
        {
            ParameterExpression param;
            NewExpression newExp;
            GetExpressions(type, ctorParams, out param, out newExp);

            LambdaExpression lambda = Expression.Lambda(typeof(CompiledLambdaCtor<T>), newExp, param);
            CompiledLambdaCtor<T> compiled = (CompiledLambdaCtor<T>)lambda.Compile();
            return compiled(ctorParams);
        }

        private delegate object CompiledLambdaCtor(params object[] ctorParams);
        /// <summary>
        /// Construct an instance of the type using a dynamically defined and
        /// compiled lambda.  This "should" replace existing Construct&lt;T&gt;
        /// implementation after benchmarks prove this one is faster.
        /// Testing shows this is actually roughly 2x slower than the existing 
        /// Construct methods.  Keeping here for novelty reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="ctorParams"></param>
        /// <returns></returns>
        public static object DynamicConstruct(this Type type, params object[] ctorParams)
        {
            ParameterExpression param;
            NewExpression newExp;
            GetExpressions(type, ctorParams, out param, out newExp);

            LambdaExpression lambda = Expression.Lambda(typeof(CompiledLambdaCtor), newExp, param);
            CompiledLambdaCtor compiled = (CompiledLambdaCtor)lambda.Compile();
            return compiled(ctorParams);
        }

        private static void GetExpressions(Type type, object[] ctorParams, out ParameterExpression param, out NewExpression newExp)
        {
            ConstructorInfo ctor = GetConstructor(type, ctorParams);
            ParameterInfo[] parameterInfos = ctor.GetParameters();

            param = Expression.Parameter(typeof(object[]), "args");
            Expression[] argsExp = new Expression[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = parameterInfos[i].ParameterType;

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);

                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                argsExp[i] = paramCastExp;
            };

            newExp = Expression.New(ctor, argsExp);
        }

        /// <summary>
        /// Construct an instance of the type
        /// </summary>
        /// <typeparam name="T">The type to cast the result as</typeparam>
        /// <param name="type">The type whose constructor will be called</param>
        /// <param name="ctorParams">The parameters to pass to the constructor if any</param>
        /// <returns></returns>
        public static T Construct<T>(this Type type, params object[] ctorParams)
        {
            return (T)type.Construct(ctorParams);
        }

        /// <summary>
        /// Construct an instance of the specified type passing in the
        /// specified parameters to the constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ctorParams"></param>
        /// <returns></returns>
        public static object Construct(this Type type, params object[] ctorParams)
        {
            ConstructorInfo ctor = GetConstructor(type, ctorParams);
            object val = null;
            if (ctor != null)
            {
                val = ctor.Invoke(ctorParams);
            }

            return val;
        }

        /// <summary>
        /// If the current string is null or empty returns
        /// the specified "instead" string otherwise returns
        /// the current string.
        /// </summary>
        /// <param name="valueOrNull"></param>
        /// <param name="instead"></param>
        /// <returns></returns>
        public static string Or(this string valueOrNull, string instead)
        {
            if (string.IsNullOrEmpty(valueOrNull))
            {
                return instead;
            }

            return valueOrNull;
        }

        public static bool Is<T>(this object obj) where T : class
        {
            T ignore;
            return obj.Is<T>(out ignore);
        }

        public static bool Is<T>(this object obj, out T objAs) where T : class
        {
            objAs = obj as T;
            return objAs != null;
        }

        public static void SerializeToFile(this object obj, SerializationFormat format, string filePath)
        {
            SerializeToFile(obj, format, new FileInfo(filePath));
        }
        public static void SerializeToFile(this object obj, SerializationFormat format, FileInfo file)
        {
            MemoryStream output = new MemoryStream();
            Serialize(obj, format, output);
            using(StreamWriter sw = new StreamWriter(file.FullName))
            {
                using(StreamReader reader = new StreamReader(output))
                {
                    sw.Write(reader.ReadToEnd());
                }
            }
        }

        public static void Serialize(this object obj, SerializationFormat format, Stream output)
        {
            SerializeActions[format](output, obj);
        }

        /// <summary>
        /// An extension method to enable functional programming access
        /// to string.Format.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatArgs"></param>
        /// <returns></returns>
        public static string _Format(this string format, params object[] formatArgs)
        {
            return string.Format(format, formatArgs);
        }

        /// <summary>
        /// Double null check the specified toInit locking on the current
        /// object using the specified ifNull function to instantiate if 
        /// toInit is null.  This guarantees thread safe access to the
        /// resulting object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sync"></param>
        /// <param name="toInit"></param>
        /// <param name="ifNull"></param>
        /// <returns></returns>
        public static T DoubleCheckLock<T>(this object sync, ref T toInit, Func<T> ifNull)
        {
            if (toInit == null)
            {
                lock (sync)
                {
                    if (toInit == null)
                    {
                        toInit = ifNull();
                    }
                }
            }

            return toInit;
        }

        /// <summary>
        /// Serialize the current object to json in the specified path
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void ToJsonFile(this object value, string path)
        {
            ToJsonFile(value, new FileInfo(path));
        }

        /// <summary>
        /// Serialize the current object as json to the specified file overwriting
        /// the existing file if there is one.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="file"></param>
        public static void ToJsonFile(this object value, FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            ToJson(value, Newtonsoft.Json.Formatting.Indented).SafeWriteToFile(file.FullName, true);
        }

        /// <summary>
        /// Return a Stream containing the current
        /// object as json
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Stream ToJsonStream(this object value)
        {
            MemoryStream stream = new MemoryStream();
            ToJsonStream(value, stream);
            return stream;
        }

        /// <summary>
        /// Write the specified value to the specified stream as json
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        public static void ToJsonStream(this object value, Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);            
            writer.Write(value.ToJson());
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
        }

        public static string ToJson(this object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            return JsonConvert.SerializeObject(value);
        }

        public static string ToJson(this object value, bool pretty, NullValueHandling nullValueHandling = NullValueHandling.Ignore)
        {
            Newtonsoft.Json.Formatting formatting = pretty ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = formatting;
            settings.NullValueHandling = nullValueHandling;
            return value.ToJson(settings);
        }

        public static string ToJson(this object value, Newtonsoft.Json.Formatting formatting)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static string ToJson(this object value, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(value, settings);
        }

        public static bool HasExtension(this FileInfo file, string dotExtension)
        {
            return Path.GetExtension(file.FullName).Equals(dotExtension);
        }

        public static bool HasNoExtension(this FileInfo file)
        {
            return Path.GetExtension(file.FullName).Equals(string.Empty);
        }

        /// <summary>
        /// Reads the file and deserializes the contents as the specified
        /// generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public static T FromJsonFile<T>(this FileInfo file)
        {
            using (StreamReader sr = new StreamReader(file.FullName))
            {
                return FromJson<T>(sr.ReadToEnd());
            }
        }

        public static T FromJson<T>(this FileInfo file)
        {
            using (StreamReader sr = new StreamReader(file.OpenRead()))
            {
                return sr.ReadToEnd().FromJson<T>();
            }
        }

        /// <summary>
        /// Deserialize the current string as the specified
        /// generic type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Deserialize the current json string as the specified
        /// type
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object FromJson(this string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// Deserialize the contents of the file path specified
        /// in the current string to the type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T FromJsonFile<T>(this string filePath)
        {
            return filePath.SafeReadFile().FromJson<T>();
        }

        public static T FromJsonStream<T>(this Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            using (StreamReader sr = new StreamReader(ms))
            {
                return sr.ReadToEnd().FromJson<T>();
            }
        }

        public static object FromJsonStream(this Stream stream, Type type)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            using (StreamReader sr = new StreamReader(ms))
            {
                return sr.ReadToEnd().FromJson(type);
            }
        }

        public static T FromJObject<T>(this JObject jObject)
        {
            return jObject.ToObject<T>();
        }

        public static T FromJObject<T>(this object jObject)
        {
            return jObject.ToJson().FromJson<T>();
        }

        public static object FromJObject(this object jObject, Type type)
        {
            return jObject.ToJson().FromJson(type);
        }

        public static string ContentHash(this string filePath, HashAlgorithms algorithm, Encoding encoding = null)
        {
            return ContentHash(new FileInfo(filePath), algorithm, encoding);
        }

        public static string ContentHash(this FileInfo file, HashAlgorithms algorithm, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            HashAlgorithm alg = HashAlgorithms[algorithm]();
            byte[] fileContents = File.ReadAllBytes(file.FullName);
            byte[] hashBytes = alg.ComputeHash(fileContents);

            return hashBytes.ToHexString();
        }

        /// <summary>
        /// Use int.TryParse to try to convert the specified
        /// number to an integer; returns 0 on failure
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int ToInt(this string number)
        {
            if (int.TryParse(number, out int value))
            {
                return value;
            }
            return 0;
        }

        public static string Md5(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Net.HashAlgorithms.MD5, encoding);
        }

        public static string Ripmd160(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Net.HashAlgorithms.RIPEMD160, encoding);
        }

        /// <summary>
        /// Calculate the SHA1 for the contents of the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Sha1(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Net.HashAlgorithms.SHA1, encoding);
        }

        /// <summary>
        /// Calculate the SHA256 for the contents of the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Sha256(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Net.HashAlgorithms.SHA256, encoding);
        }
        
        public static string Sha384(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Net.HashAlgorithms.SHA384, encoding);
        }

        public static string Sha512(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Net.HashAlgorithms.SHA512, encoding);
        }

        public static string Sha1(this byte[] bytes)
        {
            return Hash(bytes, Net.HashAlgorithms.SHA1);
        }

        public static string Sha256(this byte[] bytes)
        {
            return Hash(bytes, Net.HashAlgorithms.SHA256);
        }

        public static string Md5(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Net.HashAlgorithms.MD5, encoding);
        }

        public static string Ripmd160(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Net.HashAlgorithms.RIPEMD160, encoding);
        }

        public static string Sha384(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Net.HashAlgorithms.SHA384, encoding);
        }

        public static string Sha1(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Net.HashAlgorithms.SHA1, encoding);
        }

        public static string Sha256(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Net.HashAlgorithms.SHA256, encoding);
        }

        public static string Sha512(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Net.HashAlgorithms.SHA512, encoding);
        }

        public static string HmacSha1(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Net.HashAlgorithms.SHA1, encoding);
        }

        public static string HmacSha256(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Net.HashAlgorithms.SHA256, encoding);
        }

        public static string HmacSha384(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Net.HashAlgorithms.SHA384, encoding);
        }

        public static string HmacSha512(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Net.HashAlgorithms.SHA512, encoding);
        }

        public static string Hmac(this string toValidate, string key, HashAlgorithms algorithm, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            HMAC hmac = Hmacs[algorithm](encoding.GetBytes(key));
            return hmac.ComputeHash(encoding.GetBytes(toValidate)).ToHexString();
        }

        public static string Hash(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(toBeHashed);

            return Hash(bytes, algorithm);
        }

        public static string Hash(this byte[] bytes, HashAlgorithms algorithm)
        {
            HashAlgorithm alg = HashAlgorithms[algorithm]();
            byte[] hashBytes = alg.ComputeHash(bytes);

            return hashBytes.ToHexString();
        }

        public static byte[] ToBytes(this string text, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(text);
        }

        public static string FromBytes(this byte[] text, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(text);
        }

        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static int ToHashInt(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            byte[] hashBytes = ToHashBytes(toBeHashed, algorithm, encoding);

            return BitConverter.ToInt32(hashBytes, 0);
        }

        public static long ToHashLong(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding= null)
        {
            byte[] hashBytes = ToHashBytes(toBeHashed, algorithm, encoding);

            return BitConverter.ToInt64(hashBytes, 0);
        }

        public static byte[] ToHashBytes(string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            HashAlgorithm alg = HashAlgorithms[algorithm]();
            encoding = encoding ?? Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(toBeHashed);
            byte[] hashBytes = alg.ComputeHash(bytes);
            return hashBytes;
        }

        public static int ToSha1Int(this string toBeHashed)
        {
            return ToHashInt(toBeHashed, Net.HashAlgorithms.SHA1);
        }

        public static int ToSha256Int(this string toBeHashed)
        {
            return ToHashInt(toBeHashed, Net.HashAlgorithms.SHA256);
        }

        public static long ToSha256Long(this string toBeHashed)
        {
            return ToHashLong(toBeHashed, Net.HashAlgorithms.SHA256);
        }

        public static long ToSha1Long(this string toBeHashed)
        {
            return ToHashLong(toBeHashed, Net.HashAlgorithms.SHA1);
        }

        public static byte[] FromHexString(this string hexString)
        {
            return HexToBytes(hexString);
        }

        public static byte[] HexToBytes(this string hexString)
        {
            //check for null
            if (hexString == null) return null;
            //get length
            int len = hexString.Length;
            if (len % 2 == 1) return null;
            int len_half = len / 2;
            //create a byte array
            byte[] bs = new byte[len_half];

            //convert the hexstring to bytes
            for (int i = 0; i != len_half; i++)
            {
                bs[i] = (byte)Int32.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            //return the byte array
            return bs;
        }

        /// <summary>
        /// Attempts to determine if the file is a text file
        /// by reading the first 5000 bytes and testing 
        /// each byte to see if it is a valid Unicode 
        /// character.  If a byte is found that doesn't have
        /// a Unicode representation the return value is false.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static bool IsText(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                return false;
            }

            int size = 5000;
            if (fileInfo.Length <= 5000)
            {
                size = (int)fileInfo.Length;
            }

            byte[] sample = new byte[size];
            using (StreamReader sr = new StreamReader(fileInfo.FullName))
            {
                sr.BaseStream.Read(sample, 0, size);
            }

            foreach (byte b in sample)
            {
                try
                {
                    Convert.ToChar(b);
                }
                catch (InvalidCastException ice)
                {
                    return false; // catch only invalid cast so others can crash the app if necessary
                }
            }

            return true;
        }

        public static bool IsBinary(this FileInfo fileInfo)
        {
            return !IsText(fileInfo);
        }

        public static bool Is(this FileInfo fileInfo, FileAttributes attribute)
        {
            FileAttributes attributes = File.GetAttributes(fileInfo.FullName);
            return (attributes & attribute) == attribute;
        }

        public static void SetAttribute(this FileInfo fileInfo, FileAttributes attribute)
        {
            File.SetAttributes(fileInfo.FullName, attribute);
        }

        public static void RemoveAttribute(this FileInfo fileInfo, FileAttributes attribute)
        {
            FileAttributes removed = File.GetAttributes(fileInfo.FullName) & ~attribute;
            File.SetAttributes(fileInfo.FullName, removed);
        }

        public static string RemoveInvalidFilePathCharacters(this string value)
        {
            return value.Replace("<", "").Replace(">", "").Replace(":", "").Replace("\"", "").Replace("/", "").Replace("\\", "").Replace("|", "").Replace("?", "").Replace("*", "");
        }

        /// <summary>
        /// Read the first line of the string and return the 
        /// result.  A line is defined as a sequence of characters 
        /// followed by a line feed ("\n"), a carriage return ("\r"), 
        /// or a carriage return immediately followed by a line feed ("\r\n").
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReadLine(this string value)
        {
            string result;
            ReadLine(value, out result);
            return result;
        }

        /// <summary>
        /// Read the first line of the string returning
        /// the remainder and outing the line.
        /// A line is defined as a sequence of characters 
        /// followed by a line feed ("\n"), a carriage return 
        /// ("\r"), or a carriage return immediately followed 
        /// by a line feed ("\r\n").
        /// </summary>
        /// <param name="value"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string ReadLine(this string value, out string line)
        {
            StringReader reader = new StringReader(value);
            line = reader.ReadLine();
            return value.TruncateFront(line.Length).Trim();
        }

        /// <summary>
        /// Return the specified number of characters
        /// from the beginning of the string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Head(this string value, int count)
        {
            string head;
            value.Head(count, out head);
            return head;
        }

        /// <summary>
        /// Return the specified count of characters from the
        /// begginning of the string returning the remaining
        /// value and outing the head
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        public static string Head(this string value, int count, out string head)
        {
            char[] chars = value.ToCharArray();
            StringBuilder headBuilder = new StringBuilder();
            count.Times((i) =>
            {
                headBuilder.Append(chars[i]);
            });
            head = headBuilder.ToString();

            return value.TruncateFront(count);
        }

        /// <summary>
        /// Return the specified number of characters
        /// from the end of the string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Tail(this string value, int count)
        {
            value.Tail(count, out string tail);
            return tail;
        }

        /// <summary>
        /// Return the specified count of characters from the 
        /// end of the string returning the remaining value
        /// and outing the tail
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string Tail(this string value, int count, out string tail)
        {
            char[] chars = value.ToCharArray();
            char[] tailBuffer = new char[count];
            count.Times((i) =>
            {
                int num = i + 1;
                tailBuffer[i] = chars[chars.Length - num];
            });
            tailBuffer = tailBuffer.Reverse().ToArray();
            string tailTmp = string.Empty;
            tailBuffer.Each(c =>
            {
                tailTmp += c;
            });
            tail = tailTmp;

            return value.Truncate(count);
        }

        /// <summary>
        /// Return a random string of the specified
        /// length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(this int length)
        {
            return RandomString(length, true, true);
        }

        /// <summary>
        /// Add the specified length of random characters
        /// to the current string.  Only  lowercase
        /// letters.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(this string result, int length)
        {
            for (int i = 0; i < length; i++)
            {

                char ch = Convert.ToChar(RandomHelper.Next(97, 122)); // ascii codes for printable alphabet

                result += ch;
            }

            return result;
        }

        /// <summary>
        /// Append the specified toAppend string to the current
        /// string
        /// </summary>
        /// <param name="current"></param>
        /// <param name="toAppend"></param>
        /// <returns></returns>
        public static string Plus(this string current, string toAppend)
        {
            StringBuilder sb = new StringBuilder(current);
            sb.Append(toAppend);
            return sb.ToString();
        }

        public static string And(this string current, string toAppend)
        {
            return current.Plus(toAppend);
        }

        public static string Plus(this string current, string toAppendFormat, params object[] args)
        {
            StringBuilder sb = new StringBuilder(current);
            sb.AppendFormat(toAppendFormat, args);
            return sb.ToString();
        }

        public static string And(this string current, string toAppendFormat, params object[] args)
        {
            return current.Plus(toAppendFormat, args);
        }

        public static string ToMonthName(this int oneThroughTwelve)
        {
            Args.ThrowIf(oneThroughTwelve < 1 || oneThroughTwelve > 12, "Specified month number must be from 1 to 12: value specified ({0})", oneThroughTwelve.ToString());
            return new DateTime(1, oneThroughTwelve, 1).ToString("MMMM");
        }

        /// <summary>
        /// Returns a random lower-case character a-z or 0-9
        /// </summary>
        /// <returns>String</returns>
        public static char RandomChar()
        {
            if (RandomBool())
            {
                return RandomLetter().ToCharArray()[0];
            }
            else
            {
                return RandomNumber().ToString().ToCharArray()[0];
            }
        }


        /// <summary>
        /// Get a random boolean
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool()
        {
            return RandomHelper.Next(2) == 1;
        }

        public static string RandomString(this int length, bool mixCase, bool includeNumbers)
        {
            if (length <= 0)
                throw new InvalidOperationException("length must be greater than 0");


            string retTemp = string.Empty;

            for (int i = 0; i < length; i++)
            {
                if (includeNumbers)
                    retTemp += RandomChar().ToString();
                else
                    retTemp += RandomLetter();
            }

            if (mixCase)
            {
                string upperIzed = MixCase(retTemp);

                retTemp = upperIzed;
            }
            return retTemp;
        }

        static string[] letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        public static string[] LowerCaseLetters
        {
            get
            {
                return letters;
            }
        }

        public static string[] UpperCaseLetters
        {
            get
            {
                List<string> upper = new List<string>();
                foreach (string letter in letters)
                {
                    upper.Add(letter.ToUpper());
                }
                return upper.ToArray();
            }
        }

        public static Dictionary<char, List<T>> LetterGroups<T>(this List<T> list, Func<T, string> propertyReader)
        {
            Dictionary<char, List<T>> results = new Dictionary<char, List<T>>
            {
                { '\0', new List<T>() }
            };
            list.ForEach(val =>
            {
                string propertyValue = propertyReader(val);
                if (!string.IsNullOrEmpty(propertyValue))
                {
                    char first = propertyValue[0];
                    if (!results.ContainsKey(first))
                    {
                        results.Add(first, new List<T>());
                    }
                    results[first].Add(val);
                }
                else
                {
                    results['\0'].Add(val);
                }
            });
            return results;
        }

        /// <summary>
        /// Return the specified number of random letters
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RandomLetters(this int count)
        {
            return count.RandomString();
        }

        /// <summary>
        /// Append the specified number of characters
        /// to the end of the string
        /// </summary>
        /// <param name="val"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RandomLetters(this string val, int count)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(val);
            for (int i = 0; i < count; i++)
            {
                txt.Append(RandomLetter());
            }

            return txt.ToString();
        }

        public static IEnumerable<string> SplitByLength(this string value, int maxLength)
        {
            for (int index = 0; index < value.Length; index += maxLength)
            {
                yield return value.Substring(index, Math.Min(maxLength, value.Length - index));
            }
        }
        /// <summary>
        /// Returns a random lowercase letter from a-z."
        /// </summary>
        /// <returns>String</returns>
        public static string RandomLetter()
        {
            return letters[RandomHelper.Next(0, 26)];
        }

        /// <summary>
        /// Returns a pseudo-random number from 0 to 9.
        /// </summary>
        /// <returns></returns>
        public static int RandomNumber()
        {
            return RandomNumber(10);
        }

        public static int RandomNumber(int max)
        {
            return RandomHelper.Next(max);
        }

        private static string MixCase(string retTemp)
        {
            return MixCase(retTemp, 5);
        }

        private static string MixCase(string retTemp, int tryCount)
        {
            if (tryCount <= 0)
                return retTemp;

            if (retTemp.Length < 2)
                return retTemp;

            string upperIzed = string.Empty;
            bool didUpper = false;
            bool keptLower = false;
            foreach (char c in retTemp)
            {
                string upper = string.Empty;
                if (RandomBool())
                {
                    upper = c.ToString().ToUpper();
                    didUpper = true;
                }
                else
                {
                    upper = c.ToString();
                    keptLower = true;
                }

                upperIzed += upper;
            }

            if (didUpper && keptLower)
                return upperIzed;
            else
                return MixCase(upperIzed, --tryCount);
        }

        /// <summary>
        /// Attempts to return the plural version of the supplied word (assumed to be a noun)
        /// using basic rules.
        /// </summary>
        /// <param name="stringToPluralize"></param>
        /// <returns></returns>
        public static string Pluralize(this string stringToPluralize)
        {
            string checkValue = stringToPluralize.ToLowerInvariant();
            if (checkValue.EndsWith("ies"))
            {
                return stringToPluralize;
            }
            else if (checkValue.EndsWith("us"))
            {
                return stringToPluralize.Substring(0, stringToPluralize.Length - 2) + "i";
            }
            else if (checkValue.EndsWith("s") ||
                checkValue.EndsWith("sh"))
            {
                return stringToPluralize + "es";
            }
            else if (checkValue.EndsWith("ay") ||
                checkValue.EndsWith("ey") ||
                checkValue.EndsWith("iy") ||
                checkValue.EndsWith("oy") ||
                checkValue.EndsWith("uy"))
            {
                return stringToPluralize + "s";
            }
            else if (checkValue.EndsWith("y"))
            {
                return stringToPluralize.Substring(0, stringToPluralize.Length - 1) + "ies";
            }
            else
            {
                return stringToPluralize + "s";
            }
        }

        public static Dictionary<string, object> PropertiesToDictionary(this object value)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Type type = value.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                result.Add(prop.Name, prop.GetValue(value, null));
            }

            return result;
        }

        /// <summary>
        /// Drops the specified number of characters from the end of the 
        /// string toTruncate and returns the result
        /// </summary>
        /// <param name="toTruncate"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Truncate(this string toTruncate, int count)
        {
            if (count > toTruncate.Length)
                return "";

            return toTruncate.Substring(0, toTruncate.Length - count);
        }

        /// <summary>
        /// Drop the specified number of characters from the beginning of the
        /// string toTruncate and returns the result
        /// </summary>
        /// <param name="toTruncate"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string TruncateFront(this string toTruncate, int count)
        {
            if (count > toTruncate.Length)
                return "";

            return toTruncate.Substring(count, toTruncate.Length - count);
        }

        /// <summary>
        /// Return the first specified number of characters
        /// </summary>
        /// <param name="stringToTrim"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string First(this string stringToTrim, int count)
        {
            if (stringToTrim.Length <= count)
            {
                return stringToTrim;
            }

            return stringToTrim.Substring(0, count);
        }

        public static string TryPropertiesToString(this object obj, string separator = "\r\n")
        {
            try
            {
                return obj.PropertiesToString(separator);
            }
            catch //(Exception ex)
            {

            }

            return string.Empty;
        }

        /// <summary>
        /// Read the properties of the specified object and return the 
        /// values as a string on a single line
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PropertiesToLine(this object obj)
        {
            return PropertiesToString(obj, "~");
        }

        public static string PropertiesToString(this object obj, string separator = "\r\n")
        {
            Args.ThrowIfNull(obj);

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            return PropertiesToString(obj, properties, separator);
        }

        public static string PropertiesToString(this object obj, PropertyInfo[] properties, string separator = "\r\n")
        {
            try
            {
                StringBuilder returnValue = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        if (property.PropertyType == typeof(string[]))
                        {
                            string[] values = ((string[])property.GetValue(obj, null)) ?? new string[] { };
                            returnValue.AppendFormat("{0}: {1}{2}", property.Name, string.Join(", ", values), separator);
                        }
                        else if (property.PropertyType == typeof(HttpCookieCollection))
                        {
                            object value = property.GetValue(obj, null);
                            if (value != null)
                            {
                                HttpCookieCollection values = (HttpCookieCollection)value;
                                List<string> strings = new List<string>();
                                foreach (HttpCookie cookie in values)
                                {
                                    strings.Add(string.Format("{0}={1}", cookie.Name, cookie.Value));
                                }
                                returnValue.AppendFormat("{0}: {1}{2}", property.Name, string.Join("\r\n\t", strings.ToArray()), separator);
                            }
                            else
                            {
                                returnValue.AppendFormat("{0}: [null]{1}", property.Name, separator);
                            }
                        }
                        else if (property.PropertyType == typeof(System.Net.CookieCollection))
                        {
                            object value = property.GetValue(obj, null);
                            if (value != null)
                            {
                                System.Net.CookieCollection values = (System.Net.CookieCollection)value;
                                List<string> strings = new List<string>();
                                foreach (System.Net.Cookie cookie in values)
                                {
                                    strings.Add(string.Format("{0}={1}", cookie.Name, cookie.Value));
                                }
                                returnValue.AppendFormat("{0}: {1}{2}", property.Name, string.Join("\r\n\t", strings.ToArray()), separator);
                            }
                            else
                            {
                                returnValue.AppendFormat("{0}: [null]{1}", property.Name, separator);
                            }
                        }
                        else if (property.PropertyType == typeof(NameValueCollection))
                        {
                            object value = property.GetValue(obj, null);
                            if (value != null)
                            {
                                NameValueCollection values = (NameValueCollection)value;
                                List<string> strings = new List<string>();
                                foreach (string key in values.AllKeys)
                                {
                                    strings.Add(string.Format("{0}={1}", key, values[key]));
                                }
                                returnValue.AppendFormat("{0}: {1}{2}", property.Name, string.Join("\r\n\t", strings.ToArray()), separator);
                            }
                            else
                            {
                                returnValue.AppendFormat("{0}: [null]{1}", property.Name, separator);
                            }
                        }
                        else if (property.GetIndexParameters().Length == 0)
                        {
                            object value = property.GetValue(obj, null);
                            string stringValue = "[null]";
                            if (value != null)
                            {
                                if (value is IEnumerable values && !(value is string))
                                {
                                    List<string> strings = new List<string>();
                                    foreach (object o in values)
                                    {
                                        strings.Add(o.ToString());
                                    }
                                    stringValue = string.Join("\r\n\t", strings.ToArray());
                                }
                                else
                                {
                                    stringValue = value.ToString();
                                }
                            }

                            returnValue.AppendFormat("{0}: {1}{2}", property.Name, stringValue, separator);
                        }
                        else if (property.GetIndexParameters().Length > 0)
                        {
                            returnValue.AppendFormat("Indexed Property:{0}{1}", property.Name, separator);
                        }
                    }
                    catch (Exception ex)
                    {
                        returnValue.AppendFormat("{0}: ({1}){2}", property.Name, ex.Message, separator);
                    }
                }
                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                return $"Error Getting Properties: {ex.Message}";
            }
        }

        public static DirectoryInfo EnsureExists(this DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        public static FileInfo[] GetFiles(this DirectoryInfo parent, string[] searchPatterns, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            List<FileInfo> results = new List<FileInfo>();
            searchPatterns.Each(spattern =>
            {
                results.AddRange(parent.GetFiles(spattern, option));
            });
            return results.ToArray();
        }

        public static void Copy(this DirectoryInfo src, string destinationPath, bool overwrite = false, Action<string, string> beforeFileCopy = null, Action<string, string> beforeDirectoryCopy = null)
        {
            src.Copy(new DirectoryInfo(destinationPath), overwrite, beforeFileCopy, beforeDirectoryCopy);
        }

        public static void Copy(this DirectoryInfo src, DirectoryInfo destination, bool overwrite = false, Action<string, string> beforeFileCopy = null, Action<string, string> beforeDirectoryCopy = null)
        {
            CopyDirectory(src.FullName, destination.FullName, overwrite, beforeFileCopy, beforeDirectoryCopy);
        }

        public static void CopyDirectory(this string sourcePath, string destPath, bool overwrite = false, Action<string, string> beforeFileCopy = null, Action<string, string> beforeDirectoryCopy = null)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                beforeFileCopy?.Invoke(file, dest);
                File.Copy(file, dest, overwrite);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                beforeDirectoryCopy?.Invoke(folder, dest);
                CopyDirectory(folder, dest, overwrite, beforeFileCopy, beforeDirectoryCopy);
            }
        }

        static Dictionary<string, object> fileAccessLocks = new Dictionary<string, object>();

        /// <summary>
        /// Returns the content of the file refferred to by the current
        /// string instance.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SafeReadFile(this string filePath)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }
            
            lock (FileLock.Named(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static byte[] SafeReadFileBytes(this string filePath)
        {            
            if (!File.Exists(filePath))
            {
                return new byte[]{ };
            }
            
            lock (FileLock.Named(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
        }

        /// <summary>
        /// Write the specified textToWrite to the current filePath
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="textToWrite"></param>
        /// <param name="postWriteAction"></param>
        public static void SafeWriteFile(this string filePath, string textToWrite, Action<object> postWriteAction = null)
        {
            SafeWriteFile(filePath, textToWrite, false, postWriteAction);
        }

        /// <summary>
        /// Write the current textToWrite to the specified filePath
        /// </summary>
        /// <param name="textToWrite"></param>
        /// <param name="filePath"></param>
        /// <param name="postWriteAction"></param>
        public static void SafeWriteToFile(this string textToWrite, string filePath, Action<object> postWriteAction = null)
        {
            filePath.SafeWriteFile(textToWrite, postWriteAction);
        }

        public static void SafeWriteToFile(this string textToWrite, string filePath, bool overwrite, Action<object> postWriteAction = null)
        {
            filePath.SafeWriteFile(textToWrite, overwrite, postWriteAction);
        }

        /// <summary>
        /// Write the specified text to the specified file in a thread safe way.
        /// </summary>
        /// <param name="filePath">The path to the file to write.</param>
        /// <param name="textToWrite">The text to write.</param>
        /// <param name="overwrite">True to overwrite.  If false and the file exists an InvalidOperationException will be thrown.</param>
        public static void SafeWriteFile(this string filePath, string textToWrite, bool overwrite, Action<object> postWriteAction = null)
        {
            FileInfo fileInfo = HandleExisting(filePath, overwrite);

            lock (FileLock.Named(fileInfo.FullName))
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(textToWrite);
                }
            }

            postWriteAction?.Invoke(fileInfo);
        }

        public static void SafeWriteFileBytes(this string filePath, byte[] bytesToWrite, bool overwrite, Action<object> postWriteAction = null)
        {
            SafeWriteFileBytes(filePath, bytesToWrite, 0, overwrite, postWriteAction);
        }

        public static void SafeWriteFileBytes(this string filePath, byte[] bytesToWrite, long startIndex, bool overwrite, Action<object> postWriteAction = null)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if(startIndex == 0)
            {
                fileInfo = HandleExisting(filePath, overwrite);
            }

            lock (FileLock.Named(fileInfo.FullName))
            {
                using (FileStream sw = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    sw.Seek(startIndex, SeekOrigin.Begin);
                    sw.Write(bytesToWrite, 0, bytesToWrite.Length);
                }
            }

            postWriteAction?.Invoke(fileInfo);
        }

        private static void EnsureLockObject(string filePath)
        {
            if (!fileAccessLocks.ContainsKey(filePath))
            {
                fileAccessLocks.Add(filePath, new object());
            }
        }

        private static FileInfo HandleExisting(string filePath, bool overwrite)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!Directory.Exists(fileInfo.Directory.FullName))
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

            if (File.Exists(fileInfo.FullName) && !overwrite)
            {
                throw new InvalidOperationException("File already exists and 'overwrite' parameter was false");
            }

            return fileInfo;
        }

        /// <summary>
        /// Appends the specified text to the specified file in a thread safe way.
        /// If the file doesn't exist it will be created.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="textToAppend"></param>
        public static void SafeAppendToFile(this string textToAppend, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            
            lock (FileLock.Named(fileInfo.FullName))
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.Write(textToAppend);
                    sw.Flush();
                }
            }
        }

        /// <summary>
        /// Clears the locks created for writing and appending
        /// to files
        /// </summary>
        public static void ClearFileAccessLocks(this object any)
        {
            FileLock.ClearLocks();
        }

        /// <summary>
        /// Adds the specified value if the specified key has not been added
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddMissing<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Set the value for the specified key in the dictionary in a way that won't 
        /// throw an exception if the key isn't already there
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                AddMissing(dictionary, key, value);
            }
            else
            {
                dictionary[key] = value;
            }
        }

        public static long Smallest(this IEnumerable<long> longs)
        {
            return longs.ToArray().Smallest();
        }

        public static long Smallest(this long[] longs)
        {
            if (longs.Length == 0)
            {
                return -1;
            }
            long smallest = longs[0];
            longs.Each(l => smallest = l < smallest ? l : smallest);
            return smallest;
        }

        public static long Biggest(this IEnumerable<long> longs)
        {
            return longs.ToArray().Largest();
        }

        public static long Largest(this IEnumerable<long> longs)
        {
            return longs.ToArray().Largest();
        }

        public static long Largest(this long[] longs)
        {
            if (longs.Length == 0)
            {
                return -1;
            }
            long largest = longs[0];
            longs.Each(l => largest = l > largest ? l : largest);
            return largest;
        }

        public static T Largest<T>(this T[] values)
        {
            if (values.Length == 0)
            {
                return default(T);
            }
            T result = values[0];
            values.Each(s => result = s.ToString().CompareTo(result.ToString()) == 1 ? s : result);
            return result;
        }

        public static string Largest(this string[] strings)
        {
            if(strings.Length == 0)
            {
                return string.Empty;
            }
            string result = strings[0];
            strings.Each(s => result = s.CompareTo(result) == 1 ? s : result);
            return result;
        }

        /// <summary>
        /// Splits the specified text at capital letters inserting the specified separator.
        /// </summary>
        /// <param name="stringToPascalSplit"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string PascalSplit(this string stringToPascalSplit, string separator)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < stringToPascalSplit.Length; i++)
            {
                char next = stringToPascalSplit[i];
                if (i == 0 && char.IsLower(next))
                {
                    next = char.ToUpper(next);
                }

                if (char.IsUpper(next) && i > 0)
                {
                    returnValue.Append(separator);
                }

                returnValue.Append(next);
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// Returns a camel cased string from the specified string using the specified 
        /// separators.  For example, the input "The quick brown fox jumps over the lazy
        /// dog" with the separators of "new string[]{" "}" should return the string 
        /// "theQuickBrownFoxJumpsOverTheLazyDog".
        /// </summary>
        /// <param name="stringToCamelize">The string to camelize.</param>
        /// <param name="preserveInnerUppers">if set to <c>true</c> [preserve inner uppers].</param>
        /// <param name="separators">The separators.</param>
        /// <returns></returns>
        public static string CamelCase(this string stringToCamelize, bool preserveInnerUppers = true, params string[] separators)
        {
            if (stringToCamelize.Length > 0)
            {
                string pascalCase = stringToCamelize.PascalCase(preserveInnerUppers, separators);
                string camelCase = string.Format("{0}{1}", pascalCase[0].ToString().ToLowerInvariant(), pascalCase.Remove(0, 1));
                return camelCase;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Return an acronym for the specified string using the 
        /// captial letters in the string
        /// </summary>
        /// <param name="stringToAcronymize"></param>
        /// <param name="alwaysUseFirst"></param>
        /// <returns></returns>
        public static string CaseAcronym(this string stringToAcronymize, bool alwaysUseFirst = true)
        {
            if (stringToAcronymize?.Length > 0)
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < stringToAcronymize.Length; i++)
                {
                    char current = stringToAcronymize[i];
                    if (i == 0 && alwaysUseFirst || char.IsUpper(current))
                    {
                        result.Append(current.ToString().ToUpperInvariant());
                    }
                }
                return result.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns a pascal cased string from the specified string using the specified 
        /// separators.  For example, the input "The quick brown fox jumps over the lazy
        /// dog" with the separators of "new string[]{" "}" should return the string 
        /// "TheQuickBrownFoxJumpsOverTheLazyDog".
        /// </summary>
        /// <param name="stringToPascalize"></param>
        /// <param name="preserveInnerUppers">If true uppercase letters that appear in 
        /// the middle of a word remain uppercase if false they are converted to lowercase.</param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static string PascalCase(this string stringToPascalize, bool preserveInnerUppers = true, params string[] separators)
        {
            string[] splitString = stringToPascalize.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string retVal = string.Empty;
            foreach (string part in splitString)
            {
                string firstChar = part[0].ToString().ToUpper();
                retVal += firstChar;
                for (int i = 1; i < part.Length; i++)
                {
                    if (!preserveInnerUppers)
                    {
                        retVal += part[i].ToString().ToLowerInvariant();
                    }
                    else
                    {
                        retVal += part[i].ToString();
                    }
                }
            }

            return retVal;
        }

        public static string PrefixWithUnderscoreIfStartsWithNumber(this string targetString)
        {
            return targetString.StartsWithNumber() ? "_{0}"._Format(targetString) : targetString;
        }

        public static bool StartsWithNumber(this string targetString)
        {
            return targetString[0].IsNumber();
        }

        public static string LettersOnly(this string targetString)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in targetString)
            {
                if (c.IsLetter())
                {
                    result.Append(c.ToString());
                }
            }

            return result.ToString();
        }

        public static bool IsNumber(this char c)
        {
            int val = Convert.ToInt32(c);
            return (val > 47 && val < 58);
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        public static bool IsLetter(this char c)
        {
            int val = Convert.ToInt32(c);
            return (val > 96 && val < 123) || (val > 64 && val < 91);
        }

        public static bool IsAllCaps(this string value)
        {
            bool result = true;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                result = char.IsUpper(c);
                if (!result)
                {
                    break;
                }
            }
            return result;
        }

        public static string TrimNonLetters(this string targetString)
        {
            return targetString.DropLeadingNonLetters().DropTrailingNonLetters();
        }

        public static string DropTrailingNonLetters(this string targetString)
        {
            StringBuilder temp = new StringBuilder();
            bool foundLetter = false;
            for (int i = targetString.Length - 1; i >= 0; i--)
            {
                char c = targetString[i];
                if (c.IsLetter())
                {
                    foundLetter = true;
                }

                if (foundLetter)
                {
                    temp.Append(c);
                }
            }

            StringBuilder result = new StringBuilder();
            temp.ToString().ToCharArray().Reverse().ToArray().Each(c => result.Append(c));
            return result.ToString();
        }

        public static string DropLeadingNonLetters(this string targetString)
        {
            StringBuilder result = new StringBuilder();
            bool foundLetter = false;
            for (int i = 0; i < targetString.Length; i++)
            {
                char c = targetString[i];
                if (c.IsLetter())
                {
                    foundLetter = true;
                }

                if (foundLetter)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public static bool StartsWithLetter(this string theString)
        {
            if (string.IsNullOrEmpty(theString))
            {
                return false;
            }
            return theString[0].IsLetter();
        }

        /// <summary>
        /// Intended to delimit the specified array of T using the
        /// specified ToDelimitedDelegate.  Each item will be represented
        /// by the return value of the specified ToDelimitedDelegate.
        /// </summary>
        /// <typeparam name="T">The type of objects in the specified array</typeparam>
        /// <param name="objectsToStringify">The objects</param>
        /// <param name="toDelimiteder">The ToDelimitedDelegate used to represent each object</param>
        /// <returns>string</returns>
        public static string ToDelimited<T>(this T[] objectsToStringify, ToDelimitedDelegate<T> toDelimiteder)
        {
            return ToDelimited(objectsToStringify, toDelimiteder, ", ");
        }

        /// <summary>
        /// Intended to delimit the specified array of T using the
        /// specified ToDelimitedDelegate.  Each item will be represented
        /// by the return value of the specified ToDelimitedDelegate.
        /// </summary>
        /// <typeparam name="T">The type of objects in the specified array</typeparam>
        /// <param name="objectsToStringify">The objects</param>
        /// <param name="toDelimiteder">The ToDelimitedDelegate used to represent each object</param>
        /// <returns>string</returns>
        public static string ToDelimited<T>(this T[] objectsToStringify, ToDelimitedDelegate<T> toDelimiteder, string delimiter)
        {
            List<string> values = new List<string>();
            objectsToStringify.Each(v =>
            {
                values.Add(toDelimiteder(v));
            });
            return string.Join(delimiter, values.ToArray());
        }

        public static string[] SemiColonSplit(this string semicolonSeparatedValues)
        {
            return DelimitSplit(semicolonSeparatedValues, ";");
        }

        public static string[] DelimitSplit(this string valueToSplit, string delimiter)
        {
            return DelimitSplit(valueToSplit, new string[] { delimiter });
        }

        public static string[] DelimitSplit(this string valueToSplit, params string[] delimiters)
        {
            return DelimitSplit(valueToSplit, delimiters, false);
        }

        public static string[] DelimitSplit(this string valueToSplit, string delimiter, bool trimValues)
        {
            return DelimitSplit(valueToSplit, new string[] { delimiter }, trimValues);
        }

        public static string DelimitedReplace(this string input, string toReplace, string replaceWith, string startDelimiter = "$$~", string endDelimiter = "~$$")
        {
            return DelimitedReplace(input, new Dictionary<string, string> { { toReplace, replaceWith } }, startDelimiter, endDelimiter);
        }
        
        public static string DelimitedReplace(this string input, Dictionary<string, string> replacements, string startDelimiter = "$$~", string endDelimiter = "~$$")
        {
            StringBuilder result = new StringBuilder();
            StringBuilder innerValue = new StringBuilder();
            bool replacing = false;
            foreach (char c in input)
            {
                if (replacing)
                {
                    innerValue.Append(c);
                    string innerSoFar = innerValue.ToString();
                    foreach(string toReplace in replacements.Keys)
                    {
                        if (innerSoFar.EndsWith(toReplace))
                        {
                            StringBuilder tmp = new StringBuilder();
                            tmp.Append(innerSoFar.Truncate(toReplace.Length));
                            tmp.Append(replacements[toReplace]);
                            innerValue = tmp;
                        }
                    }

                    if (innerValue.ToString().EndsWith(endDelimiter))
                    {
                        replacing = false;
                        result.Append(innerValue.ToString().Truncate(endDelimiter.Length));
                        innerValue = new StringBuilder();
                    }
                }
                else
                {
                    result.Append(c);
                    string soFar = result.ToString();
                    if (soFar.EndsWith(startDelimiter))
                    {
                        replacing = true;
                        StringBuilder tmp = new StringBuilder();
                        tmp.Append(result.ToString().Truncate(startDelimiter.Length));
                        result = tmp;
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Split the string on the specified delimiters removing empty entries
        /// and optionally trimming each value
        /// </summary>
        /// <param name="valueToSplit"></param>
        /// <param name="delimiters"></param>
        /// <param name="trimValues"></param>
        /// <returns></returns>
        public static string[] DelimitSplit(this string valueToSplit, string[] delimiters, bool trimValues)
        {
            string[] split = valueToSplit.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            if (trimValues)
            {
                for (int i = 0; i < split.Length; i++)
                {
                    split[i] = split[i].Trim();
                }
            }

            return split;
        }

        public static bool IsEnumerable(this PropertyInfo property)
        {
            return property.PropertyType.IsEnumerable();
        }

        public static bool IsEnumerable(this Type type)
        {
            if (type == typeof(string))
            {
                return false; // it is but that's not what we're looking for
            }
            return type.IsArray ||
                typeof(IEnumerable).IsAssignableFrom(type) ||
                type.GetInterface(typeof(IEnumerable<>).FullName) != null;
        }

        /// <summary>
        /// Get the type of the array or enumerable of the specified 
        /// property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Type GetEnumerableType(this PropertyInfo property)
        {
            Type result = null;
            if (property.PropertyType.IsArray)
            {
                result = property.PropertyType.GetElementType();
            }
            else if (property.PropertyType != typeof(string) && property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null)
            {
                result = property.PropertyType.GetInterfaces()
                    .Where(t => t.IsGenericType == true && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    .Select(t => t.GetGenericArguments()[0]).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// Returns true if the specified toCheck type has 
        /// an enumerable property that is of the current type
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        public static bool HasEnumerableOfMe(this Type self, Type toCheck)
        {
            PropertyInfo ignore;
            return HasEnumerableOfMe(self, toCheck, out ignore);
        }

        /// <summary>
        /// Returns true if the specified toCheck type has 
        /// an enumerable property that is of the current type
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        public static bool HasEnumerableOfMe(this Type self, Type toCheck, out PropertyInfo enumerableProperty)
        {
            bool result = false;
            enumerableProperty = null;
            foreach (PropertyInfo property in toCheck.GetProperties())
            {
                Type enumerableType = property.GetEnumerableType();
                if (enumerableType != null)
                {
                    if (enumerableType == self)
                    {
                        enumerableProperty = property;
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Determines if the method is a special property method
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsProperty(this MethodInfo method)
        {
            return method.GetProperty() != null;
        }

        public static PropertyInfo GetProperty(this MethodInfo method)
        {
            if (!method.IsSpecialName) return null;
            string propertyName = method.Name.Substring(4);
            PropertyInfo p = method.DeclaringType.GetProperty(propertyName);

            return p;
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return BuildDynamicType<PropertyAttributeFilter>(objectToClone, out ignore);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="typeToClone"></param>
        /// <returns></returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this Type typeToClone) where PropertyAttributeFilter : Attribute, new()
        {
            return BuildDynamicType<PropertyAttributeFilter>(typeToClone, out AssemblyBuilder ignore, false);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <param name="concreteAttribute">If true the attributes must be of the specified type and not extenders of the type.</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            return BuildDynamicType<PropertyAttributeFilter>(objectToClone, out AssemblyBuilder ignore, concreteAttribute);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder) where PropertyAttributeFilter : Attribute, new()
        {
            return BuildDynamicType<PropertyAttributeFilter>(objectToClone, out assemblyBuilder, false);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <param name="concreteAttribute">If true the attributes must be of the specified type and not extenders of the type.</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            Type objType = objectToClone.GetType();
            return BuildDynamicType<PropertyAttributeFilter>(objType, out assemblyBuilder, concreteAttribute);
        }

        public static Type BuildDynamicType<PropertyAttributeFilter>(this Type objType, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            lock (_buildDynamicTypeLock)
            {
                string typeName = objType.Namespace + "." + objType.Name;
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName].DynamicType != null)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    TypeBuilder typeBuilder;
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

                    foreach (PropertyInfo property in objType.GetProperties())
                    {
                        PropertyAttributeFilter attr;
                        if (CustomAttributeExtension.HasCustomAttributeOfType<PropertyAttributeFilter>(property, true, out attr, concreteAttribute))
                        {
                            AddPropertyToDynamicType(typeBuilder, property);
                        }
                    }
                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        /// <summary>
        /// Converts a DataRow to a dynamic instance where the proeprty names
        /// are the names of the columns in the row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataRow row)
        {
            string typeName = 8.RandomLetters();
            if (row.Table != null)
            {
                typeName = row.Table.TableName.Or(typeName);
            }

            return ToDynamic(row, typeName);
        }

        /// <summary>
        /// Converts a DataRow to a dynamic object instance.  
        /// </summary>
        /// <param name="row">The row to convert</param>
        /// <param name="typeName">The name of the type to use in reflection operations</param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataRow row, string typeName)
        {
            AssemblyBuilder ignore;
            Type dynamicType = ToDynamicType(row, typeName, out ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object instance = ctor.Invoke(null);
            instance.CopyValues(row);
            return instance;
        }

        /// <summary>
        /// Creates a dynamic object from the specified instance populating only
        /// the properties that are of value types
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static dynamic ValuePropertiesToDynamic(this object instance)
        {
            return ValuePropertiesToDynamic(instance, out Type ignore);
        }

        /// <summary>
        /// Creates a dynamic object from the specified instance populating only
        /// the properties that are of value types
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static dynamic ValuePropertiesToDynamic(this object instance, out Type dynamicType)
        {
            Type instanceType = instance.GetType();
            string newTypeName = "ValuesOf.{0}.{1}"._Format(instanceType.Namespace, instanceType.Name);
            dynamicType = ValuePropertiesToDynamicType(instance, newTypeName, out AssemblyBuilder ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object valuesOnlyInstance = ctor.Invoke(null);
            DefaultConfiguration.CopyProperties(instance, valuesOnlyInstance);
            return valuesOnlyInstance;
        }

        /// <summary>
        /// Copies all the subscribed event handlers from source to
        /// the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object CopyEventHandlers(this object destination, object source)
        {
            GetEventSubscriptions(source).Each(es =>
            {
                es.EventInfo.AddEventHandler(destination, es.Delegate);
            });
            return destination;
        }

        /// <summary>
        /// Gets all the subscribed event subscriptions for the specified event name
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static IEnumerable<EventSubscription> GetEventSubscriptions(this object instance, string eventName)
        {
            return GetEventSubscriptions(instance).Where(es => es.EventInfo.Name.Equals(eventName));
        }

        /// <summary>
        /// Gets All the subscribed event subscriptions
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IEnumerable<EventSubscription> GetEventSubscriptions(this object instance)
        {
            Type type = instance.GetType();
            Func<EventInfo, FieldInfo> ei2fi =
            ei => type.GetField(ei.Name,
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetField);

            // ** yuck **
            IEnumerable<EventSubscription> results = from eventInfo in type.GetEvents()
                                                     let eventFieldInfo = ei2fi(eventInfo)
                                                     let eventFieldValue =
                                                         (System.Delegate)eventFieldInfo?.GetValue(instance)
                                                     from subscribedDelegate in eventFieldValue == null ? new Delegate[] { } : eventFieldValue.GetInvocationList()
                                                     select new EventSubscription { EventName = eventFieldInfo.Name, Delegate = subscribedDelegate, FieldInfo = eventFieldInfo, EventInfo = eventInfo };
            // ** /yuck **
            return results;
        }

        /// <summary>
        /// Copies all properties from source to destination where the name and
        /// type match.  Accounts for nullability and treats non nullable and
        /// nullable primitives as compatible
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object CopyProperties(this object destination, object source)
        {
            if (destination == null || source == null)
            {
                return destination;
            }

            ForEachProperty(destination, source, CopyProperty);

            return destination;
        }

        /// <summary>
        /// Same as CopyProperties but will clone properties
        /// whos type implements ICloneable
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object CloneProperties(this object destination, object source)
        {
            if (destination == null || source == null)
            {
                return destination;
            }

            ForEachProperty(destination, source, CloneProperty);

            return destination;
        }

        private static void ForEachProperty(object destination, object source, Action<object, object, PropertyInfo, PropertyInfo> action)
        {
            Type destinationType = destination.GetType();
            Type sourceType = source.GetType();

            foreach (PropertyInfo destProp in destinationType.GetProperties())
            {
                PropertyInfo sourceProp = sourceType.GetProperty(destProp.Name);
                action(destination, source, destProp, sourceProp);
            }
        }

        /// <summary>
        /// Copy the value of the specified property from the source
        /// to the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        public static void CopyProperty(this object destination, object source, string propertyName)
        {
            PropertyInfo destProp = destination.GetType().GetProperty(propertyName);
            PropertyInfo sourceProp = source.GetType().GetProperty(propertyName);
            destination.CopyProperty(source, destProp, sourceProp);
        }

        internal static void CopyProperty(this object destination, object source, PropertyInfo destProp, PropertyInfo sourceProp)
        {
            if (sourceProp != null)
            {
                if (destProp.IsCompatibleWith(sourceProp))
                {
                    object value = sourceProp.GetValue(source, null);
                    destProp.SetValue(destination, value, null);
                }
            }
        }

        internal static void CloneProperty(this object destination, object source, PropertyInfo destProp, PropertyInfo sourceProp)
        {
            if(sourceProp != null)
            {
                if (destProp.IsCompatibleWith(sourceProp))
                {
                    object value = sourceProp.GetValue(source, null);
                    if (value is ICloneable cloneable)
                    {
                        value = cloneable.Clone();
                    }
                    destProp.SetValue(destination, value, null);
                }
            }
        }

        public static bool IsCompatibleWith(this PropertyInfo prop, PropertyInfo other)
        {
            return AreCompatibleProperties(prop, other);
        }

        public static bool AreCompatibleProperties(PropertyInfo destProp, PropertyInfo sourceProp)
        {
            return (sourceProp.PropertyType == destProp.PropertyType ||
                                sourceProp.PropertyType == Nullable.GetUnderlyingType(destProp.PropertyType) ||
                                Nullable.GetUnderlyingType(sourceProp.PropertyType) == destProp.PropertyType)
                                && destProp.CanWrite;
        }

        public static DateTime WithoutMilliseconds(this DateTime dateTime)
        {
            return DropMilliseconds(dateTime);
        }

        public static DateTime DropMilliseconds(this DateTime dateTime)
        {
            Instant instant = new Instant(dateTime);
            instant.Millisecond = 0;
            return instant.ToDateTime();
        }

        public static Instant ToInstant(this DateTime dateTime)
        {
            return new Instant(dateTime);
        }

        public static T As<T>(this DataRow row) where T : new()
        {
            T result = new T();
            return (T)CopyValues(result, row);
        }

        public static object CopyValues(this object destination, DataRow row)
        {
            Type destinationType = destination.GetType();

            foreach (PropertyInfo destProp in destinationType.GetProperties())
            {
                if (destProp.PropertyType != typeof(DBNull))
                {
                    object value = row[destProp.Name];
                    if (value != null && value != DBNull.Value)
                    {
                        destProp.SetValue(destination, value, null);
                    }
                }
            }

            return destination;
        }

        public static IEnumerable<T> DataClone<T>(this IEnumerable<T> values) where T : new()
        {
            return values.Select(t => t.DataClone());
        }

        /// <summary>
        /// Clone the specified instance to a dynamic object instance
        /// copying only properties
        /// that are represented in the Bam.Net.Data.Schema.DataTypes enum
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object ToDynamicData(this object instance)
        {
            return ToDynamicData(instance, instance.GetType().Name);
        }

        /// <summary>
        /// Clone the specified instance copying only properties
        /// that are represented in the Bam.Net.Data.Schema.DataTypes enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T DataClone<T>(this T instance) where T : new()
        {
            T result = new T();
            object temp = ToDynamicData(instance, nameof(T));
            temp.CopyProperties(instance);
            result.CopyProperties(temp);
            return result;
        }

        public static object DataClone(this object instance, Type type)
        {
            object result = type.Construct();
            object temp = ToDynamicData(instance, type.Name);
            result.CopyProperties(temp);
            return result;
        }

        public static object ToDynamicData(this object instance, string typeName)
        {
            Type type = instance.ToDynamicType(typeName, DataTypeFilter);
            object temp = type.Construct();
            temp.CopyProperties(instance);
            return temp;
        }

        public static object ToDynamicData(this Dictionary<string, object> instance, string typeName)
        {
            Type type = instance.ToDynamicType(typeName);
            object temp = type.Construct();
            foreach(string key in instance.Keys)
            {
                temp.Property(key, instance[key]);
            }
            return temp;
        }

        private static bool DataTypeFilter(PropertyInfo prop)
        {
            return prop.PropertyType == typeof(string) ||
                        prop.PropertyType == typeof(bool) ||
                        prop.PropertyType == typeof(long) ||
                        prop.PropertyType == typeof(long?) ||
                        prop.PropertyType == typeof(int) ||
                        prop.PropertyType == typeof(int?) ||
                        prop.PropertyType == typeof(bool?) ||
                        prop.PropertyType == typeof(decimal) ||
                        prop.PropertyType == typeof(decimal?) ||
                        prop.PropertyType == typeof(byte[]) ||
                        prop.PropertyType == typeof(DateTime) ||
                        prop.PropertyType == typeof(DateTime?);
        }

        public static object ValuePropertiesToDynamicInstance(this Type type, out AssemblyBuilder assemblyBuilder)
        {
            object instance = type.Construct();
            Type dynamicType = instance.ValuePropertiesToDynamicType(type.Name, out assemblyBuilder, false);
            return dynamicType.Construct();
        }

        public static Type ValuePropertiesToDynamicType(this object instance, string typeName, bool useCache = true)
        {
            AssemblyBuilder ignore;
            return ValuePropertiesToDynamicType(instance, typeName, out ignore, useCache);
        }

        public static Type ValuePropertiesToDynamicType(this object instance, string typeName, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            return instance.ToDynamicType(typeName, p => p.PropertyType.IsValueType, out assemblyBuilder, useCache);
        }

        public static PropertyInfo[] GetValueProperties(this object instance)
        {
            return instance.GetType().GetProperties().Where(pi => pi.PropertyType.IsValueType).ToArray();
        }

        /// <summary>
        /// Combines the current instance with the specified toMerge values
        /// creating a new type with all the properties of each and value 
        /// set to the last one in
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        public static object Combine(this object instance, params object[] toMerge)
        {
            Type combinedType = CombineToDynamicType(instance, toMerge);
            object newInstance = combinedType.Construct();
            newInstance.CopyProperties(instance);
            toMerge.Each(obj =>
            {
                newInstance.CopyProperties(obj);
            });
            return newInstance;
        }

        static object _buildDynamicTypeLock = new object();
        public static Type CombineToDynamicType(this object instance, params object[] toMerge)
        {
            return CombineToDynamicType(instance, "DynamicType_".RandomLetters(8), toMerge);
        }

        public static Type CombineToDynamicType(this object instance, string typeName, params object[] toMerge)
        {
            lock (_buildDynamicTypeLock)
            {
                GetAssemblyAndTypeBuilder(typeName, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder);
                List<object> all = new List<object>();
                all.Add(instance);
                all.AddRange(toMerge);
                all.Each(obj =>
                {
                    Type type = obj.GetType();
                    AddPropertiesToDynamicType(typeBuilder, type);
                });

                return CreateDynamicType(typeName, typeBuilder);
            }
        }

        /// <summary>
        /// Create a dynamic type for the object with the specified typeName
        /// using the specified propertyPredicate to determine what properties
        /// of the original type to include
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="typeName"></param>
        /// <param name="propertyPredicate"></param>
        /// <returns></returns>
        public static Type ToDynamicType(this object instance, string typeName, Func<PropertyInfo, bool> propertyPredicate)
        {
            return ToDynamicType(instance, typeName, propertyPredicate, out AssemblyBuilder ignore);
        }

        /// <summary>
        /// Create a dynamic type for the object with the specified typeName
        /// using the specified propertyPredicate to determine what properties
        /// of the original type to include
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="typeName"></param>
        /// <param name="propertyPredicate"></param>
        /// <param name="assemblyBuilder"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public static Type ToDynamicType(this object instance, string typeName, Func<PropertyInfo, bool> propertyPredicate, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            lock (_buildDynamicTypeLock)
            {
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out TypeBuilder typeBuilder);

                    Type actualType = instance.GetType();
                    PropertyInfo[] properties = actualType.GetProperties();
                    properties.Each(p =>
                    {
                        if (propertyPredicate(p))
                        {
                            AddPropertyToDynamicType(typeBuilder, p);
                        }
                    });

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        public static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, List<Type> created)
        {
            return ToDynamicType(dictionary, typeName, 0, created, out AssemblyBuilder ignore, false);
        }

        public static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, bool useCache = true)
        {
            return ToDynamicType(dictionary, typeName, 0, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, bool useCache = true)
        {
            return dictionary.ToDynamicType(typeName, recursionThusFar, out AssemblyBuilder ignore, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            return ToDynamicType(dictionary, typeName, recursionThusFar, new List<Type>(), out assemblyBuilder, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, List<Type> createdTypes, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            ThrowIfLimitReached(recursionThusFar);

            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
            {
                return GetExistingDynamicType(typeName, out assemblyBuilder);
            }
            else
            {
                return BuildDynamicType(dictionary, typeName, recursionThusFar, createdTypes, out assemblyBuilder);
            }
        }

        private static ConstructorInfo GetConstructor(Type type, object[] ctorParams)
        {
            List<Type> paramTypes = new List<Type>();
            foreach (object o in ctorParams)
            {
                paramTypes.Add(o.GetType());
            }

            ConstructorInfo ctor = type.GetConstructor(paramTypes.ToArray());
            return ctor;
        }

        private static Type BuildDynamicType(Dictionary<object, object> dictionary, string typeName, int recursionThusFar, List<Type> createdTypes, out AssemblyBuilder assemblyBuilder)
        {
            lock (_buildDynamicTypeLock)
            {
                TypeBuilder typeBuilder;
                GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder, false);

                foreach (object key in dictionary.Keys)
                {
                    string propertyName = key as string;
                    if (propertyName == null)
                    {
                        Args.Throw<InvalidOperationException>("Key was ({0}), expected string", key.GetType().Name);
                    }
                    propertyName = propertyName.PascalCase();
                    object value = dictionary[key];
                    if (value == null)
                    {
                        AddPropertyToDynamicType(typeBuilder, propertyName, typeof(string));
                    }
                    else
                    {
                        Type valueType = value.GetType();
                        if (valueType.IsArray)
                        {
                            AddPropertyToDynamicType(typeBuilder, propertyName, typeof(object[]));
                        }
                        else if (valueType == typeof(Dictionary<object, object>))
                        {
                            string childTypeName = "{0}_{1}"._Format(typeName, propertyName);
                            Type childType = ((Dictionary<object, object>)value).ToDynamicType(childTypeName, ++recursionThusFar, false);
                            createdTypes.Add(childType);
                            AddPropertyToDynamicType(typeBuilder, propertyName, childType);
                        }
                        else if (valueType.IsPrimitive || valueType == typeof(string) || valueType.IsValueType)
                        {
                            AddPropertyToDynamicType(typeBuilder, propertyName, valueType);
                        }
                    }
                }

                Type created = CreateDynamicType(typeName, typeBuilder);
                createdTypes.Add(created);
                return created;
            }
        }

        /// <summary>
        /// Convert a dictionary to an instance of a specified type.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="type">The type.</param>
        /// <param name="ctorParams">The ctor parameters.</param>
        /// <returns></returns>
        public static object FromDictionary(this Dictionary<object, object> dictionary, Type type, params object[] ctorParams)
        {
            object result = type.Construct(ctorParams);
            SetProperties(dictionary, result);
            return result;
        }

        /// <summary>
        /// Convert a dictionary to an instance of a specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="ctorParams">The ctor parameters.</param>
        /// <returns></returns>
        public static TResult FromDictionary<TKey, TValue, TResult>(this Dictionary<TKey, TValue> dictionary, params object[] ctorParams)
        {
            return FromDictionary<TKey, TValue, TResult>(dictionary, (k) => k.ToString(), (p, v) => v, ctorParams);
        }

        /// <summary>
        /// Convert a dictionary to an instance of a specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="keyMunger">The key munger.</param>
        /// <param name="valueMunger">The value munger.</param>
        /// <param name="ctorParams">The ctor parameters.</param>
        /// <returns></returns>
        public static TResult FromDictionary<TKey, TValue, TResult>(this Dictionary<TKey, TValue> dictionary, Func<TKey, string> keyMunger, Func<PropertyInfo, TValue, object> valueMunger, params object[] ctorParams)
        {
            TResult result = Construct<TResult>(typeof(TResult), ctorParams);
            foreach(TKey key in dictionary.Keys)
            {
                string propertyName = keyMunger(key);
                result.Property(propertyName, valueMunger(typeof(TResult).GetProperty(propertyName), dictionary[key]));
            }
            return result;
        }

        /// <summary>
        /// Convert the specified dicationary to an instance
        /// of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="ctorParams"></param>
        /// <returns></returns>
        public static T FromDictionary<T>(this Dictionary<object, object> dictionary, params object[] ctorParams)
        {
            T result = Construct<T>(typeof(T), ctorParams);
            SetProperties(dictionary, result);
            return result;
        }

        private static void SetProperties(Dictionary<object, object> dictionary, object result)
        {
            foreach (object key in dictionary.Keys)
            {
                result.Property(key.ToString(), dictionary[key]);
            }
        }

        public static Dictionary<string, T> ToDictionary<T>(this object instance, Func<PropertyInfo, string> keyMunger, Func<object, T> valueConverter)
        {
            Type dyn = instance.GetType();
            Dictionary<string, T> result = new Dictionary<string, T>();
            foreach (PropertyInfo prop in dyn.GetProperties())
            {
                string key = keyMunger(prop);
                result[key] = valueConverter(prop.GetValue(instance));
            }
            return result;

        }

        /// <summary>
        /// Intended to turn a dynamic object into a dictionary. For example,
        /// new { key1 = value1, key2 = value 2} becomes a dictionary with
        /// two keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="valueConverter">The value converter.</param>
        /// <returns></returns>
        public static Dictionary<string, T> ToDictionary<T>(this object instance, Func<object, T> valueConverter)
        {
            Type dyn = instance.GetType();
            Dictionary<string, T> result = new Dictionary<string, T>();
            foreach (PropertyInfo prop in dyn.GetProperties())
            {
                result[prop.Name] = valueConverter(prop.GetValue(instance));
            }
            return result;
        }

        /// <summary>
        /// Intended to turn a dynamic object into a dictionary. For example,
        /// new { key1 = value1, key2 = value 2} becomes a dictionary with
        /// two keys.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this object instance)
        {
            Type dyn = instance.GetType();
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (PropertyInfo prop in dyn.GetProperties())
            {
                result[prop.Name] = prop.GetValue(instance);
            }
            return result;
        }

        public static Type MergeToDynamicType(this Type type, params Type[] types)
        {
            List<object> instances = new List<object>();
            instances.Add(type.Construct());
            types.Each(t =>
            {
                instances.Add(t.Construct());
            });
            return instances.MergeToDynamicType(type.Name, 0, true);
        }

        public static Type MergeToDynamicType(this List<object> objects, string typeName, int recursionThusFar, bool useCache = true)
        {
            return MergeToDynamicType(objects, typeName, recursionThusFar, out AssemblyBuilder ignore, useCache);
        }

        public static Type MergeToDynamicType(this List<object> objects, string typeName, int recursionThusFar, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            lock (_buildDynamicTypeLock)
            {
                ThrowIfLimitReached(recursionThusFar);

                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    TypeBuilder typeBuilder;
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

                    // foreach object get the type
                    foreach (object obj in objects)
                    {
                        Type currentType = obj.GetType();
                        // if it's a Dictionary<object, object> use ToDynamicType to get a type representing it
                        if (currentType == typeof(Dictionary<object, object>))
                        {
                            Type dynamicDictionaryType = ((Dictionary<object, object>)obj).ToDynamicType(typeName, ++recursionThusFar, false);
                            AddPropertiesToDynamicType(typeBuilder, dynamicDictionaryType);
                        }
                        else if (currentType.IsArray)// if it's an array increment recursion and call self
                        {
                            Type mergedArrayType = ((object[])obj).ToList().MergeToDynamicType(typeName, ++recursionThusFar, false);
                            AddPropertiesToDynamicType(typeBuilder, mergedArrayType);
                        }
                        else// otherwise add the valueproperties					
                        {
                            Type valueProps = obj.ValuePropertiesToDynamicType(typeName, false);
                            AddPropertiesToDynamicType(typeBuilder, valueProps);
                        }
                    }

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        /// <summary>
        /// Convert the table into a list of dynamic objects
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<dynamic> ToDynamicList(this DataTable table)
        {
            return ToDynamicList(table, table.TableName.Or(8.RandomLetters()));
        }

        /// <summary>
        /// Conver the table into a list of dynamic objects with the specified typeName
        /// used for reflection
        /// </summary>
        /// <param name="table"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static List<dynamic> ToDynamicList(this DataTable table, string typeName)
        {
            List<dynamic> instances = new List<dynamic>();
            foreach (DataRow row in table.Rows)
            {
                instances.Add(row.ToDynamic(typeName));
            }

            return instances;
        }

        public static IEnumerable<dynamic> ToDynamicEnumerable(this DataTable table, string typeName = null)
        {
            typeName = typeName ?? table.TableName.Or(8.RandomLetters());
            foreach (DataRow row in table.Rows)
            {
                yield return row.ToDynamic(typeName);
            }
        }

        public static Type ToDynamicType(this DataRow row)
        {
            string typeName = 8.RandomLetters();
            if (row.Table != null)
            {
                typeName = row.Table.TableName.Or(typeName);
            }

            return ToDynamicType(row, typeName, out AssemblyBuilder ignore);
        }

        public static Type ToDynamicType(this DataRow row, string typeName, out AssemblyBuilder assemblyBuilder)
        {
            lock (_buildDynamicTypeLock)
            {
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out TypeBuilder typeBuilder);

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        CustomPropertyInfo propInfo = new CustomPropertyInfo(column.ColumnName, row[column].GetType());
                        AddPropertyToDynamicType(typeBuilder, propInfo);
                    }

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        public static Type ToDynamicType(this Dictionary<string, object> dictionary, string typeName)
        {
            return ToDynamicType(dictionary, typeName, out AssemblyBuilder ignore);
        }

        public static Type ToDynamicType(this Dictionary<string, object> dictionary, string typeName, out AssemblyBuilder assemblyBuilder)
        {
            lock (_buildDynamicTypeLock)
            {
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out TypeBuilder typeBuilder);

                    foreach (string propertyName in dictionary.Keys)
                    {
                        CustomPropertyInfo propInfo = new CustomPropertyInfo(propertyName, dictionary[propertyName].GetType());
                        AddPropertyToDynamicType(typeBuilder, propInfo);
                    }

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        public static Type BuildDynamicType(this string typeName, params string[] propertyNames)
        {
            return BuildDynamicType(typeName, string.Empty, propertyNames);
        }

        public static Type BuildDynamicType(this string typeName, string nameSpace, params string[] propertyNames)
        {
            return BuildDynamicType(typeName, nameSpace, out AssemblyBuilder ignore, propertyNames);
        }

        public static Type BuildDynamicType(this string typeName, string nameSpace, out AssemblyBuilder assemblyBuilder, params string[] propertyNames)
        {
            lock (_buildDynamicTypeLock)
            {

                string fullName = string.IsNullOrWhiteSpace(nameSpace) ? typeName : $"{nameSpace}.{typeName}";
                if (DynamicTypeStore.Current.ContainsTypeInfo(fullName) && DynamicTypeStore.Current[fullName] != null)
                {
                    return GetExistingDynamicType(fullName, out assemblyBuilder);
                }
                else
                {
                    TypeBuilder typeBuilder;
                    GetAssemblyAndTypeBuilder(fullName, out assemblyBuilder, out typeBuilder);
                    foreach (string propertyName in propertyNames)
                    {
                        AddPropertyToDynamicType(typeBuilder, new CustomPropertyInfo(propertyName, typeof(object)));
                    }
                    return CreateDynamicType(fullName, typeBuilder);
                }
            }
        }

        public static void GetAssemblyAndTypeBuilder(string typeName, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder, bool useCache = true)
        {
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && useCache)
            {
                DynamicTypeInfo info = DynamicTypeStore.Current[typeName];
                assemblyBuilder = info.AssemblyBuilder;
                typeBuilder = info.TypeBuilder;
            }
            else
            {
                string name = typeName;
                AssemblyName assemblyName = new AssemblyName("Bam.Net.DynamicGenerator");
                assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");

                typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Public);

                DynamicTypeStore.Current.AddType(typeName, new DynamicTypeInfo { AssemblyBuilder = assemblyBuilder, TypeBuilder = typeBuilder, TypeName = typeName });
            }
        }

        private static void ThrowIfLimitReached(int recursionThusFar)
        {
            if (DynamicTypeInfo.RecursionLimit <= recursionThusFar)
            {
                throw new DynamicTypeRecursionLimitReachedException(DynamicTypeInfo.RecursionLimit);
            }
        }

        private static void AddPropertiesToDynamicType(TypeBuilder typeBuilder, Type type)
        {
            foreach (PropertyInfo prop in type.GetProperties())
            {
                AddPropertyToDynamicType(typeBuilder, prop);
            }
        }

        private static void AddPropertyToDynamicType(TypeBuilder typeBuilder, PropertyInfo p)
        {
            CustomPropertyInfo propInfo = new CustomPropertyInfo(p.Name, p.PropertyType);
            AddPropertyToDynamicType(typeBuilder, propInfo);
        }

        private static void AddPropertyToDynamicType(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            CustomPropertyInfo propInfo = new CustomPropertyInfo(propertyName, propertyType);
            AddPropertyToDynamicType(typeBuilder, propInfo);
        }
        
        private static Type CreateDynamicType(string typeName, TypeBuilder typeBuilder)
        {
            Type returnType = typeBuilder.CreateType();
            if (DynamicTypeStore.Current[typeName] == null)
            {
                throw new InvalidOperationException("DynamicTypeInfo was null");
            }
            DynamicTypeStore.Current[typeName].DynamicType = returnType;
            return returnType;
        }

        private static Type GetExistingDynamicType(string typeName, out AssemblyBuilder assemblyBuilder)
        {
            DynamicTypeInfo info = DynamicTypeStore.Current[typeName];
            assemblyBuilder = info.AssemblyBuilder;
            return info.DynamicType;
        }

        internal static void AddPropertyToDynamicType(TypeBuilder typeBuilder, _PropertyInfo property)
        {
            FieldBuilder propertyField = typeBuilder.DefineField("_" + property.Name.ToLower(), property.PropertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(property.Name, System.Reflection.PropertyAttributes.HasDefault, property.PropertyType, Type.EmptyTypes);
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, getSetAttr, property.PropertyType, Type.EmptyTypes);
            ILGenerator propertyGetIL = getMethodBuilder.GetILGenerator();
            propertyGetIL.Emit(OpCodes.Ldarg_0);
            propertyGetIL.Emit(OpCodes.Ldfld, propertyField);
            propertyGetIL.Emit(OpCodes.Ret);

            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, getSetAttr, null, new Type[] { property.PropertyType });
            ILGenerator propertySetIL = setMethodBuilder.GetILGenerator();
            propertySetIL.Emit(OpCodes.Ldarg_0);
            propertySetIL.Emit(OpCodes.Ldarg_1);
            propertySetIL.Emit(OpCodes.Stfld, propertyField);
            propertySetIL.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }

    }
}
