/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An educational organization.</summary>
	public class EducationalOrganization: Organization
	{
		///<summary>Alumni of educational organization. Inverse property: alumniOf.</summary>
		public Person Alumni {get; set;}
	}
}
