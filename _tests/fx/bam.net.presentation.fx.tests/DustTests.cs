using Bam.Net.Presentation.Dust;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bam.Net.Presentation;
using System.Text.RegularExpressions;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class DustTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void ShouldCompileDust()
        {
            DustTemplate dc = new DustTemplate("Hello {name}!", "test");
            dc.Compile();
            OutLine();
            OutLine(dc.CompiledScript, ConsoleColor.Green);
        }

        [UnitTest]
        public static void ShouldRenderDust()
        {
            DustTemplate dc = new DustTemplate("Hello {Name}!", "test");
            object value = new { Name = "Guy" };
            string result = dc.Render(value);
            Expect.AreEqual("Hello Guy!", result);
            OutLine();
            OutLine(result, ConsoleColor.Yellow);
            value = new { Name = "Dude" };
            result = dc.Render(value);
            Expect.AreEqual("Hello Dude!", result);
            OutLine(result, ConsoleColor.Yellow);
        }

        [UnitTest]
        public static void ShouldRenderDustUsingStatic()
        {
            DustTemplate dc = new DustTemplate("Hello {Name}!", "test");
            dc.Compile();
            object value = new { Name = "Guy" };
            MvcHtmlString result = Bam.Net.Presentation.Dust.Dust.RenderMvcHtmlString("test", value);
            Expect.AreEqual("Hello Guy!", result.ToHtmlString());
            OutLine();
            OutLine(result.ToString(), ConsoleColor.Yellow);
            value = new { Name = "Dude" };
            result = Bam.Net.Presentation.Dust.Dust.RenderMvcHtmlString("test", value);
            Expect.AreEqual("Hello Dude!", result.ToHtmlString());
            OutLine(result.ToString(), ConsoleColor.Yellow);
        }

        [UnitTest]
        public static void TestFunkyEscapeSequenceInServerRender()
        {
            DustTemplate dc = new DustTemplate(@"Hello {Name}!
this is another line
and another", "test");
            dc.Compile();

            object value = new { Name = "Guy" };
            string result = Bam.Net.Presentation.Dust.Dust.RenderMvcHtmlString("test", value).ToString();
            OutLine(result, ConsoleColor.Yellow);
        }

        [UnitTest]
        public static void TestUnescape()
        {
            string value = @"\r\n\t";
            OutLineFormat("Value: {0}", ConsoleColor.Yellow, value);
            OutLineFormat("Unescaped Value: {0}", ConsoleColor.Cyan, Regex.Unescape(value));
        }

        [UnitTest]
        public static void TemplateNameShouldBeFileNameWithoutExtension()
        {
            FileInfo file = new FileInfo("test.dust");
            FileInfo file2 = new FileInfo("test2.txt");
            if (!file.Exists)
            {
                "Hello {name}!".SafeWriteToFile(file.FullName);
                file.Refresh();
            }

            if (!file2.Exists)
            {
                "Hello2 {name}!".SafeWriteToFile(file2.FullName);
                file2.Refresh();
            }

            DustTemplate dc = new DustTemplate(file);
            Expect.AreEqual("test", dc.Name);
            dc = new DustTemplate(file2);
            Expect.AreEqual("test2", dc.Name);
        }


    }
}
