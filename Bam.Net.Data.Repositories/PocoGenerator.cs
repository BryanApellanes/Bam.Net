/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// A class used to generate Poco type wrappers which 
	/// enable lazy loading of IEnumerable properties.
	/// </summary>
	public class PocoGenerator
	{
		public PocoGenerator(string nameSpace = null)
		{
			this.Namespace = nameSpace;
		}

		public string Namespace { get; set; }

		public void Generate(TypeSchema schema, string writeTo)
		{
			foreach(Type type in schema.Tables)
			{
				PocoModel model = new PocoModel(type, schema, Namespace);
				string fileName = "{0}Poco.cs"._Format(type.Name);
				using (StreamWriter sw = new StreamWriter(Path.Combine(writeTo, fileName)))
				{
					sw.Write(model.Render());
				}
			}
		}
	}
}
