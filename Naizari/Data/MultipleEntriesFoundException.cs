/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Extensions;

namespace Naizari.Data
{
    public class MultipleEntriesFoundException: Exception
    {
        public MultipleEntriesFoundException()
            : base("Multiple entries found for DbSelectParameters specified")
        {
        }

        public MultipleEntriesFoundException(Exception inner)
            : base("Multiple entries found for DbSelectParameters specified", inner)
        {
        }
    }
}
