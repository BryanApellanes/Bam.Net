using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A music recording (track), usually a single song.</summary>
	public class MusicRecording: CreativeWork
	{
		///<summary>The artist that performed this album or recording.</summary>
		public MusicGroup ByArtist {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The album to which this recording belongs.</summary>
		public MusicAlbum InAlbum {get; set;}
		///<summary>The playlist to which this recording belongs.</summary>
		public MusicPlaylist InPlaylist {get; set;}
		///<summary>The International Standard Recording Code for the recording.</summary>
		public Text IsrcCode {get; set;}
		///<summary>The composition this track is a recording of. Inverse property: recordedAs.</summary>
		public MusicComposition RecordingOf {get; set;}
	}
}
