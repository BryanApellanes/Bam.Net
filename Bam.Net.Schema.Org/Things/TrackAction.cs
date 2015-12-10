/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent tracks an object for updates.Related actions:FollowAction: Unlike FollowAction, TrackAction refers to the interest on the location of innanimates objects.SubscribeAction: Unlike SubscribeAction, TrackAction refers to  the interest on the location of innanimate objects.</summary>
	public class TrackAction: FindAction
	{
		///<summary>A sub property of instrument. The method of delivery.</summary>
		public DeliveryMethod DeliveryMethod {get; set;}
	}
}
