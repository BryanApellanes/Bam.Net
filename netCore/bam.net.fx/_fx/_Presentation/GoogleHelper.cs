/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bam.Net;
using Bam.Net.Presentation.Html;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;

namespace Bam.Net.Presentation
{
    public static class Google
    {
        public static MvcHtmlString Analytics(this ServiceProxyHelper helper, string trackingId)
        {
            return new TagBuilder("script")
                .Attr("type", "text/javascript")
                .Html(@"
              var _gaq = _gaq || [];
              _gaq.push(['_setAccount', '" + MvcHtmlString.Create(trackingId).ToString() + @"']);
              _gaq.push(['_trackPageview']);

              (function() {
                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
              })();
")
                .ToHtml();
                
        }
    }
}
