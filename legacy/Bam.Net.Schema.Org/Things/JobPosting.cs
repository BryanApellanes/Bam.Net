using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A listing that describes a job opening in a certain organization.</summary>
	public class JobPosting: Intangible
	{
		///<summary>The base salary of the job or of an employee in an EmployeeRole.</summary>
		public OneOfThese<MonetaryAmount,Number,PriceSpecification> BaseSalary {get; set;}
		///<summary>Publication date for the job posting.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date DatePosted {get; set;}
		///<summary>Educational background needed for the position.</summary>
		public Text EducationRequirements {get; set;}
		///<summary>Type of employment (e.g. full-time, part-time, contract, temporary, seasonal, internship).</summary>
		public Text EmploymentType {get; set;}
		///<summary>Description of skills and experience needed for the position.</summary>
		public Text ExperienceRequirements {get; set;}
		///<summary>Organization offering the job position.</summary>
		public Organization HiringOrganization {get; set;}
		///<summary>Description of bonus and commission compensation aspects of the job. Supersedes incentives.</summary>
		public Text IncentiveCompensation {get; set;}
		///<summary>The industry associated with the job position.</summary>
		public Text Industry {get; set;}
		///<summary>Description of benefits associated with the job. Supersedes benefits.</summary>
		public Text JobBenefits {get; set;}
		///<summary>A (typically single) geographic location associated with the job position.</summary>
		public Place JobLocation {get; set;}
		///<summary>Category or categories describing the job. Use BLS O*NET-SOC taxonomy: http://www.onetcenter.org/taxonomy.html. Ideally includes textual label and formal code, with the property repeated for each applicable value.</summary>
		public Text OccupationalCategory {get; set;}
		///<summary>Specific qualifications required for this role.</summary>
		public Text Qualifications {get; set;}
		///<summary>Responsibilities associated with this role.</summary>
		public Text Responsibilities {get; set;}
		///<summary>The currency (coded using ISO 4217 ) used for the main salary information in this job posting or for this employee.</summary>
		public Text SalaryCurrency {get; set;}
		///<summary>Skills required to fulfill this role.</summary>
		public Text Skills {get; set;}
		///<summary>Any special commitments associated with this job posting. Valid entries include VeteranCommit, MilitarySpouseCommit, etc.</summary>
		public Text SpecialCommitments {get; set;}
		///<summary>The title of the job.</summary>
		public Text Title {get; set;}
		///<summary>The date after when the item is not valid. For example the end of an offer, salary period, or a period of opening hours.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidThrough {get; set;}
		///<summary>The typical working hours for this job (e.g. 1st shift, night shift, 8am-5pm).</summary>
		public Text WorkHours {get; set;}
	}
}
