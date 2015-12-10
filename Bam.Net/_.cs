/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Web;

namespace Bam.Net
{
	/// <summary>
	/// Repurposed magic underscore to be a convenience wrapper
	/// to the Extensions static class.  May consider implementing
	/// CSharp version of lodash functions or at least some
	/// subset to start (https://lodash.com/).
	/// </summary>
	public static class _
	{
		public static string RandomString(int count)
		{
			return Extensions.RandomString(count);
		}

		public static string RandomLetters(int count)
		{
			return Extensions.RandomLetters("", count);
		}

		public static string Get(string url, Dictionary<string, string> headers = null)
		{
			return Get(new Uri(url), headers);
		}

		public static string Get(Uri uri, Dictionary<string, string> headers = null)
		{
			return Http.Get(uri.AbsoluteUri, headers);
		}

		public static T GetJson<T>(string url, Dictionary<string, string> headers = null)
		{
			return Http.GetJson<T>(url, headers);
		}

		public static T GetXml<T>(string url, Dictionary<string, string> headers = null)
		{
			return Http.GetXml<T>(url, headers);
		}
	}
}
