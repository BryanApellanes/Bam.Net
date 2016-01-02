/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Naizari.Logging;
using System.Security.Cryptography;
using System.Reflection;

namespace Naizari.Extensions
{
    public static class FileInfoExtension
    {
        public static bool AssemblyIsLoaded(this FileInfo info)
        {
            Assembly ignore;
            return AssemblyIsLoaded(info, out ignore);
        }

        /// <summary>
        /// Determines if the assembly represented by the current FileInfo instance
        /// has been loaded.  This is accomplished by comparing the SHA1 value
        /// of the specified FileInfo to the currently loaded assemblies in the 
        /// current app domain.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="loaded">The loaded assembly if the assembly is loaded null otherwise</param>
        /// <returns></returns>
        public static bool AssemblyIsLoaded(this FileInfo info, out Assembly loaded)
        {
            loaded = null;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Uri uri = new Uri(assembly.CodeBase);
                FileInfo loadedFileInfo = new FileInfo(uri.LocalPath);
                string loadedFileSHA1 = loadedFileInfo.SHA1();
                string compareSHA1 = info.SHA1();
                if (loadedFileSHA1.Equals(compareSHA1))
                {
                    loaded = assembly;
                    return true;
                }
            }

            return false;
        }

        public static string SHA1(this FileInfo fileInfo)
        {
            byte[] data = new byte[]{};
            if (fileInfo.Exists)
                data = File.ReadAllBytes(fileInfo.FullName);

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(sha1.ComputeHash(data));
        }

        public static bool IsText(this FileInfo fileInfo)
        {
            if(!fileInfo.Exists)
                return false;

            int size = 5000;
            try
            {
                if (fileInfo.Length <= 5000)
                    size = (int)fileInfo.Length;
            }
            catch(Exception ex)
            {
                Log.Default.AddEntry("An error occurred determining sample size: {0}", ex, ex.Message);
                throw;
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
    }
}
