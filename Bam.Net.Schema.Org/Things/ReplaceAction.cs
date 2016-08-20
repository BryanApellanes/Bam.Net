using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of editing a recipient by replacing an old object with a new object.</summary>
	public class ReplaceAction: UpdateAction
	{
		///<summary>A sub property of object. The object that is being replaced.</summary>
		public Thing Replacee {get; set;}
		///<summary>A sub property of object. The object that replaces.</summary>
		public Thing Replacer {get; set;}
	}
}
