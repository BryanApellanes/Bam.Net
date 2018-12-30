/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Util;
using Naizari.Configuration;
using System.IO;

namespace Naizari.Helpers.Web
{
    public static class BrowserAccessHelper
    {
        static BrowserAccessInfo browserAccessInfo;

        static BrowserAccessHelper()
        {
            Initialize();
        }

        public static void Initialize()
        {
            // instantiate here so the ApprovedBrowserXmlPath can be determined from the config file.
            browserAccessInfo = new BrowserAccessInfo();
            string localPath = HttpContext.Current.Server.MapPath(ApprovedBrowserXmlPath);
            if (!File.Exists(localPath))
            {
                BrowserAccessInfo temp = new BrowserAccessInfo();
                temp.AddRule(BrowserAccessRule.FromCurrentContext());
                SerializationUtil.XmlSerialize(temp, localPath);
            }

            // assign the deserialized version to the current instance;
            browserAccessInfo = SerializationUtil.DeserializeFromFile<BrowserAccessInfo>(localPath);
        }

        public static bool IsIELessThanOrEqualTo(int majorVersion)
        {
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string currentBrowserName = browser.Browser;
            int currentMajorVersion = browser.MajorVersion;

            return currentBrowserName.Equals("IE") && currentMajorVersion <= majorVersion;
        }

        public static bool IsIEGreaterThan(int majorVersion)
        {
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string currentBrowserName = browser.Browser;
            int currentMajorVersion = browser.MajorVersion;

            return currentBrowserName.Equals("IE") && currentMajorVersion >= majorVersion;
        }

        public static bool IsFireFoxGreaterThan(int majorVersion)
        {
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string currentBrowserName = browser.Browser;
            int currentMajorVersion = browser.MajorVersion;

            return currentBrowserName.Equals("Firefox") && currentMajorVersion >= majorVersion;
        }

        public static bool IsApproved
        {
            get
            {
                return browserAccessInfo.IsApproved;
            }
        }

        public static BrowserAccessRule[] ApprovedBrowserRules
        {
            get
            {
                return browserAccessInfo.BrowserAccessRules;
            }
        }
        /// <summary>
        /// The path to the serialized UserAgentAccessInfo file to deserialize.
        /// </summary>
        public static string ApprovedBrowserXmlPath
        {
            get
            {
                DefaultConfiguration.SetProperties(browserAccessInfo);
                if (string.IsNullOrEmpty(browserAccessInfo.ApprovedBrowserXmlPath))
                {
                    return "~/App_Data/ApprovedBrowsers.xml";
                }
                else
                {
                    return browserAccessInfo.ApprovedBrowserXmlPath;
                }
            }
        }

        
    }
}
