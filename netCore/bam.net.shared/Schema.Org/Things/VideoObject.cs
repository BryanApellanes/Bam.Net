using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A video file.</summary>
	public class VideoObject: MediaObject
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>The caption for this object.</summary>
		public Text Caption {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The composer of the soundtrack.</summary>
		public OneOfThese<MusicGroup,Person> MusicBy {get; set;}
		///<summary>Thumbnail image for an image or video.</summary>
		public ImageObject Thumbnail {get; set;}
		///<summary>If this MediaObject is an AudioObject or VideoObject, the transcript of that object.</summary>
		public Text Transcript {get; set;}
		///<summary>The frame size of the video.</summary>
		public Text VideoFrameSize {get; set;}
		///<summary>The quality of the video.</summary>
		public Text VideoQuality {get; set;}
	}
}
