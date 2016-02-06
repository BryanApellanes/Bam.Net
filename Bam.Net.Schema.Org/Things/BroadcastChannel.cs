using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A unique instance of a BroadcastService on a CableOrSatelliteService lineup.</summary>
	public class BroadcastChannel: Intangible
	{
		///<summary>The unique address by which the BroadcastService can be identified in a provider lineup. In US, this is typically a number.</summary>
		public Text BroadcastChannelId {get; set;}
		///<summary>The type of service required to have access to the channel (e.g. Standard or Premium).</summary>
		public Text BroadcastServiceTier {get; set;}
		///<summary>The CableOrSatelliteService offering the channel.</summary>
		public CableOrSatelliteService InBroadcastLineup {get; set;}
		///<summary>The BroadcastService offered on this channel.</summary>
		public BroadcastService ProvidesBroadcastService {get; set;}
	}
}
