using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An image file.</summary>
	public class ImageObject: MediaObject
	{
		///<summary>The caption for this object.</summary>
		public Text Caption {get; set;}
		///<summary>exif data for this object.</summary>
		public ThisOrThat<PropertyValue , Text> ExifData {get; set;}
		///<summary>Indicates whether this image is representative of the content of the page.</summary>
		public Boolean RepresentativeOfPage {get; set;}
		///<summary>Thumbnail image for an image or video.</summary>
		public ImageObject Thumbnail {get; set;}
	}
}
