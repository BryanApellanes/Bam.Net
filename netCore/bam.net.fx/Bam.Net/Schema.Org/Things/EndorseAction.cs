using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An agent approves/certifies/likes/supports/sanction an object.</summary>
	public class EndorseAction: ReactAction
	{
		///<summary>A sub property of participant. The person/organization being supported.</summary>
		public OneOfThese<Organization,Person> Endorsee {get; set;}
	}
}
