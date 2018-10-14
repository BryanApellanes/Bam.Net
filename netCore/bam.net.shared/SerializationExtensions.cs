/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Bam.Net;
using Bam.Net.Logging;

namespace Bam.Net
{
	/// <summary>
	/// Container for serialization related extensioni methods
	/// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serialize the object to the specified filePath.  The same as
        /// ToXmlFile().
        /// </summary>
        /// <param name="target"></param>
        /// <param name="filePath"></param>
        public static void XmlSerialize(this object target, string filePath)
        {
            ToXmlFile(target, filePath);
        }

        /// <summary>
        /// Serialize the object to the specified filePath.  The same as XmlSerialzie()
        /// </summary>
        /// <param name="target"></param>
        /// <param name="filePath"></param>
        public static void ToXmlFile(this object target, string filePath)
        {
            ToXmlFile(target, filePath, true);
        }

        static object lockObj = new object();
        public static void ToXmlFile(this object target, string filePath, bool throwIfNotSerializable)
        {
            Type t = target.GetType();
            if (throwIfNotSerializable)
            {
                bool isSerializable = IsSerializable(t);
                if (!isSerializable)
                {
                    ThrowInvalidOperationException(t);
                }
            }

            XmlSerializer ser = new XmlSerializer(t);
            lock (lockObj)
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    ser.Serialize(sw, target);
                }
            }
        }

        public static string ToXml(this object target)
        {
            MemoryStream stream = new MemoryStream();
            ToXmlStream(target, stream);
            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string ToXml(this object target, bool includeXmlDeclaration)
        {
            return ToXml(target, new XmlWriterSettings { OmitXmlDeclaration = !includeXmlDeclaration, Indent = true });
        }

        public static string ToXml(this object target, XmlWriterSettings settings)
        {
            MemoryStream stream = new MemoryStream();
            ToXmlStream(target, stream, settings);
            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static void ToXmlStream(this object target, Stream stream)
        {
            ToXmlStream(target, stream, new XmlWriterSettings { Indent = true });
        }

        public static void ToXmlStream(this object target, Stream stream, XmlWriterSettings settings)
        {
            XmlSerializer ser = new XmlSerializer(target.GetType());
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                ser.Serialize(writer, target);
            }                
        }

        public static bool IsSerializable(Type t)
        {
            bool isSerializable = t.GetCustomAttributes(typeof(SerializableAttribute), false).Length > 0;
            return isSerializable;
        }

        public static T XmlDeserialize<T>(this string filePath)
        {
            return FromXmlFile<T>(filePath);
        }

        public static T DeserializeFromFile<T>(string filePath)
        {
            return FromXmlFile<T>(filePath);
        }

        public static T FromBinaryBytes<T>(this byte[] bytes)
        {
            object retVal = FromBinaryBytes(bytes);
            Expect.IsTrue(retVal.GetType() == typeof(T));
            return (T)retVal;
        }

        public static object FromBinaryBytes(this byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            return formatter.Deserialize(stream);
        }

		/// <summary>
		/// Get the ammount of memory occupied by the 
		/// specified target (current target if used as extension method)
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int MemorySize(this object target)
		{
            return MemorySize(target, out byte[] ignore);
        }

		public static int MemorySize(this object target, out byte[] byteStream)
		{
			byteStream = ToBinaryBytes(target);
			return byteStream.Length;
		}
		public static long LongMemorySize(this object target)
		{
			byte[] ignore;
			return LongMemorySize(target, out ignore);
		}

		public static long LongMemorySize(this object target, out byte[] byteStream)
		{
			byteStream = ToBinaryBytes(target);
			return byteStream.LongLength;
		}

        public static bool TryToBinaryBytes(this object target, out byte[] bytes, Action<Exception> exceptionHandler)
        {
            try
            {
                bytes = ToBinaryBytes(target);
                return true;
            }
            catch (Exception ex)
            {
                exceptionHandler(ex);
                bytes = null;
                return false;
            }
        }

        public static byte[] ToBinaryBytes(this object target)
        {
            return ToBinaryStream(target).GetBuffer();
        }

        public static MemoryStream ToBinaryStream(this object target)
        {
            Args.ThrowIfNull(target, "target");            
            MemoryStream stream = new MemoryStream();
            ToBinaryStream(target, stream);
            return stream;
        }

        public static void ToBinaryStream(this object target, Stream stream)
        {
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, target);
                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                Log.Default.AddEntry("Failed to serialize target of type ({0}): {1}", ex, target.GetType().FullName, ex.Message);
            }
        }

        public static object FromBinaryStream(this Stream stream)
        {
            MemoryStream outPut = new MemoryStream();
            byte[] buffer = new byte[32768]; // max int size
            while (true)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                outPut.Write(buffer, 0, read);
            }
            return outPut.GetBuffer().FromBinaryBytes();
        }

        public static void ToBinaryFile(this object target, string filePath)
        {
			FileInfo file = new FileInfo(filePath);
			if (!file.Directory.Exists)
			{
				file.Directory.Create();
			}
            TryWrite(target, filePath);
        }

        private static void TryWrite(object target, string filePath, int retryCount = 10)
        {
            try
            {
                File.WriteAllBytes(filePath, target.ToBinaryBytes());
            }
            catch (Exception ex)
            {
                Logging.Log.Warn("Failed to write BinaryFile: {0}\r\nType:{1}\r\nMessage: {2}\r\nWillRetry: {3}",
                    filePath, target == null ? "[null]" : target.GetType().Name, ex.Message, retryCount);
                if (retryCount > 0)
                {
                    TryWrite(target, filePath, --retryCount);
                }
                else
                {
                    Logging.Log.Error("Failed to write BinaryFile: {0}\r\nType:{1}\r\nMessage: {2}", ex,
                        filePath, target == null ? "[null]" : target.GetType().Name, ex.Message);
                }
            }
        }

        public static T FromBinaryFile<T>(this FileInfo file)
        {
            return FromBinaryFile<T>(file.FullName);
        }

        public static T FromBinaryFile<T>(this string filePath)
        {
            return (T)filePath.FromBinaryFile(typeof(T));
        }

        public static object FromBinaryFile(this FileInfo file, Type type)
        {
            return FromBinaryFile(file.FullName, type);
        }

        public static object FromBinaryFile(this string filePath, Type type, ILogger logger = null)
        {
            try
            {
                lock (FileLock.Named(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        MemoryStream outPut = new MemoryStream();
                        byte[] buffer = new byte[32768]; // max int size
                        while (true)
                        {
                            int read = fs.Read(buffer, 0, buffer.Length);
                            if (read <= 0)
                            {
                                break;
                            }
                            outPut.Write(buffer, 0, read);
                        }

                        outPut.Seek(0, SeekOrigin.Begin);
                        return outPut.GetBuffer().FromBinaryBytes();
                    }
                }
            }
            catch (Exception ex)
            {
                (logger ?? Log.Default).AddEntry("Exception occurred loading binary file {0}: {1}", ex, filePath, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deserialize the xml file as the specified type
        /// </summary>
        /// <typeparam name="T">The type of the return value</typeparam>
        /// <param name="file">Th xml file</param>
        /// <returns>instance of T</returns>
        public static T FromXmlFile<T>(this FileInfo file)
        {
            return file.FullName.FromXmlFile<T>();
        }

        /// <summary>
        /// Deserialize the xml file as the specified generic type
        /// </summary>
        /// <typeparam name="T">The type of the return value</typeparam>
        /// <param name="filePath">The path to the xml file</param>
        /// <returns>instance of T</returns>
        public static T FromXmlFile<T>(this string filePath)
        {
            return (T)FromXmlFile(filePath, typeof(T));
        }

        /// <summary>
        /// Deserialize the xml file as the speicified type
        /// </summary>
        /// <param name="filePath">The path to the xml file</param>
        /// <param name="type">The type of the return value</param>
        /// <returns>instance of specified type deserialized from the specified file</returns>
        public static object FromXmlFile(this string filePath, Type type)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                return new XmlSerializer(type).Deserialize(sr);
            }
        }
        
        public static object FromXmlStream(this Stream xmlStream, Type type)
        {
            return new XmlSerializer(type).Deserialize(xmlStream);
        }

        /// <summary>
        /// Deserialize the specified xmlString as the specified 
        /// generic type
        /// </summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="xmlString">The string to deserialize</param>
        /// <returns>instance of T</returns>
        public static T FromXml<T>(this string xmlString)
        {
            return FromXml<T>(xmlString, Encoding.Default);
        }

        /// <summary>
        /// Deserialize the specified xml as the specified generic type using the specified encoding.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T FromXml<T>(this string xmlString, Encoding encoding)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            byte[] xmlBytes = encoding.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(xmlBytes);
            return (T)ser.Deserialize(ms);
        }

		public static object FromXml(this string xml, Type type, Encoding encoding = null)
		{
			XmlSerializer ser = new XmlSerializer(type);
			byte[] xmlBytes = encoding.GetBytes(xml);
			MemoryStream ms = new MemoryStream(xmlBytes);
			return ser.Deserialize(ms);
		}

        static object fromXmlProxy = new object();
        public static void SetPropertiesFromXml<T>(object target, string filePath)
        {
            T ret;
            Type t = target.GetType();

            if (!IsSerializable(t))
                ThrowInvalidOperationException(t);

            XmlSerializer ser = new XmlSerializer(t);
            lock (fromXmlProxy)
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    ret = (T)ser.Deserialize(sr);
                }
            }
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.CanWrite)
                    prop.SetValue(target, prop.GetValue(ret, null), null);
            }
        }

        private static void ThrowInvalidOperationException(Type t)
        {
            throw new InvalidOperationException("The target object specified is of type " + t.Name + " which is not serializable.  If this is a user defined type add the [Serializable] attribute to the class definition");
        }
    }
}
