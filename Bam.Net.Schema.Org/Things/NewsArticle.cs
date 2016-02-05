using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A news article.</summary>
	public class NewsArticle: Article
	{
		///<summary>The location where the NewsArticle was produced.</summary>
		public Text Dateline {get; set;}
		///<summary>The number of the column in which the NewsArticle appears in the print edition.</summary>
		public Text PrintColumn {get; set;}
		///<summary>The edition of the print product in which the NewsArticle appears.</summary>
		public Text PrintEdition {get; set;}
		///<summary>If this NewsArticle appears in print, this field indicates the name of the page on which the article is found. Please note that this field is intended for the exact page name (e.g. A5, B18).</summary>
		public Text PrintPage {get; set;}
		///<summary>If this NewsArticle appears in print, this field indicates the print section in which the article appeared.</summary>
		public Text PrintSection {get; set;}
	}
}
