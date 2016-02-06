using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent approves/certifies/likes/supports/sanction an object.</summary>
	public class EndorseAction: ReactAction
	{
		///<summary>A sub property of participant. The person/organization being supported.</summary>
		public OneOfThese<Person , Organization> Endorsee {get; set;}
	}
}
