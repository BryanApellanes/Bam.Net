/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Exegete
{
    public partial class Culture: IRequiresHttpContext // core
    {
        public CultureInfo ResolveCulture()
        {
            CultureInfo result = CultureInfo.CurrentCulture;
			string[] languages = new string[] { };
			if(this.HttpContext != null)
			{
				languages = this.HttpContext.Request.UserLanguages;
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
