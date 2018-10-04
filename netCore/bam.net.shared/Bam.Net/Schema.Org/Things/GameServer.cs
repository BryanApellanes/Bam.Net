using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Server that provides game interaction in a multiplayer game.</summary>
	public class GameServer: Intangible
	{
		///<summary>Video game which is played on this server. Inverse property: gameServer.</summary>
		public VideoGame Game {get; set;}
		///<summary>Number of players on the server.</summary>
		public Integer PlayersOnline {get; set;}
		///<summary>Status of a game server.</summary>
		public GameServerStatus ServerStatus {get; set;}
	}
}
