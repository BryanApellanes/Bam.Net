using System;
using Bam.Net.Presentation.Html;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class HtmlRenderingTests: CommandLineTool
    {
        [UnitTest]
        public void ShouldRenderDynamicObject()
        {
            string html = Tags.A(new {href = "bamapps.net"}, "this is the text").Render();
            Expect.AreEqual("<a href=\"bamapps.net\">this is the text</a>", html);
        } 
        
        [UnitTest]
        public void ShouldRenderNested()
        {
            //string html = Tags.Div()
        }
    }
}