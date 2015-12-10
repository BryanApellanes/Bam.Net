/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Naizari.Net.Dfs
{
    public class DfsUtil
    {
        [DllImport("Netapi32.dll", CharSet = CharSet.Auto, SetLastError = true, EntryPoint = "NetDfsRemove")]
        static extern int NetDfsRemoveExtern(
            [MarshalAs(UnmanagedType.LPWStr)]
            string DfsEntryPath,
            [MarshalAs(UnmanagedType.LPWStr)]
            string ServerName,
            [MarshalAs(UnmanagedType.LPWStr)]
            string ShareName
            );

        [DllImport("Netapi32.dll", CharSet = CharSet.Auto, SetLastError = true, EntryPoint = "NetDfsAdd")]
        static extern int NetDfsAddExtern(
            [MarshalAs(UnmanagedType.LPWStr)]
            string DfsEntryPath,
            [MarshalAs(UnmanagedType.LPWStr)]
            string ServerName,
            [MarshalAs(UnmanagedType.LPWStr)]
            string PathName,
            [MarshalAs(UnmanagedType.LPWStr)]
            string Comment,
            int Flags);


        public static int NetDfsRemove(string DfsEntryPath)
        {
            return NetDfsRemove(DfsEntryPath, string.Empty, string.Empty);
        }

        public static int NetDfsRemove(string DfsEntryPath, string targetServerName, string targetSharePath)
        {
            return NetDfsRemoveExtern(DfsEntryPath, targetServerName, targetSharePath);
        }

        /// <summary>
        /// Creates a new Distributed File System (DFS) link or adds targets to an existing link in a DFS namespace.
        /// see http://msdn.microsoft.com/en-us/library/bb524805(VS.85).aspx
        /// </summary>
        /// <param name="DfsEntryPath">
        ///Pointer to a string that specifies the Universal Naming Convention (UNC) path of a DFS link in a DFS namespace.
        ///The string can be in one of two forms. The first form is as follows:
        ///
        ///\\ServerName\DfsName\link_path
        ///
        ///where ServerName is the name of the root target server that hosts a stand-alone DFS namespace; DfsName is the name of the DFS namespace; and link_path is a DFS link.
        ///The second form is as follows:
        ///\\DomainName\DomDfsname\link_path
        ///
        ///where DomainName is the name of the domain that hosts a domain-based DFS namespace; DomDfsname is the name of the domain-based DFS namespace; and link_path is a DFS link.
        ///This parameter is required.
        ///</param>
        /// <param name="ServerName">Pointer to a string that specifies the link target server name. This parameter is required.</param>
        /// <param name="PathName">Pointer to a string that specifies the link target share name. This can also be a share name with a path relative to the share. 
        /// For example, share1\mydir1\mydir2. This parameter is required.</param>
        /// <param name="Comment">Pointer to a string that specifies an optional comment associated with the DFS link. 
        /// This parameter is ignored when the function adds a target to an existing link.</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        /// <remarks>
        /// The DFS namespace must already exist. This function does not create a new DFS namespace.
        ///
        ///The caller must have Administrator privilege on the DFS server. For more information about calling functions that require administrator privileges, 
        ///see Running with Special Privileges.
        ///
        ///Use of the DFS_ADD_VOLUME flag is optional. If you specify DFS_ADD_VOLUME and the link already exists, NetDfsAdd fails.
        ///If you do not specify DFS_ADD_VOLUME, NetDfsAdd creates the link, if required, and adds the target to the link. You should specify this value when you need to determine when new links are created.
        /// </remarks>
        public static int NetDfsAdd(
            string DfsEntryPath,
            string ServerName,
            string PathName,
            string Comment)
        {
            return NetDfsAddExtern(DfsEntryPath, ServerName, PathName, Comment, (int)NetDfsAddFlags.Add);
        }

        public static int NetDfsAdd(string DfsEntryPath, string targetUNC)
        {
            return NetDfsAdd(DfsEntryPath, targetUNC, "");
        }

        public static int NetDfsAdd(string DfsEntryPath, string targetUNC, string comment)
        {
            string[] splitted;
            string serverName;
            string pathName;
            GetServerNameAndPath(targetUNC, out splitted, out serverName, out pathName);

            if (splitted.Length < 1)
                throw new ArgumentException(string.Format("targetUNC was not in a recognized format ({0})", targetUNC), targetUNC);

            return NetDfsAddExtern(DfsEntryPath, serverName, pathName, comment, (int)NetDfsAddFlags.Add);
        }

        public static int NetDfsRemove(string DfsEntryPath, string targetUNC)
        {
            string[] splitted;
            string serverName;
            string pathName;
            GetServerNameAndPath(targetUNC, out splitted, out serverName, out pathName);

            if(splitted.Length < 1)
                throw new ArgumentException(string.Format("targetUNC was not in a recognized format ({0})", targetUNC), targetUNC);

            return NetDfsRemove(DfsEntryPath, serverName, pathName);
        }

        private static void GetServerNameAndPath(string targetUNC, out string[] splitted, out string serverName, out string pathName)
        {
            splitted = targetUNC.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            serverName = splitted[0];
            pathName = string.Empty;
            for (int i = 1; i < splitted.Length; i++)
            {
                pathName += splitted[i];
                if (i != splitted.Length - 1)
                    pathName += "\\";
            }
        }
    }
}
