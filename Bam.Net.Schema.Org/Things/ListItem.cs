using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An list item, e.g. a step in a checklist or how-to description.</summary>
	public class ListItem: Intangible
	{
		///<summary>An entity represented by an entry in a list or data feed (e.g. an 'artist' in a list of 'artists')â€™.</summary>
		public Thing Item {get; set;}
		///<summary>A link to the ListItem that follows the current one.</summary>
		public ListItem NextItem {get; set;}
		///<summary>The position of an item in a series or sequence of items.</summary>
		public ThisOrThat<Integer , Text> Position {get; set;}
		///<summary>A link to the ListItem that preceeds the current one.</summary>
		public ListItem PreviousItem {get; set;}
	}
}
