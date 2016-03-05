/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// A class used to generate Poco type wrappers which 
	/// enable lazy loading of IEnumerable properties.  This type
    /// is not thread safe
	/// </summary>
	public class PocoGenerator: IAssemblyGenerator
	{
		public PocoGenerator(string nameSpace, TypeSchema typeSchema = null)
		{
			Namespace = nameSpace;
            TypeSchema = typeSchema;
		}

		public string Namespace { get; set; }
        public TypeSchema TypeSchema { get; set; }
        public string WriteSourceTo { get; set; }
        public string InfoFileName => $"{Namespace}.Poco.genInfo.json";

        public void Generate(TypeSchema schema, string writeTo)
		{
            TypeSchema = schema;
            WriteSource(writeTo);
		}

        public Assembly GetGeneratedAssembly()
        {
            return GeneratedAssemblyInfo.GetGeneratedAssembly(InfoFileName, this).Assembly;
        }

        public GeneratedAssemblyInfo GenerateAssembly()
        {
            CompilerResults results;
            string fileName = $"{Namespace}.Poco.dll";
            new DirectoryInfo(WriteSourceTo).ToAssembly(fileName, out results);
            GeneratedAssemblyInfo result = new GeneratedAssemblyInfo(fileName, results);
            result.Save();
            return result;
        }

        public void WriteSource(string writeSourceDir)
        {
            WriteSourceTo = writeSourceDir;
            foreach (Type type in TypeSchema.Tables)
            {
                PocoModel model = new PocoModel(type, TypeSchema, Namespace);
                string fileName = "{0}Poco.cs"._Format(type.Name);
                using (StreamWriter sw = new StreamWriter(Path.Combine(writeSourceDir, fileName)))
                {
                    sw.Write(model.Render());
                }
            }
        }
    }
}
