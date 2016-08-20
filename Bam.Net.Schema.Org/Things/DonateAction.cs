using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of providing goods, services, or money without compensation, often for philanthropic reasons.</summary>
	public class DonateAction: TradeAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Audience,Organization,Person> Recipient {get; set;}
	}
}
