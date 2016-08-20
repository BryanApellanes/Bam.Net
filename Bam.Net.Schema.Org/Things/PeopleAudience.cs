using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A set of characteristics belonging to people, e.g. who compose an item's target audience.</summary>
	public class PeopleAudience: Audience
	{
		///<summary>Audiences defined by a person's gender.</summary>
		public Text RequiredGender {get; set;}
		///<summary>Audiences defined by a person's maximum age.</summary>
		public Integer RequiredMaxAge {get; set;}
		///<summary>Audiences defined by a person's minimum age.</summary>
		public Integer RequiredMinAge {get; set;}
		///<summary>The gender of the person or audience.</summary>
		public Text SuggestedGender {get; set;}
		///<summary>Maximal age recommended for viewing content.</summary>
		public Number SuggestedMaxAge {get; set;}
		///<summary>Minimal age recommended for viewing content.</summary>
		public Number SuggestedMinAge {get; set;}
	}
}
