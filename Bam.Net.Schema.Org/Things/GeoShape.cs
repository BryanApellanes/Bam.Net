/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The geographic shape of a place.</summary>
	public class GeoShape: StructuredValue
	{
		///<summary>A polygon is the area enclosed by a point-to-point path for which the starting and ending points are the same. A polygon is expressed as a series of four or more space delimited points where the first and final points are identical.</summary>
		public Text Box {get; set;}
		///<summary>A circle is the circular region of a specified radius centered at a specified latitude and longitude. A circle is expressed as a pair followed by a radius in meters.</summary>
		public Text Circle {get; set;}
		///<summary>The elevation of a location.</summary>
		public OneOfThese<Number , Text> Elevation {get; set;}
		///<summary>A line is a point-to-point path consisting of two or more points. A line is expressed as a series of two or more point objects separated by space.</summary>
		public Text Line {get; set;}
		///<summary>A polygon is the area enclosed by a point-to-point path for which the starting and ending points are the same. A polygon is expressed as a series of four or more space delimited points where the first and final points are identical.</summary>
		public Text Polygon {get; set;}
	}
}
