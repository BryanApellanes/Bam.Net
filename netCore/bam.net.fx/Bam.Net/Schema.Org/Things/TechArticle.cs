using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A technical article - Example: How-to (task) topics, step-by-step, procedural troubleshooting, specifications, etc.</summary>
	public class TechArticle: Article
	{
		///<summary>Prerequisites needed to fulfill steps in article.</summary>
		public Text Dependencies {get; set;}
		///<summary>Proficiency needed for this content; expected values: 'Beginner', 'Expert'.</summary>
		public Text ProficiencyLevel {get; set;}
	}
}
