using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A map.</summary>
	public class Map: CreativeWork
	{
		///<summary>Indicates the kind of Map, from the MapCategoryType Enumeration.</summary>
		public MapCategoryType MapType {get; set;}
	}
}
