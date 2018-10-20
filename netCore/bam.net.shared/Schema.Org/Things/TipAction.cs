using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of giving money voluntarily to a beneficiary in recognition of services rendered.</summary>
	public class TipAction: TradeAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Audience,Organization,Person> Recipient {get; set;}
	}
}
