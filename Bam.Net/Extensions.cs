/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Data.Common;
using System.Data;
using System.IO;
using Bam.Net.Configuration;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web.Script.Serialization;
using System.Xml;
using System.CodeDom;
using System.CodeDom.Compiler;
using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bam.Net
{
    public static class Extensions
    {
        static Dictionary<HashAlgorithms, Func<HashAlgorithm>> _hashAlgorithms;
        static Extensions()
        {
            _hashAlgorithms = new Dictionary<HashAlgorithms, Func<HashAlgorithm>>();
            _hashAlgorithms.Add(HashAlgorithms.MD5, () => MD5.Create());
            _hashAlgorithms.Add(HashAlgorithms.RIPEMD160, () => RIPEMD160.Create());
            _hashAlgorithms.Add(HashAlgorithms.SHA1, () => SHA1.Create());
            _hashAlgorithms.Add(HashAlgorithms.SHA256, () => SHA256.Create());
            _hashAlgorithms.Add(HashAlgorithms.SHA384, () => SHA384.Create());
            _hashAlgorithms.Add(HashAlgorithms.SHA512, () => SHA512.Create());
        }

        /// <summary>
        /// Return a copy of the specified DateTime with milliseconds
        /// set to 0
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime Trim(this DateTime dateTime)
        {
            Instant copy = new Instant(dateTime);
            copy.Millisecond = 0;
            return copy.ToDateTime();
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
            using(StreamReader reader = new StreamReader(file.FullName))
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
            List<T> results = new List<T>();
            foreach (object o in enumerable)
            {
                results.Add(o.CopyAs<T>());
            }
            return results;
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

        public static IEnumerable<object> CopyAs(this IEnumerable enumerable, Type type)
        {
            List<object> results = new List<object>();
            foreach (object o in enumerable)
            {
                results.Add(o.CopyAs(type));
            }
            return results;
        }

        /// <summary>
        /// Copy the current sourcce instance as the spcified type
        /// copying all properties that match in name and type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CopyAs(this object source, Type type)
        {
            object result = type.Construct();
            result.CopyProperties(source);
            return result;
        }

        public static T Clone<T>(this T from) where T : class, new()
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

        public static bool UnzipResource(this Assembly assembly, Type siblingOfResource, string resourceName, string extractTo, ExtractExistingFileAction existingFileAction = ExtractExistingFileAction.DoNotOverwrite)
        {
            return UnzipResource(assembly, Path.Combine(siblingOfResource.Namespace, resourceName).Replace("\\", "."), extractTo, existingFileAction);
        }

        public static bool UnzipResource(this Assembly assembly, string resourceName, string extractTo, ExtractExistingFileAction existingFileAction = ExtractExistingFileAction.DoNotOverwrite)
        {
            return UnzipResource(assembly, resourceName, new DirectoryInfo(extractTo));
        }

        public static bool UnzipResource(this Assembly assembly, string resourceName, DirectoryInfo extractTo, ExtractExistingFileAction existingFileAction = ExtractExistingFileAction.DoNotOverwrite)
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
                    ZipFile zipFile = ZipFile.Read(zipStream);
                    zipFile.Each(entry =>
                    {
                        entry.Extract(extractTo.FullName, existingFileAction);
                    });
                }
            });

            return found;
        }

        public static void UnzipTo(this string zipFilePath, string extractToDirectory)
        {
            new FileInfo(zipFilePath).UnzipTo(new DirectoryInfo(extractToDirectory));
        }

        public static void UnzipTo(this FileInfo file, DirectoryInfo extractTo, ExtractExistingFileAction existingFileAction = ExtractExistingFileAction.DoNotOverwrite)
        {
            ZipFile zipFile = ZipFile.Read(file.FullName);
            zipFile.Each(entry =>
            {
                entry.Extract(extractTo.FullName, existingFileAction);
            });
        }

        public static void ZipAndSave(this DirectoryInfo dirToZip, string saveTo)
        {
            ZipFile ignore;
            ZipAndSave(dirToZip, saveTo, out ignore);
        }

        public static void ZipAndSave(this DirectoryInfo dirToZip, string saveTo, out ZipFile zip)
        {
            zip = dirToZip.Zip();
            zip.Save(saveTo);
        }

        public static ZipFile Zip(this DirectoryInfo dirToZip, string excludePattern = null)
        {
            List<DirectoryInfo> excludeDirs = excludePattern == null ? new List<DirectoryInfo>() : new List<DirectoryInfo>(dirToZip.GetDirectories(excludePattern));
            List<FileInfo> excludeFiles = excludePattern == null ? new List<FileInfo>() : new List<FileInfo>(dirToZip.GetFiles(excludePattern));

            ZipFile zipFile = new ZipFile();
            DirectoryInfo[] dirs = dirToZip.GetDirectories();
            dirs.Each(new { Exclude = excludeDirs }, (ctx, dir) =>
            {
                if (!ctx.Exclude.Contains(dir))
                {
                    zipFile.AddDirectory(dir.FullName, dir.Name);
                }
            });

            FileInfo[] files = dirToZip.GetFiles();
            files.Each(new { Exclude = excludeFiles }, (ctx, f) =>
            {
                if (!ctx.Exclude.Contains(f))
                {
                    zipFile.AddFile(f.FullName, "");
                }
            });

            return zipFile;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T TryToEnum<T>(this string value) where T : struct
        {
            T result;
            Enum.TryParse<T>(value, out result);
            return result;
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

        public static string ToCsvLine(this object data)
        {
            return ToCsv(new object[] { data });
        }

        public static string ToCsv(this object[] dataArr, bool includeHeader = false, bool newLine = true)
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
                        WriteCsv(data, writer);
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
            Type type = data.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
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
            return type.ImplementsInterface<T>();//ImplementsInterface<T>(type);
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

        public static T[] Times<T>(this int count, Func<int, T> func)
        {
            T[] results = new T[count];
            for (int i = 0; i < count; i++)
            {
                results[i] = func(i);
            }

            return results;
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
        public static T ParseKeyValuePairs<T>(this string input, bool pascalCasify = true, string keyValueSeparator = ":", string elementSeparator = ";") where T : class, new()
        {
            T result = new T();
            string[] elements = input.DelimitSplit(elementSeparator);
            elements.Each(element =>
            {
                string[] kvp = element.DelimitSplit(keyValueSeparator);
                Args.ThrowIf<ArgumentException>(kvp.Length != 2, "Unrecognized Key Value pair format: ({0})", element);

                string key = pascalCasify ? kvp[0].PascalCase() : kvp[0];
                string value = pascalCasify ? kvp[1].PascalCase() : kvp[1];

                result.Property(key, value);
            });

            return result;
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
            arr.ToArray().Each(action);
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

        public static void Each<T>(this IEnumerable<T> arr, Func<T, bool> function)
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
        /// Iterate over the current array passing
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
        /// <param name="action"></param>
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
        /// <param name="action"></param>
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

        public static bool TryConstruct(this Type type, out object constructed, params object[] ctorParams)
        {
            bool result = true;
            constructed = null;
            try
            {
                constructed = Construct(type, ctorParams);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static bool TryConstruct<T>(this Type type, out T constructed, params object[] ctorParams)
        {
            bool result = true;
            constructed = default(T);
            try
            {
                constructed = Construct<T>(type, ctorParams);
            }
            catch //(Exception ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Construct an instance of the type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="ctorParams"></param>
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
            List<Type> paramTypes = new List<Type>();
            foreach (object o in ctorParams)
            {
                paramTypes.Add(o.GetType());
            }

            ConstructorInfo ctor = type.GetConstructor(paramTypes.ToArray());
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
        /// toInit is null
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

        public static void ToJsonFile(this object value, string path)
        {
            ToJsonFile(value, new FileInfo(path));
        }

        public static void ToJsonFile(this object value, FileInfo file)
        {
            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                sw.Write(ToJson(value, Newtonsoft.Json.Formatting.Indented));
            }
        }

        public static Stream ToJsonStream(this object value)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(value.ToJson());
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
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

        public static T FromJObject<T>(this JObject jObject)
        {
            return jObject.ToJson().FromJson<T>();
        }

        public static T FromJObject<T>(this object jObject)
        {
            return jObject.ToJson().FromJson<T>();
        }

        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
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

            HashAlgorithm alg = _hashAlgorithms[algorithm]();
            byte[] fileContents = File.ReadAllBytes(file.FullName);
            byte[] hashBytes = alg.ComputeHash(fileContents);

            return hashBytes.ToHexString();
        }

        public static string Md5(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(HashAlgorithms.MD5, encoding);
        }

        public static string Ripmd160(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(HashAlgorithms.RIPEMD160, encoding);
        }

        /// <summary>
        /// Calculate the SHA1 for the contents of the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Sha1(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(HashAlgorithms.SHA1, encoding);
        }

        public static string Sha256(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(HashAlgorithms.SHA256, encoding);
        }

        public static string Sha384(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(HashAlgorithms.SHA384, encoding);
        }

        public static string Sha512(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(HashAlgorithms.SHA512, encoding);
        }

        public static string Hash(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            HashAlgorithm alg = _hashAlgorithms[algorithm]();
            byte[] bytes = encoding.GetBytes(toBeHashed);
            byte[] hashBytes = alg.ComputeHash(bytes);

            return hashBytes.ToHexString();
        }

        public static string Md5(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(HashAlgorithms.MD5, encoding);
        }

        public static string Ripmd160(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(HashAlgorithms.RIPEMD160, encoding);
        }

        public static string Sha384(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(HashAlgorithms.SHA384, encoding);
        }

        public static string Sha1(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(HashAlgorithms.SHA1, encoding);
        }

        public static string Sha256(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(HashAlgorithms.SHA256, encoding);
        }

        public static string Sha512(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(HashAlgorithms.SHA512, encoding);
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
        /// a Unicode representation the return value will
        /// be false
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
            string tail;
            value.Tail(count, out tail);
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

        public static string RandomString(int length, bool mixCase, bool includeNumbers)
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
            if (stringToPluralize.ToLowerInvariant().EndsWith("ies"))
            {
                return stringToPluralize;
            }
            else if (stringToPluralize.ToLowerInvariant().EndsWith("us"))
            {
                return stringToPluralize.Substring(0, stringToPluralize.Length - 2) + "i";
            }
            else if (stringToPluralize.ToLowerInvariant().EndsWith("s") ||
                stringToPluralize.ToLowerInvariant().EndsWith("sh"))
            {
                return stringToPluralize + "es";
            }
            else if (stringToPluralize.ToLowerInvariant().EndsWith("ey"))
            {
                return stringToPluralize + "s";
            }
            else if (stringToPluralize.ToLowerInvariant().EndsWith("y"))
            {
                return stringToPluralize.Substring(0, stringToPluralize.Length - 1) + "ies";
            }
            else
            {
                return stringToPluralize + "s";
            }
        }

        /// <summary>
        /// Gets the full path to the App_Data folder if HttpContext.Current is not null.
        /// Otherwise returns the full path to Environment.SpecialFolder.ApplicationData
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static string GetAppDataFolder(this object any)
        {
            StringBuilder path = new StringBuilder();
            if (HttpContext.Current == null)
            {
                path.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                if (!path.ToString().EndsWith("\\"))
                {
                    path.Append("\\");
                }

                path.Append(DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN") + "\\");
                FileInfo fileInfo = new FileInfo(path.ToString());
                if (!Directory.Exists(fileInfo.Directory.FullName))
                {
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
                }
            }
            else
            {
                path.Append(HttpContext.Current.Server.MapPath("~/App_Data/"));
            }

            return path.ToString();
        }

        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable source, Func<TSource, TKey> keySelector)
        {
            Dictionary<TKey, TSource> result = new Dictionary<TKey, TSource>();
            foreach (object o in source)
            {
                TSource s = (TSource)o;
                result.Add(keySelector(s), s);
            }

            return result;
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

        public static string PropertiesToString(this object obj, string separator = "\r\n")
        {
            Args.ThrowIfNull(obj);

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            return obj.PropertiesToString(properties, separator);
        }

        public static string PropertiesToString(this object obj, PropertyInfo[] properties, string separator = "\r\n")
        {
            StringBuilder returnValue = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    if (property.GetIndexParameters().Length == 0)
                    {
                        object value = property.GetValue(obj, null);
                        if (value != null)
                        {
                            returnValue.AppendFormat("{0}: {1}{2}", property.Name, value.ToString(), separator);
                        }
                    }
                    else
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

        private static void CopyDirectory(string sourcePath, string destPath, bool overwrite = false, Action<string, string> beforeFileCopy = null, Action<string, string> beforeDirectoryCopy = null)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                if (beforeFileCopy != null)
                {
                    beforeFileCopy(file, dest);
                }
                File.Copy(file, dest, overwrite);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                if (beforeDirectoryCopy != null)
                {
                    beforeDirectoryCopy(folder, dest);
                }
                CopyDirectory(folder, dest, overwrite, beforeFileCopy, beforeDirectoryCopy);
            }
        }

        static Dictionary<string, object> safeReadLock = new Dictionary<string, object>();
        /// <summary>
        /// Returns the content of the file refferred to by the current
        /// string instance.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SafeReadFile(this string filePath)
        {
            if (!safeReadLock.ContainsKey(filePath))
                safeReadLock.Add(filePath, new object());

            if (!File.Exists(filePath))
                return string.Empty;

            string retVal = string.Empty;

            lock (safeReadLock[filePath])
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    retVal = sr.ReadToEnd();
                }
            }
            return retVal;
        }

        static Dictionary<string, object> safeWriteLock = new Dictionary<string, object>();
        public static void SafeWriteFile(this string filePath, string textToWrite, Action<object> postWriteAction = null)
        {
            SafeWriteFile(filePath, textToWrite, false, postWriteAction);
        }

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
            FileInfo fileInfo = new FileInfo(filePath);

            if (!Directory.Exists(fileInfo.Directory.FullName))
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

            if (File.Exists(fileInfo.FullName) && !overwrite)
            {
                throw new InvalidOperationException("File already exists and 'overwrite' parameter was false");
            }

            lock (safeWriteLock)
            {
                if (!safeWriteLock.ContainsKey(fileInfo.FullName))
                {
                    safeWriteLock.Add(fileInfo.FullName, new object());
                }

                lock (safeWriteLock[fileInfo.FullName])
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.Write(textToWrite);
                    }
                }
            }

            if (postWriteAction != null)
            {
                postWriteAction(new { });
            }
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

            if (!safeWriteLock.ContainsKey(fileInfo.FullName))
            {
                safeWriteLock.Add(fileInfo.FullName, new object());
            }
            lock (safeWriteLock[fileInfo.FullName])
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.Write(textToAppend);
                    sw.Flush();
                }
            }
        }

        /// <summary>
        /// Clears the locks createed for writing and appending
        /// to files
        /// </summary>
        public static void ClearWriteLocks(this object any)
        {
            lock (safeWriteLock)
            {
                safeWriteLock.Clear();
            }
        }

        /// <summary>
        /// Clears the locks created for reading files.
        /// </summary>
        /// <param name="any"></param>
        public static void ClearReadLocks(this object any)
        {
            lock (safeReadLock)
            {
                safeReadLock.Clear();
            }
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

        public static bool IsLetter(this char c)
        {
            int val = Convert.ToInt32(c);
            return (val > 96 && val < 123) || (val > 64 && val < 91);
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
            foreach (char c in targetString)
            {
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
            return property.PropertyType.IsArray ||
                typeof(IEnumerable).IsAssignableFrom(property.PropertyType) ||
                property.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null;
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
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return CreateDynamicType<PropertyAttributeFilter>(objectToClone, out ignore);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="typeToClone"></param>
        /// <returns></returns>
        public static Type CreateDynamicType<PropertyAttributeFilter>(this Type typeToClone) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return CreateDynamicType<PropertyAttributeFilter>(typeToClone, out ignore, false);
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
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return CreateDynamicType<PropertyAttributeFilter>(objectToClone, out ignore, concreteAttribute);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder) where PropertyAttributeFilter : Attribute, new()
        {
            return CreateDynamicType<PropertyAttributeFilter>(objectToClone, out assemblyBuilder, false);
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
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            Type objType = objectToClone.GetType();
            return CreateDynamicType<PropertyAttributeFilter>(objType, out assemblyBuilder, concreteAttribute);
        }

        public static Type CreateDynamicType<PropertyAttributeFilter>(this Type objType, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            string typeName = objType.Namespace + "." + objType.Name;
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName].DynamicType != null)
            {
                assemblyBuilder = DynamicTypeStore.Current[typeName].AssemblyBuilder;
                return DynamicTypeStore.Current[typeName].DynamicType;
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

                Type jsonSafeType = typeBuilder.CreateType();
                if (DynamicTypeStore.Current[typeName] == null)
                {
                    throw new InvalidOperationException("DynamicTypeInfo was null");
                }
                DynamicTypeStore.Current[typeName].DynamicType = jsonSafeType;
                return jsonSafeType;
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
            AssemblyBuilder ignore;
            Type instanceType = instance.GetType();
            string newTypeName = "ValuesOf.{0}.{1}"._Format(instanceType.Namespace, instanceType.Name);
            Type dynamicType = ValuePropertiesToDynamicType(instance, newTypeName, out ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object valuesOnlyInstance = ctor.Invoke(null);
            DefaultConfiguration.CopyProperties(instance, valuesOnlyInstance);
            return valuesOnlyInstance;
        }

        /// <summary>
        /// Copies all properties from source to destination where the name and
        /// type match.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object CopyProperties(this object destination, object source)
        {
            Type destinationType = destination.GetType();
            Type sourceType = source.GetType();

            foreach (PropertyInfo destProp in destinationType.GetProperties())
            {
                PropertyInfo sourceProp = sourceType.GetProperty(destProp.Name);
                CopyProperty(destination, source, destProp, sourceProp);
            }

            return destination;
        }

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
                if ((sourceProp.PropertyType == destProp.PropertyType ||
                    sourceProp.PropertyType == Nullable.GetUnderlyingType(destProp.PropertyType) ||
                    Nullable.GetUnderlyingType(sourceProp.PropertyType) == destProp.PropertyType)
                    && destProp.CanWrite)
                {
                    object value = sourceProp.GetValue(source, null);
                    destProp.SetValue(destination, value, null);
                }
            }
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

        public static Type CombineToDynamicType(this object instance, params object[] toMerge)
        {
            return CombineToDynamicType(instance, "DynamicType_".RandomLetters(8), toMerge);
        }

        public static Type CombineToDynamicType(this object instance, string typeName, params object[] toMerge)
        {
            TypeBuilder typeBuilder;
            AssemblyBuilder assemblyBuilder;
            GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);
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

        public static Type ToDynamicType(this object instance, string typeName, Func<PropertyInfo, bool> propertyPredicate)
        {
            AssemblyBuilder ignore;
            return ToDynamicType(instance, typeName, propertyPredicate, out ignore);
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
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
            {
                return GetExistingDynamicType(typeName, out assemblyBuilder);
            }
            else
            {
                TypeBuilder typeBuilder;
                GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

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

        public static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, List<Type> created)
        {
            AssemblyBuilder ignore;
            return ToDynamicType(dictionary, typeName, 0, created, out ignore, false);
        }

        public static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, bool useCache = true)
        {
            return ToDynamicType(dictionary, typeName, 0, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, bool useCache = true)
        {
            AssemblyBuilder ignore;
            return dictionary.ToDynamicType(typeName, recursionThusFar, out ignore, useCache);
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
                return CreateDynamicType(dictionary, typeName, recursionThusFar, createdTypes, out assemblyBuilder);
            }
        }

        private static Type CreateDynamicType(Dictionary<object, object> dictionary, string typeName, int recursionThusFar, List<Type> createdTypes, out AssemblyBuilder assemblyBuilder)
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
                        string childTypeName = "{0}{1}"._Format(typeName, propertyName);
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
            AssemblyBuilder ignore;
            return MergeToDynamicType(objects, typeName, recursionThusFar, out ignore, useCache);
        }

        public static Type MergeToDynamicType(this List<object> objects, string typeName, int recursionThusFar, out AssemblyBuilder assemblyBuilder, bool useCache = true)
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

        public static Type ToDynamicType(this DataRow row)
        {
            AssemblyBuilder ignore;
            string typeName = 8.RandomLetters();
            if (row.Table != null)
            {
                typeName = row.Table.TableName.Or(typeName);
            }

            return ToDynamicType(row, typeName, out ignore);
        }

        public static Type ToDynamicType(this DataRow row, string typeName, out AssemblyBuilder assemblyBuilder)
        {
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null)
            {
                return GetExistingDynamicType(typeName, out assemblyBuilder);
            }
            else
            {
                TypeBuilder typeBuilder;
                GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

                foreach (DataColumn column in row.Table.Columns)
                {
                    CustomPropertyInfo propInfo = new CustomPropertyInfo(column.ColumnName, row[column].GetType());
                    AddPropertyToDynamicType(typeBuilder, propInfo);
                }

                return CreateDynamicType(typeName, typeBuilder);
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

        internal static void GetAssemblyAndTypeBuilder(object objectToClone, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder)
        {
            Type type = objectToClone.GetType();
            GetAssemblyAndTypeBuilder(type.Namespace + "." + type.Name, out assemblyBuilder, out typeBuilder);
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
                AssemblyName assemblyName = new AssemblyName("DynamicGenerator");
                assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");

                typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Public);

                DynamicTypeStore.Current.AddType(typeName, new DynamicTypeInfo { AssemblyBuilder = assemblyBuilder, TypeBuilder = typeBuilder, TypeName = typeName });
            }
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
