using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A delivery service through which content is provided via broadcast over the air or online.</summary>
	public class BroadcastService: Service
	{
		///<summary>The media network(s) whose content is broadcast on this station.</summary>
		public Organization BroadcastAffiliateOf {get; set;}
		///<summary>The name displayed in the channel guide. For many US affiliates, it is the network name.</summary>
		public Text BroadcastDisplayName {get; set;}
		///<summary>The timezone in ISO 8601 format for which the service bases its broadcasts.</summary>
		public Text BroadcastTimezone {get; set;}
		///<summary>The organization owning or operating the broadcast service.</summary>
		public Organization Broadcaster {get; set;}
		///<summary>A broadcast service to which the broadcast service may belong to such as regional variations of a national channel.</summary>
		public BroadcastService ParentService {get; set;}
		///<summary>The type of screening or video broadcast used (e.g. IMAX, 3D, SD, HD, etc.).</summary>
		public Text VideoFormat {get; set;}
	}
}
