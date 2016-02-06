/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A subclass of OrganizationRole used to describe employee relationships.</summary>
	public class EmployeeRole: OrganizationRole
	{
		///<summary>The base salary of the job or of an employee in an EmployeeRole.</summary>
		public OneOfThese<Number , PriceSpecification> BaseSalary {get; set;}
		///<summary>The currency (coded using ISO 4217, http://en.wikipedia.org/wiki/ISO_4217 ) used for the main salary information in this job posting or for this employee.</summary>
		public Text SalaryCurrency {get; set;}
	}
}
