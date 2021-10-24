using System;
using System.IO;
using System.Xml.Linq;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class NuspecFileTests : CommandLineTool
    {
        [UnitTest]
        public void CanWriteNuspecFile()
        {
            NuspecFile testFile = new NuspecFile($"./{nameof(CanWriteNuspecFile)}.nuspec")
            {
                Id = "TestId", Version = "1.0.x", Authors = "Bryan Apellanes", Description = "test description"
            };

            FileInfo fileInfo = testFile.Write();
            Console.WriteLine(fileInfo.FullName.SafeReadFile());
            string expected = "<package xmlns=\"http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd\">\r\n" +
                              "  <metadata>\r\n" +
                              "    <id>TestId</id>\r\n" +
                              "    <version>1.0.x</version>\r\n" +
                              "    <authors>Bryan Apellanes</authors>\r\n" +
                              "    <description>test description</description>\r\n" +
                              "  </metadata>\r\n" +
                              "  <files>\r\n" +
                              "    <file src=\"_._\" target=\"lib/net5.0\" />\r\n" +
                              "    <file src=\"$publishdir$\\net5.0\\**\\*\" target=\"tools/net5.0\" />\r\n" +
                              "  </files>\r\n" +
                              "</package>";
            Expect.AreEqual(expected, fileInfo.FullName.SafeReadFile());
        }
    }
}