/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of participating in exertive activity for the purposes of improving health and fitness.</summary>
	public class ExerciseAction: PlayAction
	{
		///<summary>A sub property of location. The course where this action was taken.</summary>
		public Place Course {get; set;}
		///<summary>A sub property of instrument. The diet used in this action.</summary>
		public Diet Diet {get; set;}
		///<summary>The distance travelled, e.g. exercising or travelling.</summary>
		public Distance Distance {get; set;}
		///<summary>A sub property of instrument. The exercise plan used on this action.</summary>
		public ExercisePlan ExercisePlan {get; set;}
		///<summary>Type(s) of exercise or activity, such as strength training, flexibility training, aerobics, cardiac rehabilitation, etc.</summary>
		public Text ExerciseType {get; set;}
		///<summary>A sub property of location. The original location of the object or the agent before the action.</summary>
		public Place FromLocation {get; set;}
		///<summary>A sub property of participant. The opponent on this action.</summary>
		public Person Opponent {get; set;}
		///<summary>A sub property of location. The sports activity location where this action occurred.</summary>
		public SportsActivityLocation SportsActivityLocation {get; set;}
		///<summary>A sub property of location. The sports event where this action occurred.</summary>
		public SportsEvent SportsEvent {get; set;}
		///<summary>A sub property of participant. The sports team that participated on this action.</summary>
		public SportsTeam SportsTeam {get; set;}
		///<summary>A sub property of location. The final location of the object or the agent after the action.</summary>
		public Place ToLocation {get; set;}
	}
}
