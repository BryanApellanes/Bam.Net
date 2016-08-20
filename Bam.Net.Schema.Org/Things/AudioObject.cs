using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An audio file.</summary>
	public class AudioObject: MediaObject
	{
		///<summary>If this MediaObject is an AudioObject or VideoObject, the transcript of that object.</summary>
		public Text Transcript {get; set;}
	}
}
