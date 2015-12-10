/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public abstract class DaoSearchToken
    {
        public WhereAppender WhereAppender { get; set; }
    }
}
