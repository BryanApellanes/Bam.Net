/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// When implemented, determines who the current user is.
    /// </summary>
    /// <seealso cref="Bam.Net.ServiceProxy.IRequiresHttpContext" />
    public interface IUserResolver: IRequiresHttpContext
    {
        string GetCurrentUser();

        string GetUser(IHttpContext context);
    }
}
