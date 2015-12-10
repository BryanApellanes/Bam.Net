/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An infectious disease is a clinically evident human disease resulting from the presence of pathogenic microbial agents, like pathogenic viruses, pathogenic bacteria, fungi, protozoa, multicellular parasites, and prions. To be considered an infectious disease, such pathogens are known to be able to cause this disease.</summary>
	public class InfectiousDisease: MedicalCondition
	{
		///<summary>The actual infectious agent, such as a specific bacterium.</summary>
		public Text InfectiousAgent {get; set;}
		///<summary>The class of infectious agent (bacteria, prion, etc.) that causes the disease.</summary>
		public InfectiousAgentClass InfectiousAgentClass {get; set;}
		///<summary>How the disease spreads, either as a route or vector, for example 'direct contact', 'Aedes aegypti', etc.</summary>
		public Text TransmissionMethod {get; set;}
	}
}
