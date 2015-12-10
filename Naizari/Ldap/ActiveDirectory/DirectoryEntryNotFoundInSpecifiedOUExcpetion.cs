/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Ldap.ActiveDirectory
{
    public class DirectoryEntryNotFoundInSpecifiedOUExcpetion: Exception
    {
        public DirectoryEntryNotFoundInSpecifiedOUExcpetion(string groupName, string OU)
            : base(string.Format("Group named \"{0}\" could not be found in the OU [{1}]", groupName, OU))
        {
        }
    }
}
