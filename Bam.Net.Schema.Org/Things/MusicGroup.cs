using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A musical group, such as a band, an orchestra, or a choir. Can also be a solo musician.</summary>
	public class MusicGroup: PerformingGroup
	{
		///<summary>A music album. Supersedes albums.</summary>
		public MusicAlbum Album {get; set;}
		///<summary>Genre of the creative work or group.</summary>
		public OneOfThese<Text , URL> Genre {get; set;}
		///<summary>A music recording (track)—usually a single song. If an ItemList is given, the list should contain items of type MusicRecording. Supersedes tracks.</summary>
		public OneOfThese<ItemList , MusicRecording> Track {get; set;}
	}
}
