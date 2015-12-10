/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A media episode (e.g. TV, radio, video game) which can be part of a series or season.</summary>
	public class Episode: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video games etc. content. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>Position of the episode within an ordered group of episodes.</summary>
		public ThisOrThat<Integer , Text> EpisodeNumber {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public ThisOrThat<Person , MusicGroup> MusicBy {get; set;}
		///<summary>The season to which this episode belongs.</summary>
		public Season PartOfSeason {get; set;}
		///<summary>The series to which this episode or season belongs. Supersedes partOfTVSeries.</summary>
		public Series PartOfSeries {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>A publication event associated with the episode, clip or media object.</summary>
		public PublicationEvent Publication {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
