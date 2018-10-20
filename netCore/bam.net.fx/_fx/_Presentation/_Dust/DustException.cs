/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Presentation.Dust
{
    [Serializable]
    public class DustException: Exception
    {
        public DustException(string message)
            : base(message)
        {
        }

        public DustException(string message, Exception innerException)
            : base(message, innerException)
        {
        }   
    }
}
