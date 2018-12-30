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
	public interface IHasConnectionStringResolver
	{
		IConnectionStringResolver ConnectionStringResolver { get; set; }
	}
}
