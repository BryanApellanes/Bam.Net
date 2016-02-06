/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A media season e.g. tv, radio, video game etc.</summary>
	public class Season: CreativeWork
	{
		///<summary>The end date and time of the item (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>An episode of a tv, radio or game media within a series or season. Supersedes episodes.</summary>
		public Episode Episode {get; set;}
		///<summary>The number of episodes in this season or series.</summary>
		public Number NumberOfEpisodes {get; set;}
		///<summary>The series to which this episode or season belongs. Supersedes partOfTVSeries.</summary>
		public Series PartOfSeries {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>Position of the season within an ordered group of seasons.</summary>
		public OneOfThese<Integer , Text> SeasonNumber {get; set;}
		///<summary>The start date and time of the item (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
