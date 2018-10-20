/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Presentation;
using Bam.Net;
using System.Web.Mvc;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;
using Bam.Net.Presentation.Html;

namespace Bam.Net.OAuth
{
    public static class ServiceProxyProfileExtensions
    {
        public const string ProfileImageUrlFormat = "https://graph.facebook.com/{0}/picture";
        
        public static MvcHtmlString FacebookProfileImage(this ServiceProxyHelper helper, object htmlAttributes = null)
        {
            return helper.FacebookProfileImage("me", htmlAttributes);
        }

        public static MvcHtmlString FacebookProfileImage(this ServiceProxyHelper helper, string id, object htmlAttributes = null)
        {
            return new TagBuilder("img")
                .Attr("src", string.Format(ProfileImageUrlFormat, id))
                .AttrsIf(htmlAttributes != null, htmlAttributes)
                .ToHtml();
        }
    }
}
