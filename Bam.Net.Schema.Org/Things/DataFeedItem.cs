using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A single item within a larger data feed.</summary>
	public class DataFeedItem: Intangible
	{
		///<summary>The date on which the CreativeWork was created or the item was added to a DataFeed.</summary>
		public ThisOrThat<Date , DateTime> DateCreated {get; set;}
		///<summary>The datetime the item was removed from the DataFeed.</summary>
		public DateTime DateDeleted {get; set;}
		///<summary>The date on which the CreativeWork was most recently modified or when the item's entry was modified within a DataFeed.</summary>
		public ThisOrThat<Date , DateTime> DateModified {get; set;}
		///<summary>An entity represented by an entry in a list or data feed (e.g. an 'artist' in a list of 'artists')â€™.</summary>
		public Thing Item {get; set;}
	}
}
