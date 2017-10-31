using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Services.Distributed.Data;
using Bam.Net.Testing;
using Bam.Net.Web;
using CsQuery;
using System.IO;
using Bam.Net.Services.OpenApi;
using Bam.Net.Data.SQLite;
using Bam.Net.Data;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction]
        public void GenerateOpenApiObjects()
        {
            OpenApiObjectDatabase db = new OpenApiObjectDatabase();
            ObjectDescriptorCollection allObjectDescriptors = ObjectDescriptor.LoadAll(db);
            string nameSpace = "Bam.Net.Services.OpenApi.Objects";
            foreach (ObjectDescriptor objectDescriptor in allObjectDescriptors)
            {
                List<OpenApiFixedFieldModel> fields = objectDescriptor.FixedFieldsByObjectDescriptorId.Select(ff => new OpenApiFixedFieldModel { FieldName = ff.FieldName.Trim(), Type = ff.Type.Trim(), Description = ff.Description.Trim() }).ToList();
                
                OpenApiObjectDescriptorModel model = new OpenApiObjectDescriptorModel
                {
                    Namespace = nameSpace,
                    ObjectName = objectDescriptor.Name,
                    ObjectDescription = objectDescriptor.Description,
                    FixedFields = fields
                };
                model.Render().SafeWriteToFile(string.Format("C:\\src\\Bam.Net\\Bam.Net.Services\\OpenApi\\Objects\\{0}.cs", model.ObjectName));
            }
        }

        [ConsoleAction]
        public void CanSerializeAndDeserializeDataPropertyList()
        {
            DataPropertyCollection list = new DataPropertyCollection();
            list.Prop("Monkey", true);
        }

        [ConsoleAction]
        public void ScrapeOpenApi()
        {
            string html = File.ReadAllText("c:\\temp\\OpenAPISpec.htm"); // downloaded from https://swagger.io/specification
            SQLiteDatabase db = new SQLiteDatabase("c:\\temp", "OpenApi");
            db.TryEnsureSchema<ObjectDescriptor>();
            CQ cq = CQ.Create(html);
            var h4s = cq["h4"];
            bool first = true;
            Dictionary<string, Func<Dao>> fieldCtors = new Dictionary<string, Func<Dao>>()
            {
                { "FixedFields", ()=> new FixedField() },
                { "PatternedFields", ()=> new PatternedField() }
            };
            Dictionary<string, string> fieldProperty = new Dictionary<string, string>()
            {
                { "FixedFields", "FieldName" },
                { "PatternedFields", "FieldPattern" }
            };
            h4s.Each(h4 =>
            {
                if (!first)
                {
                    string objectName = h4.InnerText.PascalCase();
                    OutLineFormat("{0}", ConsoleColor.Cyan, objectName);
                    string descriptionText = WhileNextIsP(h4.NextSibling, out IDomObject description);
                    OutLineFormat(descriptionText, ConsoleColor.Green);
                    ObjectDescriptor objectDescriptor = new ObjectDescriptor
                    {
                        Name = objectName,
                        Description = descriptionText,
                        Cuid = NCuid.Cuid.Generate()
                    };
                    objectDescriptor.Save(db);
                    var fieldTypeElement = GetNext(description, "H5");
                    string fieldType = fieldTypeElement.InnerText.PascalCase();
                    if (fieldType.Equals("ParameterLocations"))
                    {
                        fieldTypeElement = GetNext(fieldTypeElement, "H5");
                        fieldType = fieldTypeElement.InnerText.PascalCase();
                    }
                    OutLineFormat("{0}", ConsoleColor.Blue, fieldType);
                    if (fieldCtors.ContainsKey(fieldType))
                    {
                        var table = GetNext(fieldTypeElement, "TABLE");//fieldTypeElement.NextElementSibling;
                        var rows = cq["tr", table];
                        rows.Each(row =>
                        {
                            var cells = cq["td", row];
                            if (cells.Length > 0)
                            {
                                Dao fieldEntry = fieldCtors[fieldType]();
                                fieldEntry.Property("ObjectDescriptorId", objectDescriptor.Id);
                                string fieldName = cells[0].InnerText;
                                string type = CleanUpHtml(cells[1].InnerHTML);
                                string appliesTo = null;
                                fieldEntry.Property(fieldProperty[fieldType], fieldName);
                                fieldEntry.Property("Type", type);
                                fieldEntry.Property("AppliesTo", appliesTo);
                                string fieldDescription = null;
                                if (cells.Length == 4)
                                {
                                    appliesTo = CleanUpHtml(cells[2].InnerHTML);
                                    fieldDescription = CleanUpHtml(cells[3].InnerHTML);
                                }
                                else
                                {
                                    fieldDescription = CleanUpHtml(cells[2].InnerHTML);
                                }
                                OutLineFormat("{0} {1}", ConsoleColor.White, fieldName, type);
                                if (!string.IsNullOrEmpty(appliesTo))
                                {
                                    OutLineFormat("\tApplies To: {0}", ConsoleColor.White, appliesTo);
                                }
                                OutLineFormat("Description: {0}", ConsoleColor.White, fieldDescription);
                                fieldEntry.Property("Description", fieldDescription);
                                fieldEntry.Save(db);
                                OutLine();
                            }
                        });
                    }
                }
                first = false;
            });
        }
        private IDomObject GetNext(IDomObject domObj, string tagType)
        {
            if (domObj.NextElementSibling.NodeName.Equals(tagType, StringComparison.InvariantCultureIgnoreCase))
            {
                return domObj.NextElementSibling;
            }
            else
            {
                return GetNext(domObj.NextElementSibling, tagType);
            }
        }
        private string WhileNextIsP(IDomObject domObj, out IDomObject last)
        {
            string innerText = string.Empty;
            last = domObj;
            if (domObj.NextElementSibling.NodeName.Equals("P"))
            {
                innerText += domObj.NextElementSibling.InnerHTML;
                string next = WhileNextIsP(domObj.NextElementSibling, out last);
                if (!string.IsNullOrEmpty(next))
                {
                    innerText += next;
                }
            }
            return innerText;
        }

        private string CleanUpHtml(string html)
        {
            return html.Replace("<code>", "").Replace("</code>", "").Replace("&quot;", "\"");
        }
    }
}
