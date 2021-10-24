using System;
using Bam.Net.Presentation.Handlebars;
using Bam.Net.Testing.Unit;
using Bam.Net.Presentation.Html;
using Bam.Net.Testing;
using static Bam.Net.Presentation.Html.Tags;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class HtmlTagTests:CommandLineTool
    {
        [UnitTest]
        public void TagRenders()
        {
            string expected = "<div id=\"monkey\">the content</div>";
            string actual = new Tag("div", new {id = "monkey"}, "the content").Render();
            Expect.AreEqual(expected, actual);
        }

        [UnitTest]
        public void TagRendersNested()
        {
            string expected = @"<div>
  <span>text</span>
</div>";
            string actual = new Tag("div", ()=> new Tag("span", "text")).Render().XmlToHumanReadable();
            Expect.AreEqual(expected, actual);
        }

        [UnitTest]
        public void TagRendersStyles()
        {
            string expected = "<div id=\"banana\" style=\"color: blue; width: 50px;\">the content</div>";
            string actual = new Tag("div", new {id = "banana"}, "the content")
                .AddStyles(new {color = "blue", width = "50px"}).Render(true);
            Expect.AreEqual(expected, actual);
        }

        [UnitTest]
        public void RendersClasses()
        {
            string expected = "<div id=\"banana\" style=\"color: blue;\" class=\"Monkey withTail\">the content</div>";
            string actual = Tags.Div(new {id = "banana"}, "the content").AddStyles(new {color = "blue"})
                .AddClasses("Monkey", "withTail").Render();
            
            Expect.AreEqual(expected, actual);
        }
    }
}