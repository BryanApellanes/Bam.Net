/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>          A Series in schema.org is a group of related items, typically but not necessarily of the same kind.          Series are usually organized into some order, often chronological. Unlike ItemList which          is a general purpose data structure for lists of things, the emphasis with Series          is on published materials (written e.g. books and periodicals, or media such as          tv, radio and games).                    Specific subtypes are available for describing TVSeries, RadioSeries,          MovieSeries,          BookSeries,          Periodical          and VideoGameSeries. In each case,          the hasPart / isPartOf properties          can be used to relate the Series to its parts. The general Series type serves largely          just to organize these more specific and practical subtypes.                    It is common for properties applicable to an item from the series to be usefully applied to the containing group.          Schema.org attempts to anticipate some of these cases, but publishers should be free to apply          properties of the series parts to the series as a whole wherever they seem appropriate.</summary>
	public class Series: CreativeWork
	{
		///<summary>The end date and time of the item (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>The start date and time of the item (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
	}
}
