/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using System.Net;
using CsQuery;
using Sdo = Bam.Net.Schema.Org;
using System.Xml;
using Bam.Net.Web;
using System.Threading.Tasks;
using System.Threading;
using Dt = Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");			
			DefaultMethod = typeof(Program).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(Program).Assembly);
			}
			else
			{
				Interactive();
			}
		}

        static string _genDir = "C:\\Schema.org";
        static string _tmpDir = "C:\\Schema.org.tmp";
        private void Create(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
		public static HashSet<string> FailedTypeNames { get; private set; }
        static HashSet<string> _writtenTypes;
        [ConsoleAction("Generate Schema.org.cs files")]
        public void Generate()
        {
            Create(_genDir);
            Create(_tmpDir);

			FailedTypeNames = new HashSet<string>();
            _writtenTypes = new HashSet<string>();
            HashSet<SpecificType> types = GetTypes();
            types.ToList().ForEach(currentType =>
            {
                WriteCsCode(currentType);
            });
            
			foreach(string failedType in FailedTypeNames)
			{
				FileInfo file = new FileInfo($"{_genDir}\\{0}.cs"._Format(failedType));
                try
				{
					file.Delete();
				}
				catch (Exception ex)
				{
					OutLineFormat("Failed to delete file {0}: {1}", ConsoleColor.Magenta, file.FullName, ex.Message);
				}
			}
        }



        [ConsoleAction("List sub types")]
        public void ListTypes()
        {
            string topType = Prompt("Enter the top type to retrieve sub types for");
            GetSubTypes(topType).ToList().ForEach(st =>
            {
                OutLine($"{st.TypeName} extends {st.Extends}");
            });
        }

        [ConsoleAction("List all types")]
        public void ListAllTypes()
        {
            HashSet<SpecificType> allTypes = GetTypes();
            OutLine($"Found {allTypes.Count} types");
            Pause();
            allTypes.ToList().ForEach(st =>
            {
                OutLine($"{st.TypeName} extends {st.Extends}");
            });
        }

        private void WriteCsCode(SpecificType currentType)
        {
            OutLine($"Writing code for {currentType.TypeName}: {currentType.Extends}", ConsoleColor.Cyan);
            int num;
            string specified = $"C:\\Schema.org\\{currentType.TypeName.LettersOnly()}.cs";
            string path = specified.GetNextFileName(out num);
            if (num > 0)
            {
                currentType.ClassName = $"{currentType.ClassName}_{num}";
            }
            string code = GetCsCode(currentType);
            TryWrite(code, path);
        }

        private HashSet<SpecificType> GetTypes()
        {
            HashSet<SpecificType> results = new HashSet<SpecificType>();
            Queue<SpecificType> queue = new Queue<SpecificType>();
            SpecificType thing = new SpecificType { TypeName = "Thing", Extends = "DataType" };
            results.Add(thing);
            queue.Enqueue(thing);

            while(queue.Count > 0)
            {
                OutLine($"Queue count is {queue.Count}", ConsoleColor.Yellow);
                SpecificType currentType = queue.Dequeue();
                OutLine($"Getting sub types for {currentType.TypeName}", ConsoleColor.Cyan);
                List<SpecificType> subTypes = GetSubTypes(currentType.TypeName).ToList();
                OutLine($"Found {subTypes.Count} sub types", ConsoleColor.Blue);
                subTypes.ForEach(subType =>
                {
                    queue.Enqueue(subType);
                    results.Add(subType);
                    OutLine($"\t{subType.TypeName} extends {subType.Extends}", ConsoleColor.DarkCyan);
                });
            }

            return results;
        }

        private HashSet<SpecificType> GetSubTypes(string topType)
        {
            HashSet<SpecificType> types = new HashSet<SpecificType>();
            CQ cq = CQ.Create(GetFullHtml());
            cq[$"#{topType} > ul > li"].Each(li =>
            {
                types.Add(new SpecificType { TypeName = cq[li].Attr("id"), Extends = topType });
            });
            return types;
        }

        private void TryWrite(string content, string path)
        {
			try
			{
                File.WriteAllText(path, content);
			}
			catch (Exception ex)
			{
				OutLineFormat("Unable to write content for path {0}\r\n{1}\r\n\r\n{2}", ConsoleColor.DarkYellow, path, ex.Message, ex.StackTrace);
			}
        }
        
        

        private string GetCsCode(SpecificType currentType)
        {
            if (string.IsNullOrEmpty(currentType.TypeName))
            {
                return string.Empty;
            }

            SchemaDotOrgProperty[] properties = GetProperties(currentType.TypeName);

            StringBuilder result = new StringBuilder();
            result.AppendLine("using Bam.Net.Schema.Org.DataTypes;");
            result.AppendLine();
            result.AppendLine("namespace Bam.Net.Schema.Org.Things");
            result.AppendLine("{");
            result.AppendFormat("\t///<summary>{0}</summary>\r\n", GetTypeDescription(currentType.TypeName));
            result.AppendFormat("\tpublic class {0}", currentType.ClassName);
            if (!string.IsNullOrEmpty(currentType.Extends))
            {
                result.AppendFormat(": {0}", currentType.Extends);
            }
            result.AppendLine();
            result.AppendLine("\t{");
            foreach (SchemaDotOrgProperty prop in properties)
            {
                result.AppendFormat("\t\t///<summary>{0}</summary>\r\n", prop.Description);
				result.AppendFormat("\t\tpublic {0} {1} {{get; set;}}\r\n", prop.ExpectedType, prop.Name.PrefixWithUnderscoreIfStartsWithNumber().PascalCase());
            }
            result.AppendLine("\t}");
            result.AppendLine("}");

            return result.ToString();
        }

        private static string GetTypeDescription(string typeName)
        {
            string html = TryGetHtml(typeName);
			string returnValue = string.Empty;
			if (!string.IsNullOrEmpty(html))
			{
				CQ cq = CQ.Create(html);
				cq = cq.Remove("script");
				returnValue = cq["[property='rdfs:comment']"].First().Text().Replace("\r", "").Replace("\n", "");
			}

			return returnValue;
        }

        private static SchemaDotOrgProperty[] GetProperties(string typeName)
        {
            string html = TryGetHtml(typeName);
            CQ cq = CQ.Create(html);
            CQ propBody = cq[string.Format(".definition-table .supertype-name a[href='/{0}']", typeName)].First().ParentsUntil(".definition-table").Next();
            List<SchemaDotOrgProperty> properties = new List<SchemaDotOrgProperty>();
            cq["tr", propBody].Each((row) =>
            {
                string propName = cq[".prop-nam", row].Children().First().Text().PascalCase();
                if (!string.IsNullOrEmpty(propName))
                {
                    string expectedType = cq[".prop-ect", row].Text().Trim();
                    string description = cq[".prop-desc", row].Text().Trim().Replace("\r", "").Replace("\n", "");
                    if (properties.Where(p => p.Name.Equals(propName)).FirstOrDefault() == null)
                    {
                        properties.Add(new SchemaDotOrgProperty { Name = propName, ExpectedType = expectedType, Description = description });
                    }
                }
            });

            return properties.ToArray();
        }

        private static string TryGetHtml(string typeName)
        {
			try
			{
                FileInfo typeFile = new FileInfo(Path.Combine(_tmpDir, $"{typeName}.html"));
                if (typeFile.Exists)
                {
                    return typeFile.ReadAllText();
                }

				string baseUrl = "http://schema.org/";
				WebClient client = new WebClient();
				string html = client.DownloadString(string.Format("{0}{1}", baseUrl, typeName));
                html.SafeWriteToFile(typeFile.FullName);
				return html;
			}
			catch (Exception ex)
			{
				OutLineFormat("An error occurred getting html for type {0}\r\n{1}\r\n\r\n{2}", ConsoleColor.Yellow, typeName, ex.Message, ex.StackTrace);
				FailedTypeNames.Add(typeName);
				return string.Empty;
			}
        }

        string fullHtml = string.Empty;
        private string GetFullHtml()
        {
            string url = "http://schema.org/docs/full.html";
            if (string.IsNullOrEmpty(fullHtml))
            {
                fullHtml = Http.Get(url);
            }
            return fullHtml;
        }

        [UnitTest("DataType.GetDataType should get expected Types")]
        public void DataTypeGetDataType()
        {
            string[] types = new string[] { "Boolean", "Date", "Number", "Text", "Time", "Url" };
            foreach (string typeName in types)
            {
                object dataType = Dt.DataType.GetDataType(typeName);
                Type sysType = Dt.DataType.GetTypeOfDataType(typeName);
                Expect.AreSame(sysType, dataType.GetType());
            }            
        }

        private int AddTest(int one, int two)
        {
            return one + two;
        }

        [UnitTest("Schema Integer should be implicitly int")]
        public void SchemaInteger()
        {
            Dt.Integer three = new Dt.Integer(3);
            Dt.Integer four = new Dt.Integer(4);
            int result = AddTest(three, four);
            Expect.IsTrue(result == 7);
        }

        [UnitTest("DataType.GetDataType should get expected generic types")]
        public void DataTypeGetGenericDataType()
        {
            Dt.Boolean b = Dt.DataType.GetDataType<Dt.Boolean>("Boolean");
            Expect.IsNotNull(b);

            Dt.Date d = Dt.DataType.GetDataType<Dt.Date>("Date");
            Expect.IsNotNull(d);

            Dt.Number n = Dt.DataType.GetDataType<Dt.Number>("Number");
            Expect.IsNotNull(n);

            Dt.Text t = Dt.DataType.GetDataType<Dt.Text>("Text");
            Expect.IsNotNull(t);

            Dt.Url u = Dt.DataType.GetDataType<Dt.Url>("Url");
            Expect.IsNotNull(u);
        }

    }

}
