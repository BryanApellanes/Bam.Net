using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Season dedicated to TV broadcast and associated online delivery.</summary>
	public class TVSeason: CreativeWork
	{
		///<summary>The country of the principal offices of the production company or individual responsible for the movie or program.</summary>
		public Country CountryOfOrigin {get; set;}
	}
}
