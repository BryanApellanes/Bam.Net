/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A collection of music tracks in playlist form.</summary>
	public class MusicPlaylist: CreativeWork
	{
		///<summary>The number of tracks in this album or playlist.</summary>
		public Integer NumTracks {get; set;}
		///<summary>A music recording (track)—usually a single song. If an ItemList is given, the list should contain items of type MusicRecording. Supersedes tracks.</summary>
		public ThisOrThat<ItemList , MusicRecording> Track {get; set;}
	}
}
