using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of obtaining an object under an agreement to return it at a later date. Reciprocal of LendAction.Related actions:LendAction: Reciprocal of BorrowAction.</summary>
	public class BorrowAction: TransferAction
	{
		///<summary>A sub property of participant. The person that lends the object being borrowed.</summary>
		public Person Lender {get; set;}
	}
}
