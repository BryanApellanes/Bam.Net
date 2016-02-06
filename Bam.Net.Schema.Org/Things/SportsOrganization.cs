/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Represents the collection of all sports organizations, including sports teams, governing bodies, and sports associations.</summary>
	public class SportsOrganization: Organization
	{
		///<summary>A type of sport (e.g. Baseball).</summary>
		public OneOfThese<URL , Text> Sport {get; set;}
	}
}
