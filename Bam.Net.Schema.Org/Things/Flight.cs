using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An airline flight.</summary>
	public class Flight: Intangible
	{
		///<summary>The kind of aircraft (e.g., "Boeing 747").</summary>
		public ThisOrThat<Text , Vehicle> Aircraft {get; set;}
		///<summary>The airport where the flight terminates.</summary>
		public Airport ArrivalAirport {get; set;}
		///<summary>Identifier of the flight's arrival gate.</summary>
		public Text ArrivalGate {get; set;}
		///<summary>Identifier of the flight's arrival terminal.</summary>
		public Text ArrivalTerminal {get; set;}
		///<summary>The expected arrival time.</summary>
		public DateTime ArrivalTime {get; set;}
		///<summary>The type of boarding policy used by the airline (e.g. zone-based or group-based).</summary>
		public BoardingPolicyType BoardingPolicy {get; set;}
		///<summary>The airport where the flight originates.</summary>
		public Airport DepartureAirport {get; set;}
		///<summary>Identifier of the flight's departure gate.</summary>
		public Text DepartureGate {get; set;}
		///<summary>Identifier of the flight's departure terminal.</summary>
		public Text DepartureTerminal {get; set;}
		///<summary>The expected departure time.</summary>
		public DateTime DepartureTime {get; set;}
		///<summary>The estimated time the flight will take.</summary>
		public ThisOrThat<Duration , Text> EstimatedFlightDuration {get; set;}
		///<summary>The distance of the flight.</summary>
		public ThisOrThat<Distance , Text> FlightDistance {get; set;}
		///<summary>The unique identifier for a flight including the airline IATA code. For example, if describing United flight 110, where the IATA code for United is 'UA', the flightNumber is 'UA110'.</summary>
		public Text FlightNumber {get; set;}
		///<summary>Description of the meals that will be provided or available for purchase.</summary>
		public Text MealService {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public ThisOrThat<Person , Organization> Provider {get; set;}
		///<summary>An entity which offers (sells / leases / lends / loans) the services / goods.  A seller may also be a provider. Supersedes vendor, merchant.</summary>
		public ThisOrThat<Person , Organization> Seller {get; set;}
		///<summary>The time when a passenger can check into the flight online.</summary>
		public DateTime WebCheckinTime {get; set;}
	}
}
