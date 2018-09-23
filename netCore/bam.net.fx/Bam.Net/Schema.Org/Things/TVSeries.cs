using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>CreativeWorkSeries dedicated to TV broadcast and associated online delivery.</summary>
	public class TVSeries: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>A season that is part of the media series. Supersedes season.</summary>
		public CreativeWorkSeason ContainsSeason {get; set;}
		///<summary>The country of the principal offices of the production company or individual responsible for the movie or program.</summary>
		public Country CountryOfOrigin {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>An episode of a tv, radio or game media within a series or season. Supersedes episodes.</summary>
		public Episode Episode {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public OneOfThese<MusicGroup,Person> MusicBy {get; set;}
		///<summary>The number of episodes in this season or series.</summary>
		public Integer NumberOfEpisodes {get; set;}
		///<summary>The number of seasons in this series.</summary>
		public Integer NumberOfSeasons {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
