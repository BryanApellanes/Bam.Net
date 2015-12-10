/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of providing an object under an agreement that it will be returned at a later date. Reciprocal of BorrowAction.Related actions:BorrowAction: Reciprocal of LendAction.</summary>
	public class LendAction: TransferAction
	{
		///<summary>A sub property of participant. The person that borrows the object being lent.</summary>
		public Person Borrower {get; set;}
	}
}
