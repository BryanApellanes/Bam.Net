using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Data.Schema;
using Bam.Net.Data.Schema.GraphQL;
using Bam.Net.Razor;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Repositories.Tests
{

    [Serializable]
    public class GraphQLUnitTests: CommandLineTestInterface
    {
        [ConsoleAction]
        public void CanGenerateEndpointWithArguments()
        {
            GraphQLArgument[] arguments = new GraphQLArgument[]
            {
                new GraphQLArgument{ Name ="Arg1", Type = "ArgType1" },
                new GraphQLArgument{ Name ="Arg2", Type = "ArgType2" },
                new GraphQLArgument{ Name ="Arg3", Type = "ArgType3" },
            };
            GraphQLEndpoint ep = new GraphQLEndpoint { EndpointName = "TheEndpoint", Arguments = arguments, ReturnType = "TheReturnType" };

            RazorParser<GraphQLRazorTemplate<GraphQLEndpoint>> razorParser = new RazorParser<GraphQLRazorTemplate<GraphQLEndpoint>>();
            List<Assembly> referenceAssemblies = new List<Assembly>();
            referenceAssemblies.AddRange(DaoGenerator.GetReferenceAssemblies());
            referenceAssemblies.Add(typeof(GraphQLEndpoint).Assembly);
            razorParser.GetDefaultAssembliesToReference = () => referenceAssemblies.ToArray();
            FileInfo file = new FileInfo(".\\GraphQL\\Templates\\GraphQLEndpoint.tmpl");
            OutLineFormat("razor template: {0}", file.FullName);
            string filePath = file.FullName;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string output = razorParser.Execute(sr, new { Model = ep });
                OutLine(output);
                string expected = "TheEndpoint(Arg1: ArgType1, Arg2: ArgType2, Arg3: ArgType3): TheReturnType";
                Expect.AreEqual(expected, output);
            }            
        }

        [ConsoleAction]
        public void CanGenerateEndpointWithoutArguments()
        {
            GraphQLEndpoint ep = new GraphQLEndpoint { EndpointName = "TheEndpoint", Arguments = new GraphQLArgument[] { }, ReturnType = "TheReturnType" };

            RazorParser<GraphQLRazorTemplate<GraphQLEndpoint>> razorParser = new RazorParser<GraphQLRazorTemplate<GraphQLEndpoint>>();
            List<Assembly> referenceAssemblies = new List<Assembly>();
            referenceAssemblies.AddRange(DaoGenerator.GetReferenceAssemblies());
            referenceAssemblies.Add(typeof(GraphQLEndpoint).Assembly);
            razorParser.GetDefaultAssembliesToReference = () => referenceAssemblies.ToArray();
            FileInfo file = new FileInfo(".\\GraphQL\\Templates\\GraphQLEndpoint.tmpl");
            OutLineFormat("razor template: {0}", file.FullName);
            string filePath = file.FullName;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string output = razorParser.Execute(sr, new { Model = ep });
                OutLine(output);
                string expected = "TheEndpoint: TheReturnType";
                Expect.AreEqual(expected, output);
            }
        }
    }
}
