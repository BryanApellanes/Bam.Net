using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A part of a successively published publication such as a periodical or multi-volume work, often numbered. It may represent a time span, such as a year.  <br/><br/>See also <a href="http://blog.schema.org/2014/09/schemaorg-support-for-bibliographic_2.html">blog post</a>.</summary>
	public class PublicationVolume: CreativeWork
	{
		///<summary>The page on which the work ends; for example "138" or "xvi".</summary>
		public OneOfThese<Integer,Text> PageEnd {get; set;}
		///<summary>The page on which the work starts; for example "135" or "xiii".</summary>
		public OneOfThese<Integer,Text> PageStart {get; set;}
		///<summary>Any description of pages that is not separated into pageStart and pageEnd; for example, "1-6, 9, 55" or "10-12, 46-49".</summary>
		public Text Pagination {get; set;}
		///<summary>Identifies the volume of publication or multi-part work; for example, "iii" or "2".</summary>
		public OneOfThese<Integer,Text> VolumeNumber {get; set;}
	}
}
