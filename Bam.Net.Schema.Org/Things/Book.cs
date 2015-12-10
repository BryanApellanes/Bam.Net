/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A book.</summary>
	public class Book: CreativeWork
	{
		///<summary>The edition of the book.</summary>
		public Text BookEdition {get; set;}
		///<summary>The format of the book.</summary>
		public BookFormatType BookFormat {get; set;}
		///<summary>The illustrator of the book.</summary>
		public Person Illustrator {get; set;}
		///<summary>The ISBN of the book.</summary>
		public Text Isbn {get; set;}
		///<summary>The number of pages in the book.</summary>
		public Integer NumberOfPages {get; set;}
	}
}
