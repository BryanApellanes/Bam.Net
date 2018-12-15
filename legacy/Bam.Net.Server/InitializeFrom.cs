/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public enum InitializeFrom
    {
        Invalid,
        /// <summary>
        /// Initialize from the embedded resource file 
        /// that comes with the Bam system
        /// </summary>
        Resource,
        /// <summary>
        /// Initialize from a zip file
        /// specified path
        /// </summary>
        ZipPath
    }
}
