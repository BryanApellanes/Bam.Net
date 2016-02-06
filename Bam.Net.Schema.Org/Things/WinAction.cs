using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of achieving victory in a competitive activity.</summary>
	public class WinAction: AchieveAction
	{
		///<summary>A sub property of participant. The loser of the action.</summary>
		public Person Loser {get; set;}
	}
}
