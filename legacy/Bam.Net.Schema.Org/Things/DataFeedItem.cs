using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A single item within a larger data feed.</summary>
	public class DataFeedItem: Intangible
	{
		///<summary>The date on which the CreativeWork was created or the item was added to a DataFeed.</summary>
		public OneOfThese<Bam.Net.Schema.Org.DataTypes.Date,Bam.Net.Schema.Org.DataTypes.Date> DateCreated {get; set;}
		///<summary>The datetime the item was removed from the DataFeed.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date DateDeleted {get; set;}
		///<summary>The date on which the CreativeWork was most recently modified or when the item's entry was modified within a DataFeed.</summary>
		public OneOfThese<Bam.Net.Schema.Org.DataTypes.Date,Bam.Net.Schema.Org.DataTypes.Date> DateModified {get; set;}
		///<summary>An entity represented by an entry in a list or data feed (e.g. an 'artist' in a list of 'artists')â€™.</summary>
		public Thing Item {get; set;}
	}
}
