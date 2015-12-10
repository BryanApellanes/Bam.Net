/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using Naizari.Testing;
using Naizari.Logging;

namespace Naizari.Helpers
{
    public enum IISAuthenticationMode
    {
        Anonymous = 1,
        IntegratedWindows = 4,
        // Passport = 8,  this is a quess
        Digest = 16,
        Basic = 2
    }

    public static class IISHelper
    {
        public const string MetabaseFormat = "IIS://{0}/W3SVC";
        public const string MetabaseSiteFormat = "IIS://{0}/W3SVC/{1}";
        public const string MetabaseSiteRootFormat = "IIS://{0}/W3SVC/{1}/Root";
        public const string MetabaseAppPoolFormat = "IIS://{0}/W3SVC/AppPools";

        /// <summary>
        /// Create a new website.  The current process owner should have the proper permissions to
        /// perform the operation.
        /// </summary>
        /// <param name="serverName">The name of the server to create the site on</param>
        /// <param name="siteID">A number in the form &lt;number&gt;, for example "555"</param>
        /// <param name="siteName">The name of the new site in the &lt;name&gt;, for example "My New Site"</param>
        /// <param name="physicalPath">The physical root of the site on the server</param>
        public static void CreateSite(string serverName, int siteID, string siteName, int port, string physicalPath)
        {
            Log.Default.AddEntry("Creating site {0}/{1} on server {2}", LogEventType.Information, new string[] { siteID.ToString(), siteName, serverName });

            DirectoryEntry service = new DirectoryEntry(string.Format(MetabaseFormat, serverName));
            string className = service.SchemaClassName.ToString();
            Expect.IsTrue(className.EndsWith("Service"), "A site can only be created in a service node.");

            DirectoryEntries sites = service.Children;
            DirectoryEntry newSite = sites.Add(siteID.ToString(), className.Replace("Service", "Server"));
            newSite.Properties["ServerComment"][0] = siteName;
            newSite.CommitChanges();

            DirectoryEntry newRoot = newSite.Children.Add("Root", "IIsWebVirtualDir");
            newRoot.Properties["Path"][0] = physicalPath;
            newRoot.Properties["AccessScript"][0] = true;
            
            newRoot.CommitChanges();

            SetSiteProperty(serverName, siteID.ToString(), "ServerBindings", ":" + port.ToString() + ":");
        }

        public static void StartWebSite(string serverName, int siteID)
        {
            DirectoryEntry site = new DirectoryEntry(string.Format(MetabaseSiteFormat, serverName, siteID));
            site.Invoke("start", new object[] { });
            site.Close();
        }

        public static void DeleteSite(string serverName, int siteID)
        {
            DirectoryEntry site = new DirectoryEntry(string.Format(MetabaseSiteFormat, serverName, siteID.ToString()));
            site.DeleteTree();
            site.CommitChanges();
        }

        public static void CreateAppPool(string serverName, string appPoolName)
        {
            DirectoryEntry appPools = new DirectoryEntry(string.Format(MetabaseAppPoolFormat, serverName));
            DirectoryEntry newAppPool = appPools.Children.Add(appPoolName, "IISApplicationPool");
            newAppPool.CommitChanges();
        }

        public static void CreateVirtualDirectory(string serverName, int siteID, string virtualDirectoryName, string physicalPath)
        {
            // took this code almost verbatum from 
            // http://msdn.microsoft.com/en-us/library/ms524896.aspx

            string metabasePath = string.Format(MetabaseSiteRootFormat, serverName, siteID.ToString());
            DirectoryEntry site = new DirectoryEntry(metabasePath);
            string className = site.SchemaClassName.ToString();
            Expect.IsTrue(className.EndsWith("Server") || className.EndsWith("VirtualDir"), "A virtual directory can only be created in a site or virtual directory node");

            DirectoryEntries vdirs = site.Children;
            DirectoryEntry newVDir = vdirs.Add(virtualDirectoryName, className.Replace("Service", "VirtualDir"));
            newVDir.Properties["Path"][0] = physicalPath;
            newVDir.Properties["AccessScript"][0] = true;
            newVDir.Properties["AppFriendlyName"][0] = virtualDirectoryName;
            newVDir.Properties["AppIsolated"][0] = 1;
            newVDir.Properties["AppRoot"][0] = "/LM" + metabasePath.Substring(metabasePath.IndexOf("/", "IIS://".Length));
            
            newVDir.CommitChanges();
        }

        public static void SetSiteProperty(string serverName, string siteID, string propertyName, object newValue)
        {
            DirectoryEntry path = new DirectoryEntry(string.Format(MetabaseSiteFormat, serverName, siteID));
            //PropertyValueCollection propValues = path.Properties[propertyName];

            SetProperty(path, propertyName, newValue);
        }

        private static void SetProperty(DirectoryEntry target, string propertyName, object newValue)
        {
            if (target.Properties[propertyName].Count == 0)
            {
                target.Properties[propertyName].Add(newValue);
            }
            else
            {
                target.Properties[propertyName][0] = newValue;
            }

            target.CommitChanges();
        }

        public static void SetAuthenticationMode(string serverName, int siteID, string appVDir, IISAuthenticationMode authMode)
        {
            DirectoryEntry app = GetVDir(serverName, siteID, appVDir);
            SetAuthenticationMode(app, authMode);
        }

        public static void SetAuthenticationMode(DirectoryEntry siteOrVDir, IISAuthenticationMode authMode)
        {
            SetProperty(siteOrVDir, "AuthFlags", (int)authMode);
        }

        public static void SetVDirProperty(string serverName, int siteID, string vDirName, string propertyName, object newValue)
        {
            DirectoryEntry vDir = GetVDir(serverName, siteID, vDirName);
            if (vDir.Properties[propertyName].Count == 0)
            {
                vDir.Properties[propertyName].Add(newValue);
            }
            else
            {
                vDir.Properties[propertyName][0] = newValue;
            }

            vDir.CommitChanges();
        }

        public static DirectoryEntry GetVDir(string serverName, int siteID, string vDirName)
        {
            string metabasePath = string.Format(MetabaseSiteRootFormat, serverName, siteID.ToString());
            string vDirPath = metabasePath + "/" + vDirName;
            return new DirectoryEntry(vDirPath);
        }

        public static void CreateApp(string serverName, int siteID, string vDirName)
        {
            DirectoryEntry app = GetVDir(serverName, siteID, vDirName);
            app.Properties["AppFriendlyName"][0] = vDirName;
            app.Properties["AppIsolated"][0] = 1;
            app.Invoke("AppCreate", true);
            app.CommitChanges();
        }

        public static bool VirtualDirectoryExists(string serverName, int siteID, string vDirName)
        {
            bool exists = false;
            try
            {
                DirectoryEntry vDir = GetVDir(serverName, siteID, vDirName);
                vDir.RefreshCache(); // should throw error if vDir doesn't exist
                exists = true;
            }
            catch
            {
            }

            return exists;
            //DirectoryEntry siteRoot = GetSiteRoot(serverName, siteID);
            //bool exists = false;
            //foreach (object child in siteRoot.Children)
            //{
            //    DirectoryEntry app = child as DirectoryEntry;
            //    if (app != null)
            //    {
            //        if (app.Properties.Count > 0)
            //        {
            //            object appName = app.Properties["AppFriendlyName"][0];
            //            if (appName.ToString().ToLowerInvariant().Equals(vDirName.ToLowerInvariant()))
            //            {
            //                exists = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            //return exists;
        }

        public static DirectoryEntry GetSite(string serverName, int siteID)
        {
            return GetSite(serverName, siteID.ToString());
        }

        public static DirectoryEntry GetSiteRoot(string serverName, int siteID)
        {
            return new DirectoryEntry(string.Format(MetabaseSiteRootFormat, serverName, siteID));
        }

        public static DirectoryEntry GetSite(string serverName, string siteID)
        {
            return new DirectoryEntry(string.Format(MetabaseSiteFormat, serverName, siteID));
        }
    }
}
