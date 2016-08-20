using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Intended audience for an item, i.e. the group for whom the item was created.</summary>
	public class Audience: Intangible
	{
		///<summary>The target group associated with a given audience (e.g. veterans, car owners, musicians, etc.).</summary>
		public Text AudienceType {get; set;}
		///<summary>The geographic area associated with the audience.</summary>
		public AdministrativeArea GeographicArea {get; set;}
	}
}
