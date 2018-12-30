using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of participating in performance arts.</summary>
	public class PerformAction: PlayAction
	{
		///<summary>A sub property of location. The entertainment business where the action occurred.</summary>
		public EntertainmentBusiness EntertainmentBusiness {get; set;}
	}
}
