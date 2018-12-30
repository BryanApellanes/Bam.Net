/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace Bam.Net.OAuth
{
    public static class Extensions
    {
        public static string ApplicationUrl(this HttpContextBase context)
        {
            string appPath = context.Request.ApplicationPath;
            if (!appPath.EndsWith("/"))
            {
                appPath = string.Format("{0}/", appPath);
            }

            string baseUrl = context.Request.Url.GetLeftPart(UriPartial.Authority) + appPath;
            return baseUrl;
        }

        public static Dictionary<string, string> ToDictionary(this string input, string ed, string kvd)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string s in input.Split(new string[] { ed }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] kv = s.Split(new string[] { kvd }, StringSplitOptions.RemoveEmptyEntries);
                result.Add(kv[0], kv[1]);
            }

            return result;
        }
    }
}
