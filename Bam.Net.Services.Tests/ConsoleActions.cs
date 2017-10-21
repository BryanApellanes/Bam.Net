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

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction]
        public void CanSerializeAndDeserializeDataPropertyList()
        {
            DataPropertyCollection list = new DataPropertyCollection();
            list.Prop("Monkey", true);
        }

        [ConsoleAction]
        public void ScrapeOpenApi()
        {
            string html = File.ReadAllText("c:\\temp\\OpenAPISpec.htm");
            SQLiteDatabase db = new SQLiteDatabase("c:\\temp\\OpenaApi");
            CQ cq = CQ.Create(html);
            var h4s = cq["h4"];
            h4s.Each(h4 =>
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
                var fieldType = description.NextElementSibling;
                OutLineFormat("{0}", ConsoleColor.Blue, fieldType.InnerText);
                var table = fieldType.NextElementSibling;
                var rows = cq["tr", table];
                rows.Each(row =>
                {
                    var cells = cq["td", row];
                    if (cells.Length > 0)
                    {
                        string fieldName = cells[0].InnerText;
                        string type = CleanUpHtml(cells[1].InnerHTML);
                        string appliesTo = null;
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
                        OutLine();
                        
                    }
                });
            });
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
