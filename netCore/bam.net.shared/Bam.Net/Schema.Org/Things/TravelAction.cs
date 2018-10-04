using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of traveling from an fromLocation to a destination by a specified mode of transport, optionally with participants.</summary>
	public class TravelAction: MoveAction
	{
		///<summary>The distance travelled, e.g. exercising or travelling.</summary>
		public Distance Distance {get; set;}
	}
}
