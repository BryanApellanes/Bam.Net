using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A screening of a movie or other video.</summary>
	public class ScreeningEvent: Event
	{
		///<summary>Languages in which subtitles/captions are available, in IETF BCP 47 standard format.</summary>
		public OneOfThese<Language,Text> SubtitleLanguage {get; set;}
		///<summary>The type of screening or video broadcast used (e.g. IMAX, 3D, SD, HD, etc.).</summary>
		public Text VideoFormat {get; set;}
		///<summary>The movie presented during this event.</summary>
		public Movie WorkPresented {get; set;}
	}
}
