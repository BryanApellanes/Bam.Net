using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A TV episode which can be part of a series or season.</summary>
	public class TVEpisode: Episode
	{
		///<summary>The country of the principal offices of the production company or individual responsible for the movie or program.</summary>
		public Country CountryOfOrigin {get; set;}
		///<summary>Languages in which subtitles/captions are available, in IETF BCP 47 standard format.</summary>
		public OneOfThese<Language,Text> SubtitleLanguage {get; set;}
	}
}
