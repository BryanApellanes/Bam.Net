using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A collection of music tracks.</summary>
	public class MusicAlbum: MusicPlaylist
	{
		///<summary>Classification of the album by it's type of content: soundtrack, live album, studio album, etc.</summary>
		public MusicAlbumProductionType AlbumProductionType {get; set;}
		///<summary>A release of this album. Inverse property: releaseOf.</summary>
		public MusicRelease AlbumRelease {get; set;}
		///<summary>The kind of release which this album is: single, EP or album.</summary>
		public MusicAlbumReleaseType AlbumReleaseType {get; set;}
		///<summary>The artist that performed this album or recording.</summary>
		public MusicGroup ByArtist {get; set;}
	}
}
