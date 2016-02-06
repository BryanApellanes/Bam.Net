using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A media season e.g. tv, radio, video game etc.</summary>
	public class CreativeWorkSeason: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video games etc. content. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The end date and time of the item (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>An episode of a tv, radio or game media within a series or season. Supersedes episodes.</summary>
		public Episode Episode {get; set;}
		///<summary>The number of episodes in this season or series.</summary>
		public Integer NumberOfEpisodes {get; set;}
		///<summary>The series to which this episode or season belongs. Supersedes partOfTVSeries.</summary>
		public CreativeWorkSeries PartOfSeries {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>Position of the season within an ordered group of seasons.</summary>
		public OneOfThese<Text , Integer> SeasonNumber {get; set;}
		///<summary>The start date and time of the item (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
