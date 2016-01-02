/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A delivery service through which content is provided via broadcast over the air or online.</summary>
	public class BroadcastService: Thing
	{
		///<summary>The area within which users can expect to reach the broadcast service.</summary>
		public Place Area {get; set;}
		///<summary>The organization owning or operating the broadcast service.</summary>
		public Organization Broadcaster {get; set;}
		///<summary>A broadcast service to which the broadcast service may belong to such as regional variations of a national channel.</summary>
		public BroadcastService ParentService {get; set;}
	}
}
