/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    /// <summary>
    /// This class exists for consisteny in the 
    /// initialization calls intended to be called
    /// from the Global.asax file and App_Start
    /// classes.
    /// </summary>
    public static class DaoProxySystem
    {
        /// <summary>
        /// Registers the mvc route for query interface (qi.js; pronounced "chi") 
        /// calls.  This route should be registered after the default route
        /// </summary>
        public static void Initialize()
        {
            DaoProxyRegistration.Initialize();
        }
    }
}
