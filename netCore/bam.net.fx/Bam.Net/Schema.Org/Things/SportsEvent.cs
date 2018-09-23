using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Event type: Sports event.</summary>
	public class SportsEvent: Event
	{
		///<summary>The away team in a sports event.</summary>
		public OneOfThese<Person,SportsTeam> AwayTeam {get; set;}
		///<summary>A competitor in a sports event.</summary>
		public OneOfThese<Person,SportsTeam> Competitor {get; set;}
		///<summary>The home team in a sports event.</summary>
		public OneOfThese<Person,SportsTeam> HomeTeam {get; set;}
	}
}
