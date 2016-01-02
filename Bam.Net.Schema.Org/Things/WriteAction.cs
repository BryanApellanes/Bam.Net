/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of authoring written creative content.</summary>
	public class WriteAction: CreateAction
	{
		///<summary>A sub property of instrument. The language used on this action.</summary>
		public Language Language {get; set;}
	}
}
