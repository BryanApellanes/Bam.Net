using Bam.Net.CommandLine;
using Bam.Net.Schema.Org;
using Bam.Net.Testing;
using Bam.Net.Web;
using CsQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string _genDir = "C:\\bam\\src\\_gen\\Schema.org";
        static string _tmpDir = "C:\\bam\\src\\_gen\\Schema.org.tmp";

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

            foreach (string failedType in FailedTypeNames)
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

        private void Create(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private void WriteCsCode(SpecificType currentType)
        {
            OutLine($"Writing code for {currentType.TypeName}: {currentType.Extends}", ConsoleColor.Cyan);
            string specified = $"{_genDir}\\{currentType.TypeName.LettersOnly()}.cs";
            string path = specified.GetNextFileName(out int num);
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

            while (queue.Count > 0)
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
    }
}
