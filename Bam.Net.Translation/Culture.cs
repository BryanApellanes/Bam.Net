/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.Security;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Exegete
{
    public class Culture: IRequiresHttpContext
    {
        public static event UnhandledExceptionEventHandler ResolveCultureException;

        public static event UnhandledExceptionEventHandler ResolveRegionException;

        public CultureInfo ResolveCulture()
        {
            CultureInfo result = CultureInfo.CurrentCulture;
			string[] languages = new string[] { };
			if(this.HttpContext != null)
			{
				languages = this.HttpContext.Request.UserLanguages;
			}
			else if (System.Web.HttpContext.Current != null &&
			   System.Web.HttpContext.Current.Request != null &&
			   System.Web.HttpContext.Current.Request.UserLanguages != null)
			{
				languages = System.Web.HttpContext.Current.Request.UserLanguages;
			}

			if (languages.Length > 0)
			{
				CultureInfo fromLanguages = TryGetFirstCultureFromLangugages(languages);
				if (fromLanguages != null)
				{
					result = fromLanguages;
				}
			}
            return result;
        }

        public RegionInfo ResolveRegion()
        {
            CultureInfo culture = ResolveCulture();
            return new RegionInfo(culture.LCID);            
        }

		#region IRequiresHttpContext Members

		public IHttpContext HttpContext
		{
			get;
			set;
		}

        public object Clone()
        {
            Culture clone = new Culture();
            clone.CopyProperties(this);
            return clone;
        }

		#endregion
		
		private static CultureInfo TryGetFirstCultureFromLangugages(string[] languages)
		{
			CultureInfo result = null;
			if (languages != null && languages.Length > 0)
			{
				try
				{
					string language = languages[0].ToLowerInvariant().Trim();
					result = CultureInfo.CreateSpecificCulture(language);
				}
				catch (Exception ex)
				{
					OnResolveCultureException(ex);
				}
			}
			return result;
		}
		private static void OnResolveCultureException(Exception ex)
		{
			if (ResolveCultureException != null)
			{
				ResolveCultureException(null, new UnhandledExceptionEventArgs(ex, false));
			}
		}

		private static void OnResolveRegionException(Exception ex)
		{
			if (ResolveRegionException != null)
			{
				ResolveRegionException(null, new UnhandledExceptionEventArgs(ex, false));
			}
		}

	}
}
