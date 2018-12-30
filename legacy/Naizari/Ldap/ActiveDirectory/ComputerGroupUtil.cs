/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using Naizari.Net;
using System.DirectoryServices;
using ActiveDs;

namespace Naizari.Ldap.ActiveDirectory
{
    public class ComputerGroupUtil
    {
        public static bool UserIsInAdminGroup(string userName)
        {
            return UserIsInAdminGroup(userName, ".");
        }

        public static bool UserIsInAdminGroup(string userName, string computerName)
        {
            DirectoryEntry entry = new DirectoryEntry("WinNT://" + computerName + "/Administrators,group");
            //IADsGroup admins = (IADsGroup)entry.NativeObject;
            //DirectoryEntry entry = new DirectoryEntry("WinNT://sea-apebry,Computer");
            DirectoryEntry adm = entry.Children.Find("Administrators", "Group");
            ////IADsGroup group = (IADsGroup)entry.NativeObject;
            object members = adm.Invoke("Members", null);
            foreach (object member in (IEnumerable)members)
            {
                DirectoryEntry user = new DirectoryEntry(member);
                foreach (string prop in user.Properties.PropertyNames)
                {
                    Console.WriteLine(prop + ": " + user.Properties[prop].Value.ToString());
                }
                Console.WriteLine();
            }
            throw new NotImplementedException("ComputerGroupUtil is not done and has no usable methods");
            return true;
        }
    }
}
