using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A PerformanceRole is a Role that some entity places with regard to a theatrical performance, e.g. in a Movie, TVSeries etc.</summary>
	public class PerformanceRole: Role
	{
		///<summary>The name of a character played in some acting or performing role, i.e. in a PerformanceRole.</summary>
		public Text CharacterName {get; set;}
	}
}
