/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An audio file.</summary>
	public class AudioObject: MediaObject
	{
		///<summary>If this MediaObject is an AudioObject or VideoObject, the transcript of that object.</summary>
		public Text Transcript {get; set;}
	}
}
