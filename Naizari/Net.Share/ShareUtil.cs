/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Naizari.Net.Share
{
    public class ShareUtil
    {
        [DllImport("netapi32.dll", SetLastError=true)]
        private static extern uint NetShareDel(
            [MarshalAs(UnmanagedType.LPWStr)] string strServer,
            [MarshalAs(UnmanagedType.LPWStr)] string strNetName,
            Int32 reserved //must be 0
            );

        [DllImport("Netapi32.dll")]
        private static extern uint NetShareAdd(
            [MarshalAs(UnmanagedType.LPWStr)] string strServer,
            Int32 dwLevel,
            ref SHARE_INFO_502 buf,
            out uint parm_err
        );

        [DllImport("Netapi32.dll", CharSet = CharSet.Auto)]
        private static extern int NetApiBufferFree(IntPtr Buffer);

        [DllImport("Netapi32.dll", CharSet = CharSet.Auto)]
        private static extern int NetShareGetInfo(
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            [MarshalAs(UnmanagedType.LPWStr)] string netname, 
            int level, 
            ref IntPtr bufptr
        );

        private enum NetError : uint
        {
            NERR_Success = 0,
            NERR_BASE = 2100,
            NERR_UnknownDevDir = (NERR_BASE + 16),
            NERR_DuplicateShare = (NERR_BASE + 18),
            NERR_BufTooSmall = (NERR_BASE + 23),
        }

        private enum SHARE_TYPE : uint
        {
            STYPE_DISKTREE = 0,
            STYPE_PRINTQ = 1,
            STYPE_DEVICE = 2,
            STYPE_IPC = 3,
            STYPE_SPECIAL = 0x80000000,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHARE_INFO_2
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi2_netname;
            public uint shi2_type;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi2_remark;
            public uint shi2_permissions;
            public uint shi2_max_uses;
            public uint shi2_current_uses;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi2_path;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi2_passwd;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SHARE_INFO_502
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_netname;
            public SHARE_TYPE shi502_type;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_remark;
            public Int32 shi502_permissions;
            public Int32 shi502_max_uses;
            public Int32 shi502_current_uses;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_path;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_passwd;
            public Int32 shi502_reserved;
            public IntPtr shi502_security_descriptor;
        }

        public static string GetShareLocalPath(string server, string shareName)
        {
            string path = string.Empty;
            IntPtr ptr = IntPtr.Zero;
            int result = NetShareGetInfo(server, shareName, 2, ref ptr);
            if (result == (int)GetShareInfoResultType.NerrSuccess)
            {
                SHARE_INFO_2 shareInfo = (SHARE_INFO_2)
                        Marshal.PtrToStructure(ptr, typeof(SHARE_INFO_2));

                path = shareInfo.shi2_path;
                NetApiBufferFree(ptr);
            }
            return path;
        }

        public static CreateShareResultType CreateShare(string server, string localPathOnServer, string shareName, string shareDescription)
        {
            SHARE_INFO_502 info = new SHARE_INFO_502();
            info.shi502_netname = shareName;
            info.shi502_type = SHARE_TYPE.STYPE_DISKTREE;
            info.shi502_remark = shareDescription;
            info.shi502_permissions = 0;
            info.shi502_max_uses = -1;
            info.shi502_current_uses = 0;
            info.shi502_path = localPathOnServer;
            info.shi502_passwd = null;
            info.shi502_reserved = 0;
            info.shi502_security_descriptor = IntPtr.Zero;
            uint error = 0;
            uint result = NetShareAdd(server, 502, ref info, out error);
            return (CreateShareResultType)result;            
        }

        public static bool IsShareRoot(string uncPath)
        {
            if (uncPath.StartsWith("\\\\"))
            {
                string[] splitPath = uncPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                return splitPath.Length == 2;
            }
            return false;
        }

        public static string GetAdminSharePathForUNCRoot(string uncPath)
        {
            if (IsShareRoot(uncPath))
            {
                string[] splitPath = uncPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                string server = splitPath[0];
                string share = splitPath[1];
                string localPath = GetShareLocalPath(server, share);
                return "\\\\" + server + "\\" + localPath.Replace(":", "$");
            }
            return string.Empty;
        }

        /// <summary>
        /// Deletes the specified share on the speicifed server.
        /// </summary>
        /// <param name="server">The name of the server</param>
        /// <param name="shareName">The name of the share</param>
        /// <returns>0 on success another value on failure</returns>
        public static uint DeleteShare(string server, string shareName)
        {
            return NetShareDel(server, shareName, 0);
        }
    }
}
