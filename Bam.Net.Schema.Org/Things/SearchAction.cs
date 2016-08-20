using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of searching for an object.Related actions:FindAction: SearchAction generally leads to a FindAction, but not necessarily.</summary>
	public class SearchAction: Action
	{
		///<summary>A sub property of instrument. The query used on this action.</summary>
		public Text Query {get; set;}
	}
}
