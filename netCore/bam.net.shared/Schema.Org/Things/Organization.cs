using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An organization such as a school, NGO, corporation, club, etc.</summary>
	public class Organization: Thing
	{
		///<summary>Physical address of the item.</summary>
		public OneOfThese<PostalAddress,Text> Address {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>Alumni of an organization. Inverse property: alumniOf.</summary>
		public Person Alumni {get; set;}
		///<summary>The geographic area where a service or offered item is provided. Supersedes serviceArea.</summary>
		public OneOfThese<AdministrativeArea,GeoShape,Place,Text> AreaServed {get; set;}
		///<summary>An award won by or for this item. Supersedes awards.</summary>
		public Text Award {get; set;}
		///<summary>The brand(s) associated with a product or service, or the brand(s) maintained by an organization or business person.</summary>
		public OneOfThese<Brand,Organization> Brand {get; set;}
		///<summary>A contact point for a person or organization. Supersedes contactPoints.</summary>
		public ContactPoint ContactPoint {get; set;}
		///<summary>A relationship between an organization and a department of that organization, also described as an organization (allowing different urls, logos, opening hours). For example: a store with a pharmacy, or a bakery with a cafe.</summary>
		public Organization Department {get; set;}
		///<summary>The date that this organization was dissolved.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date DissolutionDate {get; set;}
		///<summary>The Dun & Bradstreet DUNS number for identifying an organization or business person.</summary>
		public Text Duns {get; set;}
		///<summary>Email address.</summary>
		public Text Email {get; set;}
		///<summary>Someone working for this organization. Supersedes employees.</summary>
		public Person Employee {get; set;}
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>A person who founded this organization. Supersedes founders.</summary>
		public Person Founder {get; set;}
		///<summary>The date that this organization was founded.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date FoundingDate {get; set;}
		///<summary>The place where the Organization was founded.</summary>
		public Place FoundingLocation {get; set;}
		///<summary>A person or organization that supports (sponsors) something through some kind of financial contribution.</summary>
		public OneOfThese<Organization,Person> Funder {get; set;}
		///<summary>The Global Location Number (GLN, sometimes also referred to as International Location Number or ILN) of the respective organization, person, or place. The GLN is a 13-digit number used to identify parties and physical locations.</summary>
		public Text GlobalLocationNumber {get; set;}
		///<summary>Indicates an OfferCatalog listing for this Organization, Person, or Service.</summary>
		public OfferCatalog HasOfferCatalog {get; set;}
		///<summary>Points-of-Sales operated by the organization or person.</summary>
		public Place HasPOS {get; set;}
		///<summary>The International Standard of Industrial Classification of All Economic Activities (ISIC), Revision 4 code for a particular organization, business person, or place.</summary>
		public Text IsicV4 {get; set;}
		///<summary>The official name of the organization, e.g. the registered company name.</summary>
		public Text LegalName {get; set;}
		///<summary>An organization identifier that uniquely identifies a legal entity as defined in ISO 17442.</summary>
		public Text LeiCode {get; set;}
		///<summary>The location of for example where the event is happening, an organization is located, or where an action takes place.</summary>
		public OneOfThese<Place,PostalAddress,Text> Location {get; set;}
		///<summary>An associated logo.</summary>
		public OneOfThese<ImageObject,Url> Logo {get; set;}
		///<summary>A pointer to products or services offered by the organization or person. Inverse property: offeredBy.</summary>
		public Offer MakesOffer {get; set;}
		///<summary>A member of an Organization or a ProgramMembership. Organizations can be members of organizations; ProgramMembership is typically for individuals. Supersedes members, musicGroupMember. Inverse property: memberOf.</summary>
		public OneOfThese<Organization,Person> Member {get; set;}
		///<summary>An Organization (or ProgramMembership) to which this Person or Organization belongs. Inverse property: member.</summary>
		public OneOfThese<Organization,ProgramMembership> MemberOf {get; set;}
		///<summary>The North American Industry Classification System (NAICS) code for a particular organization or business person.</summary>
		public Text Naics {get; set;}
		///<summary>The number of employees in an organization e.g. business.</summary>
		public QuantitativeValue NumberOfEmployees {get; set;}
		///<summary>Products owned by the organization or person.</summary>
		public OneOfThese<OwnershipInfo,Product> Owns {get; set;}
		///<summary>The larger organization that this local business is a branch of, if any. Supersedes branchOf. Inverse property: subOrganization.</summary>
		public Organization ParentOrganization {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>A pointer to products or services sought by the organization or person (demand).</summary>
		public Demand Seeks {get; set;}
		///<summary>A person or organization that supports a thing through a pledge, promise, or financial contribution. e.g. a sponsor of a Medical Study or a corporate sponsor of an event.</summary>
		public OneOfThese<Organization,Person> Sponsor {get; set;}
		///<summary>A relationship between two organizations where the first includes the second, e.g., as a subsidiary. See also: the more specific 'department' property. Inverse property: parentOrganization.</summary>
		public Organization SubOrganization {get; set;}
		///<summary>The Tax / Fiscal ID of the organization or person, e.g. the TIN in the US or the CIF/NIF in Spain.</summary>
		public Text TaxID {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
		///<summary>The Value-added Tax ID of the organization or person.</summary>
		public Text VatID {get; set;}
	}
}
