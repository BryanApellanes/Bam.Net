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
    /// <summary>
    /// A base razor template whose model is of type 
    /// TModel
    /// </summary>
    /// <typeparam name="TModel">The type of the model</typeparam>
	public abstract class RazorTemplate<TModel>: RazorBaseTemplate
	{
		public RazorTemplate()
		{
			Generated = new StringBuilder();
		}
		
		public TModel Model { get; set; }
	}
}
