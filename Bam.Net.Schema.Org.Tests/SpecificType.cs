/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Schema.Org.Tests
{

	public class SpecificType
	{
		string _typeName;
		public string TypeName
		{
			get
			{
				return _typeName;
			}
			set
			{
				_typeName = value.LettersOnly().PascalCase();
			}
		}
		public string Extends { get; set; }

		public override string ToString()
		{
			return "{TypeName} extends {Extends}".NamedFormat(this);
		}
	}
}
