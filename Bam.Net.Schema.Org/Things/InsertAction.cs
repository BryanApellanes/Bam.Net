/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of adding at a specific location in an ordered collection.</summary>
	public class InsertAction: AddAction
	{
		///<summary>A sub property of location. The final location of the object or the agent after the action.</summary>
		public Place ToLocation {get; set;}
	}
}
