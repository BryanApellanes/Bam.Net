using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A musical composition.</summary>
	public class MusicComposition: CreativeWork
	{
		///<summary>The person or organization who wrote a composition, or who is the composer of a work performed at some event.</summary>
		public OneOfThese<Organization,Person> Composer {get; set;}
		///<summary>The date and place the work was first performed.</summary>
		public Event FirstPerformance {get; set;}
		///<summary>Smaller compositions included in this work (e.g. a movement in a symphony).</summary>
		public MusicComposition IncludedComposition {get; set;}
		///<summary>The International Standard Musical Work Code for the composition.</summary>
		public Text IswcCode {get; set;}
		///<summary>The person who wrote the words.</summary>
		public Person Lyricist {get; set;}
		///<summary>The words in the song.</summary>
		public CreativeWork Lyrics {get; set;}
		///<summary>An arrangement derived from the composition.</summary>
		public MusicComposition MusicArrangement {get; set;}
		///<summary>The type of composition (e.g. overture, sonata, symphony, etc.).</summary>
		public Text MusicCompositionForm {get; set;}
		///<summary>The key, mode, or scale this composition uses.</summary>
		public Text MusicalKey {get; set;}
		///<summary>An audio recording of the work. Inverse property: recordingOf.</summary>
		public MusicRecording RecordedAs {get; set;}
	}
}
