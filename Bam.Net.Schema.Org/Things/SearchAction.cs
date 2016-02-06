/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of searching for an object.Related actions:FindAction: SearchAction generally leads to a FindAction, but not necessarily.</summary>
	public class SearchAction: Action
	{
		///<summary>A sub property of instrument. The query used on this action.</summary>
		public ThisOrThat<Class , Text> Query {get; set;}
	}
}
