using Bam.Net.Presentation.Components;
using Bam.Net.Testing.Unit;
using Markdig;
using Markdig.Helpers;

namespace Bam.Net.Presentation.Tests
{
    class TestComponentParser: ComponentParser
    {
        public StringSlice CallReadUntil(StringSlice slice, char until)
        {
            return ReadUntil(slice, until, out int ignore);
        }
    }
    
    public class ComponentParserTests
    {
        [UnitTest]
        public void CanParseBamComponent()
        {
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder().Build();
            string html = Markdig.Markdown.ToHtml("[!DataElement1:Person(name=Bryan)]");
            
        }
        
        [UnitTest]
        public void ComponentParserReadUntil()
        {
            TestComponentParser parser = new TestComponentParser();
            StringSlice slice = new StringSlice("[!clientId:NameOfComponent(- or |)] # comment", 2, 45);
            StringSlice clientId = parser.CallReadUntil(slice, ':');
            Expect.AreEqual("clientId", clientId.ToString());
        }
    }
}