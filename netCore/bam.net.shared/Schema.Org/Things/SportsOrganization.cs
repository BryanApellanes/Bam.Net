using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Represents the collection of all sports organizations, including sports teams, governing bodies, and sports associations.</summary>
	public class SportsOrganization: Organization
	{
		///<summary>A type of sport (e.g. Baseball).</summary>
		public OneOfThese<Text,Url> Sport {get; set;}
	}
}
