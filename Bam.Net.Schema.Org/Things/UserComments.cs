/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The UserInteraction event in which a user comments on an item.</summary>
	public class UserComments: UserInteraction
	{
		///<summary>The text of the UserComment.</summary>
		public Text CommentText {get; set;}
		///<summary>The time at which the UserComment was made.</summary>
		public Date CommentTime {get; set;}
		///<summary>The creator/author of this CreativeWork or UserComments. This is the same as the Author property for CreativeWork.</summary>
		public OneOfThese<Person , Organization> Creator {get; set;}
		///<summary>Specifies the CreativeWork associated with the UserComment.</summary>
		public CreativeWork Discusses {get; set;}
		///<summary>The URL at which a reply may be posted to the specified UserComment.</summary>
		public URL ReplyToUrl {get; set;}
	}
}
