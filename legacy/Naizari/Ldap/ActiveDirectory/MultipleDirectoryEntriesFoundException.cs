/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Ldap.ActiveDirectory
{
    public class MultipleDirectoryEntriesFoundException: Exception
    {
        public MultipleDirectoryEntriesFoundException()
            : this("")
        { }

        public MultipleDirectoryEntriesFoundException(string filter)
            : base("Multiple Directory Entries were found for the specified filter: " + filter + "")
        { }
    }
}
