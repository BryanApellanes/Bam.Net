/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of expressing a preference from a set of options or a large or unbounded set of choices/options.</summary>
	public class ChooseAction: AssessAction
	{
		///<summary>A sub property of object. The options subject to this action.</summary>
		public OneOfThese<Text , Thing> Option {get; set;}
	}
}
