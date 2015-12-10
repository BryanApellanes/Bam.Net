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
using Naizari;
using System.Data;
using Naizari.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Naizari.Testing;

namespace Naizari.Configuration
{
    public static class SerializationUtil
    {
        /// <summary>
        /// Serialize the object to the specified filePath.  The same as
        /// ToXml().
        /// </summary>
        /// <param name="target"></param>
        /// <param name="filePath"></param>
        public static void XmlSerialize(this object target, string filePath)
        {
            ToXml(target, filePath);
        }

        /// <summary>
        /// Serialize the object to the specified filePath.  The same as XmlSerialzie()
        /// </summary>
        /// <param name="target"></param>
        /// <param name="filePath"></param>
        public static void ToXml(this object target, string filePath)
        {
            ToXml(target, filePath, true);
        }

        static object lockObj = new object();
        public static void ToXml(this object target, string filePath, bool throwIfNotSerializable)
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

        public static string GetXml(this object target)
        {
            XmlSerializer ser = new XmlSerializer(target.GetType());
            MemoryStream serialized = new MemoryStream();
            ser.Serialize(serialized, target);
            serialized.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(serialized))
            {
                return reader.ReadToEnd();
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

        public static byte[] ToBinaryBytes(this object target)
        {
            return ToBinaryStream(target).GetBuffer();
        }        

        public static MemoryStream ToBinaryStream(this object target)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream outPut = new MemoryStream();
            serializer.Serialize(outPut, target);
            outPut.Seek(0, SeekOrigin.Begin);
            return outPut;
        }

        public static void ToBinaryFile(this object target, string filePath)
        {
            FileMode mode = File.Exists(filePath) ? FileMode.Truncate : FileMode.Create;
            using (FileStream fs = new FileStream(filePath, mode))
            {
                byte[] binary = target.ToBinaryBytes();
                fs.Write(binary, 0, binary.Length);
            }            
        }

        public static T FromBinaryFile<T>(this string filePath)
        {
            return (T)filePath.FromBinaryFile(typeof(T));
        }

        public static object FromBinaryFile(this string filePath, Type type)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                MemoryStream outPut = new MemoryStream();
                byte[] buffer = new byte[32768]; // max int size
                while(true)
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

        public static T FromXmlFile<T>(this string filePath)
        {
            return (T)FromXmlFile(filePath, typeof(T));
        }

        static object fromXmlLock = new object();
        public static object FromXmlFile(this string filePath, Type type)
        {
            XmlSerializer ser = new XmlSerializer(type);
            lock (fromXmlLock)
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    return ser.Deserialize(sr);
                }
            }
        }

        public static T FromXmlString<T>(string xmlString)
        {
            return FromXmlString<T>(xmlString, Encoding.ASCII);
        }

        /// <summary>
        /// Deserialize the specified xml as the specified generic type using the specified encoding.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T FromXmlString<T>(string xmlString, Encoding encoding)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            byte[] xmlBytes = encoding.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(xmlBytes);
            return (T)ser.Deserialize(ms);
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
