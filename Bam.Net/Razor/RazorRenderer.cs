/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Razor
{
    public class RazorRenderer
    {
        /// <summary>
        /// Render the specified generic type T using the 
        /// specified embedded resource template.  The template
        /// must be embedded in the same Assembly where 
        /// T is defined in a subdirectory of "Templates"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static string RenderResource<T>(T model, string templateName, IEnumerable<Assembly> assembliesToReference = null)
        {
            Type type = typeof(T);
            string namespacePath = string.Format("{0}.Templates.", type.Namespace);
            RazorParser<RazorTemplate<T>> razorParser = GetRazorParser<T>(assembliesToReference);
            return razorParser.ExecuteResource(templateName, namespacePath, type.Assembly, new { Model = model });        
        }

        public static string Render<T>(T model, FileInfo razorFile, IEnumerable<Assembly> assembliesToReference = null)
        {
            RazorParser<RazorTemplate<T>> razorParser = GetRazorParser<T>();
            using(StreamReader sr = new StreamReader(razorFile.FullName))
            {
                return razorParser.Execute(sr, new { Model = model });
            }
        }

        public static string Render<T>(T model, Stream razorTemplate, IEnumerable<Assembly> assembliesToReference = null)
        {
            RazorParser<RazorTemplate<T>> razorParser = GetRazorParser<T>();
            using (StreamReader sr = new StreamReader(razorTemplate))
            {
                return razorParser.Execute(sr, new { Model = model });
            }
        }

        private static RazorParser<RazorTemplate<T>> GetRazorParser<T>(IEnumerable<Assembly> assembliesToReference = null)
        {
            RazorParser<RazorTemplate<T>> razorParser = new RazorParser<RazorTemplate<T>>();
            razorParser.GetDefaultAssembliesToReference = () => assembliesToReference == null ? new Assembly[] { } : assembliesToReference.ToArray();
            return razorParser;
        }

    }
}
