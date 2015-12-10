/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A person (alive, dead, undead, or fictional).</summary>
	public class Person: Thing
	{
		///<summary>An additional name for a Person, can be used for a middle name.</summary>
		public Text AdditionalName {get; set;}
		///<summary>Physical address of the item.</summary>
		public PostalAddress Address {get; set;}
		///<summary>An organization that this person is affiliated with. For example, a school/university, a club, or a team.</summary>
		public Organization Affiliation {get; set;}
		///<summary>An educational organizations that the person is an alumni of. Inverse property: alumni.</summary>
		public EducationalOrganization AlumniOf {get; set;}
		///<summary>An award won by this person or for this creative work. Supersedes awards.</summary>
		public Text Award {get; set;}
		///<summary>Date of birth.</summary>
		public Date BirthDate {get; set;}
		///<summary>The place where the person was born.</summary>
		public Place BirthPlace {get; set;}
		///<summary>The brand(s) associated with a product or service, or the brand(s) maintained by an organization or business person.</summary>
		public ThisOrThat<Brand , Organization> Brand {get; set;}
		///<summary>A child of the person.</summary>
		public Person Children {get; set;}
		///<summary>A colleague of the person. Supersedes colleagues.</summary>
		public Person Colleague {get; set;}
		///<summary>A contact point for a person or organization. Supersedes contactPoints.</summary>
		public ContactPoint ContactPoint {get; set;}
		///<summary>Date of death.</summary>
		public Date DeathDate {get; set;}
		///<summary>The place where the person died.</summary>
		public Place DeathPlace {get; set;}
		///<summary>The Dun & Bradstreet DUNS number for identifying an organization or business person.</summary>
		public Text Duns {get; set;}
		///<summary>Email address.</summary>
		public Text Email {get; set;}
		///<summary>Family name. In the U.S., the last name of an Person. This can be used along with givenName instead of the name property.</summary>
		public Text FamilyName {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>The most generic uni-directional social relation.</summary>
		public Person Follows {get; set;}
		///<summary>Gender of the person.</summary>
		public Text Gender {get; set;}
		///<summary>Given name. In the U.S., the first name of a Person. This can be used along with familyName instead of the name property.</summary>
		public Text GivenName {get; set;}
		///<summary>The Global Location Number (GLN, sometimes also referred to as International Location Number or ILN) of the respective organization, person, or place. The GLN is a 13-digit number used to identify parties and physical locations.</summary>
		public Text GlobalLocationNumber {get; set;}
		///<summary>Points-of-Sales operated by the organization or person.</summary>
		public Place HasPOS {get; set;}
		///<summary>The height of the item.</summary>
		public ThisOrThat<Distance , QuantitativeValue> Height {get; set;}
		///<summary>A contact location for a person's residence.</summary>
		public ThisOrThat<ContactPoint , Place> HomeLocation {get; set;}
		///<summary>An honorific prefix preceding a Person's name such as Dr/Mrs/Mr.</summary>
		public Text HonorificPrefix {get; set;}
		///<summary>An honorific suffix preceding a Person's name such as M.D. /PhD/MSCSW.</summary>
		public Text HonorificSuffix {get; set;}
		///<summary>A count of a specific user interactions with this item—for example, 20 UserLikes, 5 UserComments, or 300 UserDownloads. The user interaction type should be one of the sub types of UserInteraction.</summary>
		public Text InteractionCount {get; set;}
		///<summary>The International Standard of Industrial Classification of All Economic Activities (ISIC), Revision 4 code for a particular organization, business person, or place.</summary>
		public Text IsicV4 {get; set;}
		///<summary>The job title of the person (for example, Financial Manager).</summary>
		public Text JobTitle {get; set;}
		///<summary>The most generic bi-directional social/work relation.</summary>
		public Person Knows {get; set;}
		///<summary>A pointer to products or services offered by the organization or person.</summary>
		public Offer MakesOffer {get; set;}
		///<summary>An Organization (or ProgramMembership) to which this Person or Organization belongs. Inverse property: member.</summary>
		public ThisOrThat<Organization , ProgramMembership> MemberOf {get; set;}
		///<summary>The North American Industry Classification System (NAICS) code for a particular organization or business person.</summary>
		public Text Naics {get; set;}
		///<summary>Nationality of the person.</summary>
		public Country Nationality {get; set;}
		///<summary>The total financial value of the organization or person as calculated by subtracting assets from liabilities.</summary>
		public PriceSpecification NetWorth {get; set;}
		///<summary>Products owned by the organization or person.</summary>
		public ThisOrThat<Product , OwnershipInfo> Owns {get; set;}
		///<summary>A parent of this person. Supersedes parents.</summary>
		public Person Parent {get; set;}
		///<summary>Event that this person is a performer or participant in.</summary>
		public Event PerformerIn {get; set;}
		///<summary>The most generic familial relation.</summary>
		public Person RelatedTo {get; set;}
		///<summary>A pointer to products or services sought by the organization or person (demand).</summary>
		public Demand Seeks {get; set;}
		///<summary>A sibling of the person. Supersedes siblings.</summary>
		public Person Sibling {get; set;}
		///<summary>The person's spouse.</summary>
		public Person Spouse {get; set;}
		///<summary>The Tax / Fiscal ID of the organization or person, e.g. the TIN in the US or the CIF/NIF in Spain.</summary>
		public Text TaxID {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
		///<summary>The Value-added Tax ID of the organization or person.</summary>
		public Text VatID {get; set;}
		///<summary>The weight of the product or person.</summary>
		public QuantitativeValue Weight {get; set;}
		///<summary>A contact location for a person's place of work.</summary>
		public ThisOrThat<ContactPoint , Place> WorkLocation {get; set;}
		///<summary>Organizations that the person works for.</summary>
		public Organization WorksFor {get; set;}
	}
}
