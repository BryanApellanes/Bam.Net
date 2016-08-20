using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A media episode (e.g. TV, radio, video game) which can be part of a series or season.</summary>
	public class Episode: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>Position of the episode within an ordered group of episodes.</summary>
		public OneOfThese<Integer,Text> EpisodeNumber {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public OneOfThese<MusicGroup,Person> MusicBy {get; set;}
		///<summary>The season to which this episode belongs.</summary>
		public CreativeWorkSeason PartOfSeason {get; set;}
		///<summary>The series to which this episode or season belongs. Supersedes partOfTVSeries.</summary>
		public CreativeWorkSeries PartOfSeries {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
