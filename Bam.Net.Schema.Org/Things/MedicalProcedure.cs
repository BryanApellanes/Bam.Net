using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A process of care used in either a diagnostic, therapeutic, or palliative capacity that relies on invasive (surgical), non-invasive, or percutaneous techniques.</summary>
	public class MedicalProcedure: MedicalEntity
	{
		///<summary>Typical or recommended followup care after the procedure is performed.</summary>
		public Text Followup {get; set;}
		///<summary>How the procedure is performed.</summary>
		public Text HowPerformed {get; set;}
		///<summary>Typical preparation that a patient must undergo before having the procedure performed.</summary>
		public Text Preparation {get; set;}
		///<summary>The type of procedure, for example Surgical, Noninvasive, or Percutaneous.</summary>
		public MedicalProcedureType ProcedureType {get; set;}
	}
}
