using Bam.Net;
using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static partial class HashExtensions
    {
        static Dictionary<HashAlgorithms, Func<byte[], HMAC>> _hmacs;
        static object _hmacsLock = new object();
        public static Dictionary<HashAlgorithms, Func<byte[], HMAC>> Hmacs
        {
            get
            {
                return _hmacsLock.DoubleCheckLock(ref _hmacs, () => new Dictionary<HashAlgorithms, Func<byte[], HMAC>>
                {
                    {Bam.Net.HashAlgorithms.MD5, (byte[] key) => new HMACMD5(key) },
                    {Bam.Net.HashAlgorithms.SHA1, (byte[] key) => new HMACSHA1(key) },
                    {Bam.Net.HashAlgorithms.SHA256, (byte[] key) => new HMACSHA256(key) },
                    {Bam.Net.HashAlgorithms.SHA384, (byte[] key) => new HMACSHA384(key) },
                    {Bam.Net.HashAlgorithms.SHA512, (byte[] key) => new HMACSHA512(key) }
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
                    { Bam.Net.HashAlgorithms.MD5, () => MD5.Create() },
                    { Bam.Net.HashAlgorithms.SHA1, () => SHA1.Create() },
                    { Bam.Net.HashAlgorithms.SHA256, () => SHA256.Create() },
                    { Bam.Net.HashAlgorithms.SHA384, () => SHA384.Create() },
                    { Bam.Net.HashAlgorithms.SHA512, () => SHA512.Create() }
                });
            }
        }

        public static string Md5(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Bam.Net.HashAlgorithms.MD5, encoding);
        }

        public static string Ripmd160(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Bam.Net.HashAlgorithms.RIPEMD160, encoding);
        }

        /// <summary>
        /// Calculate the SHA1 for the contents of the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Sha1(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Bam.Net.HashAlgorithms.SHA1, encoding);
        }

        /// <summary>
        /// Calculate the SHA256 for the contents of the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Sha256(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Bam.Net.HashAlgorithms.SHA256, encoding);
        }

        public static string Sha384(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Bam.Net.HashAlgorithms.SHA384, encoding);
        }

        public static string Sha512(this FileInfo file, Encoding encoding = null)
        {
            return file.ContentHash(Bam.Net.HashAlgorithms.SHA512, encoding);
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

        public static string Sha1(this byte[] bytes)
        {
            return Hash(bytes, Bam.Net.HashAlgorithms.SHA1);
        }

        public static string Sha256(this byte[] bytes)
        {
            return Hash(bytes, Bam.Net.HashAlgorithms.SHA256);
        }

        public static string Md5(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Bam.Net.HashAlgorithms.MD5, encoding);
        }

        public static string Ripmd160(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Bam.Net.HashAlgorithms.RIPEMD160, encoding);
        }

        public static string Sha384(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Bam.Net.HashAlgorithms.SHA384, encoding);
        }

        public static string Sha1(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Bam.Net.HashAlgorithms.SHA1, encoding);
        }

        public static string Sha256(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Bam.Net.HashAlgorithms.SHA256, encoding);
        }

        public static string Sha512(this string toBeHashed, Encoding encoding = null)
        {
            return toBeHashed.Hash(Bam.Net.HashAlgorithms.SHA512, encoding);
        }

        public static string HmacSha1(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Bam.Net.HashAlgorithms.SHA1, encoding);
        }

        public static string HmacSha256(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Bam.Net.HashAlgorithms.SHA256, encoding);
        }

        public static string HmacSha384(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Bam.Net.HashAlgorithms.SHA384, encoding);
        }

        public static string HmacSha512(this string toValidate, string key, Encoding encoding = null)
        {
            return Hmac(toValidate, key, Bam.Net.HashAlgorithms.SHA512, encoding);
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

        public static int ToHashInt(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            byte[] hashBytes = ToHashBytes(toBeHashed, algorithm, encoding);

            return BitConverter.ToInt32(hashBytes, 0);
        }

        public static long ToHashLong(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            byte[] hashBytes = ToHashBytes(toBeHashed, algorithm, encoding);

            return BitConverter.ToInt64(hashBytes, 0);
        }

        public static ulong ToHashULong(this string toBeHashed, HashAlgorithms algorithm, Encoding encoding = null)
        {
            byte[] hashBytes = ToHashBytes(toBeHashed, algorithm, encoding);

            return BitConverter.ToUInt64(hashBytes, 0);
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
            return ToHashInt(toBeHashed, Bam.Net.HashAlgorithms.SHA1);
        }

        public static int ToSha256Int(this string toBeHashed)
        {
            return ToHashInt(toBeHashed, Bam.Net.HashAlgorithms.SHA256);
        }

        public static long ToSha256Long(this string toBeHashed)
        {
            return ToHashLong(toBeHashed, Bam.Net.HashAlgorithms.SHA256);
        }

        public static ulong ToSha256ULong(this string toBeHashed)
        {
            return ToHashULong(toBeHashed, Bam.Net.HashAlgorithms.SHA256);
        }

        public static long ToSha512Long(this string toBeHashed)
        {
            return ToHashLong(toBeHashed, Bam.Net.HashAlgorithms.SHA512);
        }

        public static ulong ToSha512ULong(this string toBeHashed)
        {
            return ToHashULong(toBeHashed, Bam.Net.HashAlgorithms.SHA512);
        }

        public static long ToSha1Long(this string toBeHashed)
        {
            return ToHashLong(toBeHashed, Bam.Net.HashAlgorithms.SHA1);
        }

    }
}
