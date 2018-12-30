/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Web;
using Naizari.Configuration;

namespace Naizari.Helpers
{
    public static class FsUtil
    {
        public const string CompanyName = "Naizari";

        public static string GetExecutionFolder()
        {
            return GetFolder(Assembly.GetExecutingAssembly());
        }

        public static string GetCallingAssemblyPath()
        {
            return GetFullAssemblyPath(Assembly.GetCallingAssembly());
        }


        /// <summary>
        /// Gets the path to the current user's AppData folder. If
        /// this is run in a Web app (HttpContext.Current isn't null)
        /// then the full path to ~/AppData/ is returned. 
        /// </summary>
        /// <param name="webSession">If true, domain_userName is appended to the path.</param>
        /// <returns></returns>
        public static string GetCurrentUserAppDataFolder()
        {
            return GetCurrentUserAppDataFolder(false);
        }

        /// <summary>
        /// Gets the path to the current user's AppData folder. If
        /// this is run in a Web app (HttpContext.Current isn't null)
        /// then the full path to ~/AppData/ is returned. 
        /// </summary>
        /// <param name="webSession">If true, domain_userName is appended to the path.  The default value is false.</param>
        /// <returns></returns>
        public static string GetCurrentUserAppDataFolder(bool webSession)
        {
            string path = "";
            if (HttpContext.Current == null)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!path.EndsWith("\\"))
                    path += "\\";

                path += FsUtil.CompanyName + "\\" + DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN") + "\\";
                FileInfo fileInfo = new FileInfo(path);
                if (!Directory.Exists(fileInfo.Directory.FullName))
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            else
            {
                path = HttpContext.Current.Server.MapPath("~/App_Data/");
                string webUser = UserUtil.GetCurrentWebUserName(true).Replace("\\", "_");
                if (webSession && !string.IsNullOrEmpty(webUser))
                {
                    path += webUser + "\\";
                }
            }

            return path;
        }
        /// <summary>
        /// This serves as a convenience method which returns the Location property
        /// of the specified assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetFullAssemblyPath(Assembly assembly)
        {
            if (assembly != null)
                return assembly.Location;
            else
                return string.Empty;
        }

        public static string GetFolder(Assembly assembly)
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            string[] folders = assembly.Location.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            string path = string.Empty;
            for (int i = 0; i < folders.Length - 1; i++)
            {
                path += folders[i];
                if (i != folders.Length - 1)
                    path += "\\";
            }

            return path;
        }


        public static FileInfo[] GetFilesWithExtension(string rootFolder, string extension)
        {
            DirectoryInfo workingFolderInfo = new DirectoryInfo(rootFolder);
            List<FileInfo> retVals = new List<FileInfo>();

            if (!extension.StartsWith("."))
                extension = "." + extension;

            retVals.AddRange(workingFolderInfo.GetFiles("*" + extension));

            foreach (DirectoryInfo subDirectory in workingFolderInfo.GetDirectories())
            {
                retVals.AddRange(GetFilesWithExtension(subDirectory.FullName, extension));
            }

            return retVals.ToArray();
        }

        public static FileInfo[] SearchFiles(string rootFolder, string searchPattern)
        {
            DirectoryInfo workingFolderInfo = new DirectoryInfo(rootFolder);
            List<FileInfo> retVals = new List<FileInfo>();

            retVals.AddRange(workingFolderInfo.GetFiles(searchPattern));

            foreach (DirectoryInfo subDirectory in workingFolderInfo.GetDirectories())
            {
                retVals.AddRange(SearchFiles(subDirectory.FullName, searchPattern));
            }

            return retVals.ToArray();
        }

        public static void SetFileAttributes(DirectoryInfo directory, FileAttributes attributes)
        {
            SetFileAttributes(directory.FullName, attributes);
        }

        public static void SetFileAttributes(string directoryPath, FileAttributes attributes)
        {
            foreach (string directory in Directory.GetDirectories(directoryPath))
            {
                SetFileAttributes(directory, attributes);
            }

            foreach (string file in Directory.GetFiles(directoryPath))
            {
                File.SetAttributes(file, attributes);
            }
        }

        public static string GetNameWithoutExtension(string filePath)
        {
            return GetNameWithoutExtension(new FileInfo(filePath));
        }

        public static string GetNameWithoutExtension(FileInfo file)
        {
            return file.Name.Replace(file.Extension, "");
        }

        public static string GetAssemblyName(Assembly assembly)
        {
            string[] folders = assembly.Location.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            return folders[folders.Length - 1];
        }
        /// <summary>
        /// Allows the specified identity the default read only permissions.
        /// </summary>
        /// <param name="identity">The account name in &lt;domain&gt;\username&lt;&gt; format for the person
        /// or group to grant read perimissions to.
        /// </param>
        /// <param name="folderPath">The local or UNC directory path to modify permissions for.</param>
        public static void AllowDefaultPermissions(string userOrGroupNameWithDomainPrefix, string folderPath)
        {
            //if( !Directory.Exists(folderPath) )
            //    throw new InvalidOperationException(string.Format("{0} was not found", folderPath));
            AllowDefaultPermissions(userOrGroupNameWithDomainPrefix, new DirectoryInfo(folderPath));
        }

        public static void AllowModifyPermissions(string userOrGroupNameWithDomainPrefix, string folderPath)
        {
            AllowModifyPermissions(userOrGroupNameWithDomainPrefix, new DirectoryInfo(folderPath));
        }

        public static void AllowFullControlPermissions(string userOrGroupNameWithDomainPrefix, string folderPath)
        {
            AllowFullControlPermissions(userOrGroupNameWithDomainPrefix, new DirectoryInfo(folderPath));
        }

        public static void DeleteAccessRule(string userOrGroupNameWithDomainPrefix, DirectoryInfo directoryInfo)
        {
            DirectorySecurity dirSec = directoryInfo.GetAccessControl();
            AuthorizationRuleCollection authRules = dirSec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            foreach (FileSystemAccessRule rule in authRules)
            {
                if (rule.IdentityReference.Value.ToLower().Equals(userOrGroupNameWithDomainPrefix.ToLower()))
                {
                    dirSec.RemoveAccessRule(rule);
                    break;
                }
            }

            directoryInfo.SetAccessControl(dirSec);
        }

        public static void AllowFullControlPermissions(string userOrGroupNameWithDomainPrefix, DirectoryInfo directoryInfo)
        {
            AllowPermissions(userOrGroupNameWithDomainPrefix, directoryInfo, FileSystemRights.FullControl);
        }

        public static void AllowModifyPermissions(string userOrGroupNameWithDomainPrefix, DirectoryInfo directoryInfo)
        {
            AllowPermissions(userOrGroupNameWithDomainPrefix, directoryInfo, FileSystemRights.Modify);
        }

        public static void AllowDefaultPermissions(string userOrGroupNameWithDomainPrefix, DirectoryInfo directoryInfo)
        {
            AllowPermissions(userOrGroupNameWithDomainPrefix, directoryInfo, FileSystemRights.ReadAndExecute);
            //DirectorySecurity dirSec = directoryInfo.GetAccessControl();
            //FileSystemAccessRule newRule = new FileSystemAccessRule(userOrGroupNameWithDomainPrefix,
            //    FileSystemRights.ReadAndExecute,
            //    InheritanceFlags.ObjectInherit ^ InheritanceFlags.ContainerInherit, 
            //    PropagationFlags.None, AccessControlType.Allow);
            //dirSec.AddAccessRule(newRule);
            //directoryInfo.SetAccessControl(dirSec);
        }

        public static void AllowPermissions(string userOrGroupNameWithDomainPrefix, DirectoryInfo directoryInfo, FileSystemRights rights)
        {
            DirectorySecurity dirSec = directoryInfo.GetAccessControl();
            FileSystemAccessRule newRule = new FileSystemAccessRule(userOrGroupNameWithDomainPrefix,
                rights, 
                InheritanceFlags.ObjectInherit ^ InheritanceFlags.ContainerInherit,
                PropagationFlags.None, AccessControlType.Allow);
            dirSec.AddAccessRule(newRule);
            directoryInfo.SetAccessControl(dirSec);
            directoryInfo.Refresh();
        }

        public static string GetDirectoryAclStringDebug(string directoryPath)
        {
            AuthorizationRuleCollection authRules = GetAuthorizationRules(directoryPath);
            StringBuilder retString = new StringBuilder();
            foreach (FileSystemAccessRule rule in authRules)
            {
                retString.AppendLine(rule.IdentityReference.Value + " inherited: " + rule.IsInherited.ToString() + "\tPropagation Flags: " + rule.PropagationFlags.ToString());
                retString.AppendLine("\t" + rule.AccessControlType.ToString());
                retString.AppendLine("\t" + rule.FileSystemRights.ToString());
            }

            return retString.ToString();
        }

        public static bool UserHasModifyRights(string directoryPath, string domainSlashUserName)
        {
            AuthorizationRuleCollection authRules = GetAuthorizationRules(directoryPath);
            foreach (FileSystemAccessRule rule in authRules)
            {
                if (rule.IdentityReference.Value.ToLower().Equals(domainSlashUserName.ToLower()))
                {
                    if (rule.AccessControlType == AccessControlType.Allow)
                    {
                        if ((int)rule.FileSystemRights == 197055 || (int)rule.FileSystemRights == 1245631)
                            return true;
                    }
                }
            }

            return false;
        }

        public static bool UserHasNoAccessRule(string directoryPath, string domainSlashUserName)
        {
            AuthorizationRuleCollection authRules = GetAuthorizationRules(directoryPath);
            foreach (FileSystemAccessRule rule in authRules)
            {
                if(rule.IdentityReference.Value.ToLower().Equals(domainSlashUserName.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        public static AuthorizationRuleCollection GetAuthorizationRules(string directoryPath)
        {
            DirectoryInfo info = new DirectoryInfo(directoryPath);
            DirectorySecurity dirSec = info.GetAccessControl();

            AuthorizationRuleCollection authRules = dirSec.GetAccessRules(true, true, typeof(NTAccount));
            return authRules;
        }

    }
}
