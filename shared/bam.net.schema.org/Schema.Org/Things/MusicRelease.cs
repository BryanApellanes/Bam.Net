using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A MusicRelease is a specific release of a music album.</summary>
	public class MusicRelease: MusicPlaylist
	{
		///<summary>The catalog number for the release.</summary>
		public Text CatalogNumber {get; set;}
		///<summary>The group the release is credited to if different than the byArtist. For example, Red and Blue is credited to "Stefani Germanotta Band", but by Lady Gaga.</summary>
		public OneOfThese<Organization,Person> CreditedTo {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>Format of this release (the type of recording media used, ie. compact disc, digital media, LP, etc.).</summary>
		public MusicReleaseFormatType MusicReleaseFormat {get; set;}
		///<summary>The label that issued the release.</summary>
		public Organization RecordLabel {get; set;}
		///<summary>The album this is a release of. Inverse property: albumRelease.</summary>
		public MusicAlbum ReleaseOf {get; set;}
	}
}
