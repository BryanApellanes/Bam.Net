/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
//using Bam.Net.Presentation.Dust;
using Bam.Net.Javascript;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Linq;
using Bam.Net.Breve;

namespace breve
{
    [Serializable]
    public class Program: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)

            AddValidArgument("file", true, description: "The path to the breve file to analyse");
            AddValidArgument("obj", false, description: "The name of the object to generate for");
            AddValidArgument("name", false, description: "The name of the breve object to generate");
            AddValidArgument("lang", false, description: "The language to generate breve objects for (C# or Java) the default is Java");
            AddValidArgument("bean", true, description: "Use java bean format");
            
            DefaultMethod = typeof(Program).GetMethod("Start");
            Expect.IsNotNull(DefaultMethod);
            #endregion
        }

        public static void Start()
        {
            if (!Arguments.Contains("file"))
            {
                Out("Please specify the js literal file to analyze", ConsoleColor.Cyan);
                Pause();
                Environment.Exit(1);
            }

            FileInfo file = new FileInfo(Arguments["file"]);
            if (!file.Exists)
            {
                Out("The specified file was not found", ConsoleColor.Red);
                Pause();
                Environment.Exit(1);
            }

            string literalName = "breve";            
            if (Arguments.Contains("obj"))
            {
                literalName = Arguments["obj"];            
            }

            string name = literalName.PascalCase();

            Dictionary<Languages, Type> formatTypes = new Dictionary<Languages, Type>
            {
                {Languages.cs, typeof(CSharpFormat) },
                {Languages.java, typeof(JavaFormat) },
            };
            Languages lang = Languages.java;

            if (Arguments.Contains("lang"))
            {
                if (Arguments["lang"].Equals("C#"))
                {
                    lang = Languages.cs;
                }
            }

            Type formatType = formatTypes[lang];
            if (Arguments.Contains("classic"))
            {
                formatType = typeof(JavaClassicFormat);
            }

            string json = file.JsonFromJsLiteralFile(literalName);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            BreveInfo info = new BreveInfo(name, obj, lang);
            BreveGenerator generator = BreveGenerator.Create(formatType, info);
            generator.Go(file.FullName, "{Name}.{Extension}".NamedFormat(new { Name = name, Extension = lang.ToString() }));
            Out("Breve object generation complete", ConsoleColor.Cyan);
            Pause();
        }
    }
}
