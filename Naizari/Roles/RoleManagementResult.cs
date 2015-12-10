/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Roles
{
    public class RoleManagementResult
    {
        public RoleModifyResult ModifyResult { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
