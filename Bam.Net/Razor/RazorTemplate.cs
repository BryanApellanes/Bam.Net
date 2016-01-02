/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Razor
{
	public abstract class RazorTemplate<TModel>: RazorBaseTemplate
	{
		public RazorTemplate()
		{
			Generated = new StringBuilder();
		}
		
		public TModel Model { get; set; }
	}
}
