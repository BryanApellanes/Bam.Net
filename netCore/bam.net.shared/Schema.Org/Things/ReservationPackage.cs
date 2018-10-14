using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A group of multiple reservations with common values for all sub-reservations.</summary>
	public class ReservationPackage: Reservation
	{
		///<summary>The individual reservations included in the package. Typically a repeated property.</summary>
		public Reservation SubReservation {get; set;}
	}
}
