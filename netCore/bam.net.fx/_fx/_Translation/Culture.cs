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
    public partial class Culture: IRequiresHttpContext // fx
    {
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
				CultureInfo fromLanguages = TryGetFirstCultureFromLanguages(languages);
				if (fromLanguages != null)
				{
					result = fromLanguages;
				}
			}
            return result;
        }     
	}
}
