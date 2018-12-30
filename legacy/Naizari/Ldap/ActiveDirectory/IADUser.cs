/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Naizari.Ldap.ActiveDirectory
{
    interface IADUser
    {
        string FirstName { get;  }
        string[] Groups { get; }
        //void initGroups();
        bool IsInGroup(string groupName);
        string LastName { get; }
        string UserId { get; }
    }
}
