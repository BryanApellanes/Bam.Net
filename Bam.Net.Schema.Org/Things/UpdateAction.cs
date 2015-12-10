/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of managing by changing/editing the state of the object.</summary>
	public class UpdateAction: Action
	{
		///<summary>A sub property of object. The collection target of the action.</summary>
		public Thing Collection {get; set;}
	}
}
