using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A short TV or radio program or a segment/part of a program.</summary>
	public class Clip: CreativeWork
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>Position of the clip within an ordered group of clips.</summary>
		public OneOfThese<Integer,Text> ClipNumber {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public OneOfThese<MusicGroup,Person> MusicBy {get; set;}
		///<summary>The episode to which this clip belongs.</summary>
		public Episode PartOfEpisode {get; set;}
		///<summary>The season to which this episode belongs.</summary>
		public CreativeWorkSeason PartOfSeason {get; set;}
		///<summary>The series to which this episode or season belongs. Supersedes partOfTVSeries.</summary>
		public CreativeWorkSeries PartOfSeries {get; set;}
	}
}
