using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A movie theater.</summary>
	public class MovieTheater: CivicStructure
	{
		///<summary>The number of screens in the movie theater.</summary>
		public Number ScreenCount {get; set;}
	}
}
