using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An educational organization.</summary>
	public class EducationalOrganization: Organization
	{
		///<summary>Alumni of an organization. Inverse property: alumniOf.</summary>
		public Person Alumni {get; set;}
	}
}
