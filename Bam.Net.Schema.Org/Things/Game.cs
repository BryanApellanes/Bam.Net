using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The Game type represents things which are games. These are typically rule-governed recreational activities, e.g. role-playing games in which players assume the role of characters in a fictional setting.</summary>
	public class Game: CreativeWork
	{
		///<summary>A piece of data that represents a particular aspect of a fictional character (skill, power, character points, advantage, disadvantage).</summary>
		public Thing CharacterAttribute {get; set;}
		///<summary>An item is an object within the game world that can be collected by a player or, occasionally, a non-player character.</summary>
		public Thing GameItem {get; set;}
		///<summary>Real or fictional location of the game (or part of game).</summary>
		public OneOfThese<Place,PostalAddress,Url> GameLocation {get; set;}
		///<summary>Indicate how many people can play this game (minimum, maximum, or range).</summary>
		public QuantitativeValue NumberOfPlayers {get; set;}
		///<summary>The task that a player-controlled character, or group of characters may complete in order to gain a reward.</summary>
		public Thing Quest {get; set;}
	}
}
