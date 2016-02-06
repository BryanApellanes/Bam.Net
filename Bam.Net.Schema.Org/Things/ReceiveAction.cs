/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of physically/electronically taking delivery of an object thathas been transferred from an origin to a destination. Reciprocal of SendAction.Related actions:SendAction: The reciprocal of ReceiveAction.TakeAction: Unlike TakeAction, ReceiveAction does not imply that the ownership has been transfered (e.g. I can receive a package, but it does not mean the package is now mine).</summary>
	public class ReceiveAction: TransferAction
	{
		///<summary>A sub property of instrument. The method of delivery.</summary>
		public DeliveryMethod DeliveryMethod {get; set;}
		///<summary>A sub property of participant. The participant who is at the sending end of the action.</summary>
		public OneOfThese<Person , Organization , Audience> Sender {get; set;}
	}
}
