/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public enum CreateTableResult
    {
        Unknown,
        Success,
        AlreadyExists,
        UnknownError
    }
}
