using System;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net.Presentation.Handlebars;
using Bam.Net.Presentation.Html;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTool
    {
        [ConsoleAction]
        public void RenderTags()
        {
            HandlebarsDirectory hbs = new HandlebarsDirectory("./Templates");
            string code = string.Empty;
            string tagData = "./Tags.txt".SafeReadFile();
            foreach (string line in tagData.DelimitSplit("\r\n"))
            {
                string tagName = line.ReadUntil('>', out string description);
                if (tagName.StartsWith("<"))
                {
                    tagName = tagName.TruncateFront(1).Trim();
                }

                code += hbs.Render("Tag", new TagDescriptor {TagName = tagName, Description = description.Trim()});
            }
            FileInfo file = new FileInfo("./tags.cs");
            code.SafeWriteToFile(file.FullName, true);
            $"notepad {file.FullName}".Run();
        }
    }
}