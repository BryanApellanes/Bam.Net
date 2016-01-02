/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of participating in performance arts.</summary>
	public class PerformAction: PlayAction
	{
		///<summary>A sub property of location. The entertainment business where the action occurred.</summary>
		public EntertainmentBusiness EntertainmentBusiness {get; set;}
	}
}
