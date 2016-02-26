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

		public static HashSet<string> FailedTypeNames { get; private set; }

		Queue<SpecificType> types;
        [ConsoleAction("Generate Schema.org.cs files")]
        public void Generate()
        {
            if (!Directory.Exists("C:\\Schema.org"))
            {
                Directory.CreateDirectory("C:\\Schema.org");
            }
			
			FailedTypeNames = new HashSet<string>();
            types = new Queue<SpecificType>();
            currentType = new SpecificType { TypeName = "Thing", Extends = "DataType" };
            types.Enqueue(currentType);

            while (types.Count > 0)
            {
                currentType = types.Dequeue();
                string content = GetCsCode();
                TryWrite(content);
                foreach (SpecificType specific in GetSpecificTypes(currentType.TypeName))
                {
                    types.Enqueue(specific);
                }
            }

			foreach(string failedType in FailedTypeNames)
			{
				FileInfo file = new FileInfo("C:\\Schema.org\\{0}.cs"._Format(failedType));
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

        private void TryWrite(string content)
        {
			try
			{
				File.WriteAllText("c:\\Schema.org\\" + currentType.TypeName.LettersOnly() + ".cs", content);
			}
			catch (Exception ex)
			{
				OutLineFormat("Unable to write content for type {0}", ConsoleColor.DarkYellow, currentType.TypeName);
			}
        }

        static SpecificType currentType;
        private static string GetCsCode()
        {
            if (string.IsNullOrEmpty(currentType.TypeName))
            {
                return string.Empty;
            }

            SchemaDotOrgProperty[] properties = GetProperties(currentType.TypeName);

            StringBuilder result = new StringBuilder();
            result.AppendLine("using System;");
            result.AppendLine();
            result.AppendLine("namespace Bam.Net.Schema.Org");
            result.AppendLine("{");
            result.AppendFormat("\t///<summary>{0}</summary>\r\n", GetTypeDescription(currentType.TypeName));
            result.AppendFormat("\tpublic class {0}", currentType.TypeName.LettersOnly().PascalCase());
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
				string baseUrl = "http://schema.org/";
				WebClient client = new WebClient();
				string html = client.DownloadString(string.Format("{0}{1}", baseUrl, typeName));
				return html;
			}
			catch (Exception ex)
			{
				OutLineFormat("An error occurred getting html for type {0}", ConsoleColor.Yellow, typeName);
				FailedTypeNames.Add(typeName);
				return string.Empty;
			}
        }

		static List<string> _gotTypes = new List<string>();
        /// <summary>
        /// Gets the extenders of the specified typeName
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static SpecificType[] GetSpecificTypes(string typeName)
        {
			List<SpecificType> result = new List<SpecificType>();
			if (!_gotTypes.Contains(typeName))
			{
				_gotTypes.Add(typeName);
				string html = TryGetHtml(typeName);
				CQ cq = CQ.Create(html);
				Action<IDomObject> eacher = (el) =>
				{
					IDomElement child = el.FirstElementChild;
					string childType = string.Empty;
					if (child != null)
					{
						string href = child.Attributes["href"];
						if (!href.StartsWith("http"))
						{
							childType = child.InnerText;
							result.Add(new SpecificType { Extends = typeName, TypeName = childType });
						}
					}
				};

				cq["#mainContent li"].Each(eacher);
			}

            return result.ToArray();
        }

        [UnitTest("DataType.GetDataType should get expected Types")]
        public void DataTypeGetDataType()
        {
            string[] types = new string[] { "Boolean", "Date", "Number", "Text", "Time", "Url" };
            foreach (string typeName in types)
            {
                object dataType = DataType.GetDataType(typeName);
                Type sysType = DataType.GetTypeOfDataType(typeName);
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
            Integer three = new Integer(3);
            Integer four = new Integer(4);
            int result = AddTest(three, four);
            Expect.IsTrue(result == 7);
        }

        [UnitTest("DataType.GetDataType should get expected generic types")]
        public void DataTypeGetGenericDataType()
        {
            Boolean b = DataType.GetDataType<Boolean>("Boolean");
            Expect.IsNotNull(b);

            Date d = DataType.GetDataType<Date>("Date");
            Expect.IsNotNull(d);

            Number n = DataType.GetDataType<Number>("Number");
            Expect.IsNotNull(n);

            Text t = DataType.GetDataType<Text>("Text");
            Expect.IsNotNull(t);

            Url u = DataType.GetDataType<Url>("Url");
            Expect.IsNotNull(u);
        }

    }

}
