using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A video game series.</summary>
	public class VideoGameSeries: CreativeWorkSeries
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>A piece of data that represents a particular aspect of a fictional character (skill, power, character points, advantage, disadvantage).</summary>
		public Thing CharacterAttribute {get; set;}
		///<summary>Cheat codes to the game.</summary>
		public CreativeWork CheatCode {get; set;}
		///<summary>A season that is part of the media series. Supersedes season.</summary>
		public CreativeWorkSeason ContainsSeason {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>An episode of a tv, radio or game media within a series or season. Supersedes episodes.</summary>
		public Episode Episode {get; set;}
		///<summary>An item is an object within the game world that can be collected by a player or, occasionally, a non-player character.</summary>
		public Thing GameItem {get; set;}
		///<summary>Real or fictional location of the game (or part of game).</summary>
		public OneOfThese<Place,PostalAddress,Url> GameLocation {get; set;}
		///<summary>The electronic systems used to play video games.</summary>
		public OneOfThese<Text,Thing,Url> GamePlatform {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public OneOfThese<MusicGroup,Person> MusicBy {get; set;}
		///<summary>The number of episodes in this season or series.</summary>
		public Integer NumberOfEpisodes {get; set;}
		///<summary>Indicate how many people can play this game (minimum, maximum, or range).</summary>
		public QuantitativeValue NumberOfPlayers {get; set;}
		///<summary>The number of seasons in this series.</summary>
		public Integer NumberOfSeasons {get; set;}
		///<summary>Indicates whether this game is multi-player, co-op or single-player.  The game can be marked as multi-player, co-op and single-player at the same time.</summary>
		public GamePlayMode PlayMode {get; set;}
		///<summary>The production company or studio responsible for the item e.g. series, video game, episode etc.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>The task that a player-controlled character, or group of characters may complete in order to gain a reward.</summary>
		public Thing Quest {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
