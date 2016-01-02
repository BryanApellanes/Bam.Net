/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.Util;

namespace Naizari.Helpers.Web
{
    public enum BrowserAccessRuleResult
    {
        Invalid,
        Pass,
        Fail
    }

    [Serializable]
    public class BrowserAccessRule
    {
        public string Browser { get; set; }
        public int MinimumMajorVersion { get; set; }
        public double MinimumMinorVersion { get; set; }

        public static BrowserAccessRule FromCurrentContext()
        {
            if(HttpContext.Current == null)
                return null;

            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            BrowserAccessRule retVal = new BrowserAccessRule();
            retVal.Browser = browser.Browser;
            retVal.MinimumMajorVersion = browser.MajorVersion;
            retVal.MinimumMinorVersion = browser.MinorVersion;

            return retVal;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}.{2}", this.Browser, this.MinimumMajorVersion, this.MinimumMinorVersion);
        }

        public BrowserAccessRuleResult Result
        {
            get
            {
                if (HttpContext.Current == null)
                    return BrowserAccessRuleResult.Invalid;

                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
                string currentBrowserName = browser.Browser;
                int currentMajorVersion = browser.MajorVersion;
                double currentMinorVersion = browser.MinorVersion;

                BrowserAccessRuleResult result = 
                    (currentBrowserName.Equals(this.Browser) && 
                    currentMajorVersion >= this.MinimumMajorVersion)
                    ? BrowserAccessRuleResult.Pass: BrowserAccessRuleResult.Fail;

                return result;
            }
        }
    }
}
