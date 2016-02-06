/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A movie.</summary>
	public class Movie: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video games etc. content. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public ThisOrThat<Person , MusicGroup> MusicBy {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
