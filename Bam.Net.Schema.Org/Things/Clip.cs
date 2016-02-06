/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A short TV or radio program or a segment/part of a program.</summary>
	public class Clip: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>Position of the clip within an ordered group of clips.</summary>
		public ThisOrThat<Integer , Text> ClipNumber {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video games etc. content. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public ThisOrThat<Person , MusicGroup> MusicBy {get; set;}
		///<summary>The episode to which this clip belongs.</summary>
		public Episode PartOfEpisode {get; set;}
		///<summary>The season to which this episode belongs.</summary>
		public Season PartOfSeason {get; set;}
		///<summary>The series to which this episode or season belongs. Supersedes partOfTVSeries.</summary>
		public Series PartOfSeries {get; set;}
		///<summary>A publication event associated with the episode, clip or media object.</summary>
		public PublicationEvent Publication {get; set;}
	}
}
