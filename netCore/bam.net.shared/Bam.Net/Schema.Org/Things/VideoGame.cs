using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A video game is an electronic game that involves human interaction with a user interface to generate visual feedback on a video device.</summary>
	public class VideoGame: Game
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>Cheat codes to the game.</summary>
		public CreativeWork CheatCode {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The electronic systems used to play video games.</summary>
		public OneOfThese<Text,Thing,Url> GamePlatform {get; set;}
		///<summary>The server on which  it is possible to play the game. Inverse property: game.</summary>
		public GameServer GameServer {get; set;}
		///<summary>Links to tips, tactics, etc.</summary>
		public CreativeWork GameTip {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public OneOfThese<MusicGroup,Person> MusicBy {get; set;}
		///<summary>Indicates whether this game is multi-player, co-op or single-player.  The game can be marked as multi-player, co-op and single-player at the same time.</summary>
		public GamePlayMode PlayMode {get; set;}
		///<summary>The trailer of a movie or tv/radio series, season, episode, etc.</summary>
		public VideoObject Trailer {get; set;}
	}
}
