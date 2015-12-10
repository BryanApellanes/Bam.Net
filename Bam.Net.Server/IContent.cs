/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.Server.Management
{
	public interface IContent
	{
		IHtmlString Render();
	}
}
