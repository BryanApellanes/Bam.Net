/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Naizari.Testing;
using Naizari.Helpers;
using Naizari.Configuration;
using System.Diagnostics;
using Naizari;
using System.Reflection;
using Naizari.Helpers.Web;
using System.Web.UI;
using System.Security.Cryptography;

namespace Naizari.Extensions
{
    public static class StringExtensions
    {
        public static void JavascriptToPage(this string script, Page target)
        {
            JavascriptToControl(script, target);
        }

        public static void JavascriptToControl(this string script, Control target)
        {
            target.Controls.Add(ControlHelper.CreateScriptControl(script));
        }  

        public static bool TryParseAs<EnumType>(this string stringToParse, out EnumType enumInstance)
        {
            try
            {
                enumInstance = (EnumType)Enum.Parse(typeof(EnumType), stringToParse);
                return true;
            }
            catch// (Exception ex)
            {
                enumInstance = default(EnumType);
                return false;
            }
        }

        public static string[] Split(this string s, string delimiter)
        {
            return s.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ThrowIfNullOrEmpty(this string s)
        {
            return s.ThrowIfNullOrEmpty("The specified string was null or empty");
        }

        public static string ThrowIfNullOrEmpty(this string s, string msg)
        {
            if (string.IsNullOrEmpty(s))
                ExceptionHelper.Throw<ArgumentException>(msg);
            return s;
        }

        public static T ToEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        /// <summary>
        /// Executes the current string on the command line
        /// and returns the output.  The same as SystemExecute().
        /// </summary>
        /// <param name="command">a valid command line</param>
        /// <returns>ProcessOutput</returns>
        public static ProcessOutput ExecuteCommandLine(this string command)
        {
            return SystemExecute(command);
        }

        /// <summary>
        /// Returns the specified valueIfNullOrEmpty if the current string is null or empty
        /// other returns the current string after calling Trim() on it.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueIfNullOrEmpty"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string value, string valueIfNullOrEmpty)
        {
            if (value != null)
                value = value.Trim();
            return string.IsNullOrEmpty(value) ? valueIfNullOrEmpty : value;
        }

        /// <summary>
        /// Executes the current string on the command line
        /// and returns the output.  The same as ExecuteCommandLine().
        /// </summary>
        /// <param name="command">a valid command line</param>
        /// <returns>ProcessOutput</returns>
        public static ProcessOutput SystemExecute(this string command)
        {
            Expect.IsFalse(string.IsNullOrEmpty(command), "command cannot be blank or null");
            Expect.IsFalse(command.Contains("\r"), "Multiple command lines not supported");
            Expect.IsFalse(command.Contains("\n"), "Multiple command lines not supported");

            string exe = string.Empty;
            string arguments = string.Empty;
            string[] split = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
            {
                exe = split[0];
                for (int i = 1; i < split.Length; i++)
                {
                    arguments += split[i];
                    if (i != split.Length - 1)
                        arguments += " ";
                }                
            }
            
            ProcessStartInfo startInfo = CreateStartInfo();


            startInfo.FileName = string.IsNullOrEmpty(exe) ? command : exe;
            startInfo.Arguments = arguments;

            string output = string.Empty;
            string error = string.Empty;

            int exitCode = -1;
            using (Process theProcess = new Process())
            {
                theProcess.StartInfo = startInfo;
                theProcess.Start();
                output = theProcess.StandardOutput.ReadToEnd();
                theProcess.StandardOutput.Close();
                error = theProcess.StandardError.ReadToEnd();
                theProcess.StandardError.Close();
                exitCode = theProcess.ExitCode;
                theProcess.Close();
            }

            return new ProcessOutput(output, error, exitCode);
        }

        private static ProcessStartInfo CreateStartInfo()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.ErrorDialog = false;
            startInfo.CreateNoWindow = true; ;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            return startInfo;
        }

        /// <summary>
        /// Converts the specified array of strings into a comma separated
        /// string of values.  The same as ToCommaSeparated.
        /// </summary>
        /// <param name="stringArray">The string array</param>
        /// <returns>string</returns>
        public static string ToCommaDelimited(string[] stringArray)
        {
            return ToCommaSeparated(stringArray);
        }

        static string[] months = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public static string GetMonth(int month)
        {
            Expect.IsTrue(month > 0);
            Expect.IsTrue(month < 13);
            return months[month];
        }

        public static string GetNowNumbers()
        {
            DateTime now = DateTime.Now;
            string month = now.Month.ToString();
            month = month.Length != 2 ? "0" + month : month;
            string day = now.Day.ToString();
            day = day.Length != 2 ? "0" + day : day;
            string year = now.Year.ToString();
            string hour = now.Hour.ToString();
            hour = hour.Length != 2 ? "0" + hour : hour;
            string minute = now.Minute.ToString();
            minute = minute.Length != 2 ? "0" + minute : minute;
            string second = now.Second.ToString();
            second = second.Length != 2 ? "0" + second : second;

            return month + day + year + hour + minute + second;
        }

        /// <summary>
        /// Used to return the specified DateTime to MM/DD/YYYY format.  If there's an easier way to do this,
        /// I'd love for someone to show me ;)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        //[Obsolete("Don't use this method.  Buggy if Intl. cultureInfo")]
        //public static string ToTwoTwoFour(this DateTime dateTime)
        //{
        //    string shortDate = dateTime.ToShortDateString();
        //    string[] monthDayYear = shortDate.Split('/');
        //    string month = monthDayYear[0];
        //    if (month.Length == 1)
        //        month = "0" + month;
        //    string date = monthDayYear[1];
        //    if (date.Length == 1)
        //        date = "0" + date;
        //    string year = monthDayYear[2];
        //    return string.Format("{0}/{1}/{2}", month, date, year);
        //}

        //[Obsolete("This method is evil.  Use DateTime.Parse and DateTime.ParseExact instead")]
        //public static DateTime TwoTwoFourToDateTime(this string twoTwoFour)
        //{
        //    return DateTime.Parse(twoTwoFour);
        //}

        /// <summary>
        /// returns an empty string if the input equals &amp;nbsp;
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CheckIfBlank(string input)
        {
            if (input.Trim().Equals("&nbsp;"))
                return "";
            else
                return input;
        }
        /// <summary>
        /// Converts the specified array of strings into a comma separated
        /// string of values.  The same as ToCommaDelimited.
        /// </summary>
        /// <param name="stringArray">The string array</param>
        /// <returns>string</returns>
        public static string ToCommaSeparated(string[] stringArray)
        {
            return ToDelimited(stringArray, ",");
        }

        /// <summary>
        /// Write the string to the specified file overwriting the
        /// existing if permissions allow.
        /// </summary>
        /// <param name="textToWrite"></param>
        /// <param name="filePath"></param>
        public static void WriteToFile(this string textToWrite, string filePath)
        {
            WriteToFile(textToWrite, filePath, false);
        }

        static Dictionary<string, object> locks = new Dictionary<string,object>();
        /// <summary>
        /// Write the string to the specified file.
        /// </summary>
        /// <param name="textToWrite"></param>
        /// <param name="filePath">the file to write to</param>
        /// <param name="append">follows the same rules as the append parameter of the (string, bool) ctor
        /// of the StreamWriter class since that is what is used internally.</param>
        public static void WriteToFile(this string textToWrite, string filePath, bool append)
        {
            if (!locks.ContainsKey(filePath))
                locks.Add(filePath, new object());

            lock (locks[filePath])
            {
                using (StreamWriter sw = new StreamWriter(filePath, append))
                {
                    sw.WriteLine(textToWrite);
                }
            }
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
            string retVal = string.Empty;
            bool first = true;
            foreach (T obj in objectsToStringify)
            {
                if (!first)
                {
                    retVal += delimiter;
                }
                retVal += toDelimiteder(obj);
                first = false;
            }
            return retVal;
        }
        /// <summary>
        /// Converts the specified array of objects into a delimited
        /// string of values using the ToString() method, delimited 
        /// by the specified delimiter.
        /// </summary>
        /// <param name="objectsToStringify">array of objects to stringify.</param>
        /// <param name="delimiter">the string to delimit the ouput by</param>
        /// <returns></returns>
        public static string ToDelimited(object[] objectsToStringify, string delimiter)
        {
            List<string> strings = new List<string>();
            foreach (object obj in objectsToStringify)
            {
                strings.Add(obj.ToString());
            }

            return ToDelimited(strings.ToArray(), delimiter);
        }

        public static string ToDelimited(this string[] stringArray, string delimiter)
        {
            return ToDelimited(stringArray, delimiter, false);
        }
        /// <summary>
        /// Converts the specified array of strings into a delimited
        /// string of values.
        /// </summary>
        /// <param name="stringArray">The string array</param>
        /// <param name="delimiter">The character to use to delimit the return string</param>
        /// <returns>string</returns>
        public static string ToDelimited(object[] objectArray, string delimiter, bool quoteText)
        {
            if (objectArray != null)
            {
                StringBuilder retVal = new StringBuilder();
                for (int i = 0; i < objectArray.Length; i++)
                {
                    retVal.Append(quoteText ? "\"" + objectArray[i].ToString() + "\"" : objectArray[i].ToString());

                    if (i != objectArray.Length - 1)
                        retVal.Append(delimiter);
                }

                return retVal.ToString();
            }

            return string.Empty;
        }

        public static Dictionary<string, string> ToDictionary(this string inputString)
        {
            return ToDictionary(inputString, ";", "=");
        }

        public static Dictionary<string, string> ToDictionary(this string inputString, string entrySeparator, string nameValueSeparator)
        {
            return ToDictionary(inputString, new string[] { entrySeparator }, new string[] { nameValueSeparator });
        }

        public static Dictionary<string, string> ToDictionary(this string inputString, string[] entrySeparator, string[] nameValueSeparator)
        {
            return ToDictionary(inputString, entrySeparator, nameValueSeparator, true);
        }

        /// <summary>
        /// Converts the specified inputString into a <seealso cref="Dictionary<string, string>"/>Dictionary&lt;string, string&gt;.
        /// </summary>
        /// <param name="inputString">The input string to convert</param>
        /// <param name="entrySeparator">The string used to separate key value pairs.</param>
        /// <param name="nameValueSeparator">The string used to separate key value relationships.</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this string inputString, string[] entrySeparator, string[] nameValueSeparator, bool trim)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            string[] entries = StringExtensions.DelimitSplit(inputString, entrySeparator);
            foreach (string entry in entries)
            {
                if (string.IsNullOrEmpty(entry.Trim()))
                    continue;
                string[] nameValuePair = StringExtensions.DelimitSplit(entry, nameValueSeparator, trim);
                if (nameValuePair.Length != 2)
                    throw new InvalidStringExtensionsFormatException(inputString);

                retVal.Add(nameValuePair[0], nameValuePair[1]);
            }

            return retVal;
        }

        public static string ToString(this Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Reverses the string.  123 would become 321.
        /// </summary>
        /// <param name="stringToReverse">The string to reverse</param>
        /// <returns></returns>
        public static string Reverse(this string stringToReverse)
        {
            string retVal = string.Empty;
            for (int i = stringToReverse.Length - 1; i > -1; i--)
            {
                retVal += stringToReverse[i].ToString();
            }
            return retVal;
        }

        public static string[] CommaSplit(this string commaSeparatedValues)
        {
            return DelimitSplit(commaSeparatedValues, ",", true);
        }

        public static T[] Split<T>(this string values, StringToObjectDelegate<T> stringToObj)
        {
            return Split<T>(values, ",", stringToObj);
        }

        public static T[] ToObjects<T>(this string[] values, StringToObjectDelegate<T> stringToObj)
        {
            List<T> retVal = new List<T>();
            foreach (string val in values)
            {
                retVal.Add(stringToObj(val));
            }
            return retVal.ToArray();
        }

        /// <summary>
        /// Converts the string to the specified object.  It is
        /// assumed that the string is in the format:
        /// PropertyName=PropertyValue;PropertyName2=PropertyValue2;
        /// </summary>
        /// <typeparam name="T">Type of object to return.</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string value) where T: new()
        {
            T ret = new T();
            Dictionary<string, string> dic = value.ToDictionary();
            foreach (string key in dic.Keys)
            {
                PropertyInfo prop = typeof(T).GetProperty(key);
                if (prop == null)
                    throw new ArgumentException(string.Format("The specified property ({0}) was not found on the specified type ({1}).", key, typeof(T).Name));

                prop.SetValue(ret, dic[key], null);
            }
            return ret;
        }

        public static T ToObject<T>(this string value, StringToObjectDelegate<T> stringToObj)
        {
            return stringToObj(value);
        }

        public static T XmlToObject<T>(this string value)
        {
            return SerializationUtil.FromXmlString<T>(value);
        }

        public static T XmlToObject<T>(this string value, Encoding encoding)
        {
            return SerializationUtil.FromXmlString<T>(value, encoding);
        }

        /// <summary>
        /// Converts the string into an array of objects using the specified
        /// StringToOjbectDelegate to create the objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="delimiter"></param>
        /// <param name="stringToObj"></param>
        /// <returns></returns>
        public static T[] Split<T>(this string values, string delimiter, StringToObjectDelegate<T> stringToObj)
        {
            List<T> retVals = new List<T>();
            foreach (string item in StringExtensions.DelimitSplit(values, delimiter))
            {
                retVals.Add(stringToObj(item));
            }
            return retVals.ToArray();
        }

        public static string Truncate(this string toTruncate, int count)
        {
            if (count > toTruncate.Length)
                return "";

            return toTruncate.Substring(0, toTruncate.Length - count);
        }

        public static string[] SemiColonSplit(this string semicolonSeparatedValues)
        {
            return DelimitSplit(semicolonSeparatedValues, ";");
        }

        public static string[] DelimitSplit(this string valueToSplit, string delimiter)
        {
            return DelimitSplit(valueToSplit, new string[] {delimiter});
        }

        public static string[] DelimitSplit(this string valueToSplit, params string[] delimiters)
        {
            return DelimitSplit(valueToSplit, delimiters, false);
        }

        public static string[] DelimitSplit(this string valueToSplit, string delimiter, bool trimValues)
        {
            return DelimitSplit(valueToSplit, new string[] { delimiter }, trimValues);
        }

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

        public static bool IsNumbers(this string valueToCheck)
        {
            List<char> numberList = new List<char>(new char[]{'0','1','2','3','4','5','6','7','8','9','0'});

            foreach (char c in valueToCheck)
            {
                if (!numberList.Contains(c))
                    return false;
            }

            return true;
        }

        public static bool IsHexChars(this string valueToCheck)
        {
            foreach (char c in valueToCheck.ToUpperInvariant())
            {
                if (!hexChars.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static string[] RemoveDuplicates(this string[] listToDeDupe)
        {
            return RemoveDuplicates(listToDeDupe, true);
        }

        public static string[] RemoveDuplicates(string[] listToDeDupe, bool caseInsensitive)
        {
            List<string> compareList = new List<string>();
            List<string> stringList = new List<string>();
            foreach (string entry in listToDeDupe)
            {
                string compare = entry;
                if (caseInsensitive)
                    compare = compare.ToLowerInvariant();

                if (!compareList.Contains(compare))
                {
                    compareList.Add(compare);
                    stringList.Add(entry);
                }
            }

            return stringList.ToArray();
        }

        public static string Depluralize(this string stringToDepluralize)
        {
            if (stringToDepluralize.ToLowerInvariant().EndsWith("s"))
                return stringToDepluralize.Substring(0, stringToDepluralize.Length - 1);
            else if (stringToDepluralize.ToLowerInvariant().EndsWith("ies"))
                return stringToDepluralize.Substring(0, stringToDepluralize.Length - 3) + "y";
            else if (stringToDepluralize.ToLowerInvariant().EndsWith("i"))
                return stringToDepluralize.Substring(0, stringToDepluralize.Length - 1) + "us";
            else
                return stringToDepluralize;
        }

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
            else if (stringToPluralize.ToLowerInvariant().EndsWith("s"))
            {
                return stringToPluralize + "es";
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

        public static string PascalSplit(this string stringToPascalSplit, string separator)
        {
            StringBuilder returnValue = new StringBuilder();
            for(int i = 0; i < stringToPascalSplit.Length; i++)
            {
                char next = stringToPascalSplit[i];                

                if (char.IsUpper(next) && i > 0)
                {
                    returnValue.Append(separator);
                }

                returnValue.Append(next);
            }

            return returnValue.ToString();
        }

        public static string CamelSplit(this string stringToCamelSplit, string separator)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < stringToCamelSplit.Length; i++)
            {
                char next = stringToCamelSplit[i];

                if (i == 0)
                {
                    returnValue.Append(next.ToString().ToUpperInvariant());
                }
                else if (char.IsUpper(next) && i > 0)
                {
                    returnValue.Append(separator);
                    returnValue.Append(next);
                }
                else
                {
                    returnValue.Append(next);
                }
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// Returns a camel cased string from the specified string using the specified 
        /// separators.  For example, the input "The quick brown fox jumps over the lazy
        /// dog" with the separators of "new string[]{" "}" should return the string 
        /// "TheQuickBrownFoxJumpsOverTheLazyDog".
        /// </summary>
        /// <param name="stringToCamelize">The input string</param>
        /// <param name="separators">The character to split the input string at before
        /// camelization.</param>
        /// <returns>string</returns>
        public static string CamelCase(this string stringToCamelize, params string[] separators)
        {
            return CamelCase(stringToCamelize, true, separators);
        }

        /// <summary>
        /// The same as CamelCase EXCEPT the first letter of the return value is lowercase.
        /// </summary>
        /// <param name="stringToPascalize"></param>
        /// <returns></returns>
        public static string PascalCase(this string stringToPascalize)
        {
            return PascalCase(stringToPascalize, " ");
        }

        /// <summary>
        /// The same as CamelCase EXCEPT the first letter of the return value is lowercase.
        /// </summary>
        /// <param name="stringToPascalize"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static string PascalCase(this string stringToPascalize, params string[] separators)
        {
            string camelCase = CamelCase(stringToPascalize, separators);

            return camelCase.Substring(0, 1).ToLowerInvariant() + camelCase.Substring(1, camelCase.Length - 1);
        }

        /// <summary>
        /// Returns only the capital letters after calling CamelCase on the specified 
        /// stringToAcronym.
        /// </summary>
        /// <param name="stringToAcronym">The target string.</param>
        /// <param name="separators">The CamelCase separators to use</param>
        /// <returns>string</returns>
        public static string CamelAcronym(this string stringToAcronym, params string[] separators)
        {
            string ret = string.Empty;
            string camel = CamelCase(stringToAcronym, separators);
            foreach (char c in camel.ToCharArray())
            {
                ret += char.IsUpper(c) ? c.ToString() : "";
            }

            if (ret.Equals(""))
                ret = camel[0].ToString();

            return ret;
        }

        /// <summary>
        /// Returns a camel cased string from the specified string using the specified 
        /// separators.  For example, the input "The quick brown fox jumps over the lazy
        /// dog" with the separators of "new string[]{" "}" should return the string 
        /// "TheQuickBrownFoxJumpsOverTheLazyDog".
        /// </summary>
        /// <param name="stringToCamelize"></param>
        /// <param name="preserveInnerUppers">If true uppercase letters that appear in 
        /// the middle of a word remain uppercase if false they are converted to lowercase.</param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static string CamelCase(this string stringToCamelize, bool preserveInnerUppers, params string[] separators)
        {
            string[] splitString = stringToCamelize.Split(separators, StringSplitOptions.RemoveEmptyEntries);
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

        /// <summary>
        /// Returns a string in the format Y_Mo_D_H_Mi_S where Y is the current year,
        /// Mo is the current month,
        /// D is the current date, H is the current hour, Mi is the current minute and S
        /// is the current second.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FileNameFromDateTime(DateTime dateTime)
        {
            string fileName = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second);
            return fileName;
        }

        public static string SHA1(this string toHash)
        {
            return SHA1(toHash, Encoding.UTF8);
        }

        public static string SHA1(this string toHash, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(toHash);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(sha1.ComputeHash(data));
        }


        public static string RandomString(this string result, int length)
        {
            for (int i = 0; i < length; i++)
            {

                char ch = Convert.ToChar(RandomHelper.Next(97, 122));

                result += ch;
            }

            return result;
        }

        //public static string RandomString(this string aString, int length)
        //{
        //    return aString + RandomString(length);
        //}

        public static string RandomString(int length)
        {
            return RandomString(length, true, true);
        }

        //public static string RandomString(int length, bool mixCase)
        //{
        //    return RandomString(length, mixCase, true);
        //}

        public static string RemoveLetters(this string targetString)
        {
            string retVal = string.Empty;
            List<string> lowerCaseLetters = new List<string>(letters);
            foreach (char c in targetString)
            {
                string character = c.ToString().ToLower();
                if (!lowerCaseLetters.Contains(character))
                    retVal += character;
            }

            return retVal;
        }

        public static string RemoveCharacter(this string targetString, char targetChar)
        {
            string retVal = string.Empty;
            foreach (char c in targetString)
            {
                if (!c.Equals(targetChar))
                    retVal += c.ToString();
            }
            return retVal;
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

        static List<char> hexChars = new List<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' });
        public static bool IsValidHtmlHexColor(this string htmlColorStringToValidate)
        {
            htmlColorStringToValidate = htmlColorStringToValidate.Trim().ToUpper();

            if (htmlColorStringToValidate.Length != 7)
                return false;

            if (!htmlColorStringToValidate.StartsWith("#"))
                return false;

            List<char> characters = new List<char>(htmlColorStringToValidate.ToCharArray());
            characters.RemoveAt(0);
            foreach (char character in characters)
            {
                if (!hexChars.Contains(character))
                    return false;
            }

            return true;
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

        //private static string EnforceMixedCase(string
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

        /// <summary>
        /// Returns a random lowercase letter from a-z."
        /// </summary>
        /// <returns>String</returns>
        public static string RandomLetter()
        {
            return letters[RandomHelper.Next(0, 26)];
        }

        static int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

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
            //return numbers[RandomHelper.Next(0, max)];
        }

        public static bool RandomBool()
        {
            return RandomHelper.Next(2) == 1;
        }

        public static string ToValidFileName(this string stringToConvert)
        {
            return stringToConvert.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
        }

        public static string ToCSharpVariableNameWithoutNumbers(this string stringToConvert)
        {
            return ToCSharpVariableName(stringToConvert, false);
        }

        public static string ToCSharpVariableName(this string stringToConvert)
        {
            return ToCSharpVariableName(stringToConvert, true);
        }

        public static string ToCSharpVariableName(this string stringToConvert, bool allowNumbers)
        {
            string retVal = string.Empty;
            List<string> lowerCase = new List<string>(letters);
            bool first = true;
            foreach (char character in stringToConvert)
            {
                if (lowerCase.Contains(character.ToString().ToLower()))
                {
                    retVal += character.ToString();
                }
                else
                {
                    int num;
                    if (int.TryParse(character.ToString(), out num) && allowNumbers && !first)
                    {
                        retVal += character.ToString();
                    }
                }

                first = false;
            }

            return retVal;
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
                using(StreamReader sr = new StreamReader(filePath))
                {
                    retVal = sr.ReadToEnd();
                }
            }
            return retVal;
        }

        static Dictionary<string, object> safeWriteLock = new Dictionary<string, object>();
        public static void SafeWriteFile(this string filePath, string textToWrite)
        {
            SafeWriteFile(filePath, textToWrite, false);
        }

        public static void SafeWriteToFile(this string textToWrite, string filePath)
        {
            filePath.SafeWriteFile(textToWrite);
        }

        public static void SafeWriteToFile(this string textToWrite, string filePath, bool overwrite)
        {
            filePath.SafeWriteFile(textToWrite, overwrite);
        }

        /// <summary>
        /// Write the specified text to the specified file in a thread safe way.
        /// </summary>
        /// <param name="filePath">The path to the file to write.</param>
        /// <param name="textToWrite">The text to write.</param>
        /// <param name="overwrite">True to overwrite.  If false and the file exists an InvalidOperationException will be thrown.</param>
        public static void SafeWriteFile(this string filePath, string textToWrite, bool overwrite)
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
                }
            }
        }
    }
}
