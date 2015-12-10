/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of organizing tasks/objects/events by associating resources to it.</summary>
	public class AllocateAction: OrganizeAction
	{
		///<summary>A goal towards an action is taken. Can be concrete or abstract.</summary>
		public ThisOrThat<MedicalDevicePurpose , Thing> Purpose {get; set;}
	}
}
