using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An article, such as a news article or piece of investigative report. Newspapers and magazines have articles of many different types and this is intended to cover them all.      See also blog post.</summary>
	public class Article: CreativeWork
	{
		///<summary>The actual body of the article.</summary>
		public Text ArticleBody {get; set;}
		///<summary>Articles may belong to one or more 'sections' in a magazine or newspaper, such as Sports, Lifestyle, etc.</summary>
		public Text ArticleSection {get; set;}
		///<summary>The page on which the work ends; for example "138" or "xvi".</summary>
		public OneOfThese<Text , Integer> PageEnd {get; set;}
		///<summary>The page on which the work starts; for example "135" or "xiii".</summary>
		public OneOfThese<Text , Integer> PageStart {get; set;}
		///<summary>Any description of pages that is not separated into pageStart and pageEnd; for example, "1-6, 9, 55" or "10-12, 46-49".</summary>
		public Text Pagination {get; set;}
		///<summary>The number of words in the text of the Article.</summary>
		public Integer WordCount {get; set;}
	}
}
