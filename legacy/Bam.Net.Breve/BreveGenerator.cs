/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bam.Net.Breve
{
    public class BreveGenerator
    {
        public BreveGenerator(BreveInfo info)
        {
            this.Info = info;
        }
        public Languages Language { get; set; }
        public BreveInfo Info { get; private set; }

        public void Go(string inputFile, string outputFile, string literalName = "breve")
        {
            string json = inputFile.JsonFromJsLiteralFile(literalName);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            BreveInfo info = new BreveInfo(literalName.PascalCase(), obj, Language);
            FileInfo file = new FileInfo(outputFile);
            StringBuilder output = new StringBuilder();
            StringBuilder properties = new StringBuilder();
            Info.Properties.Each(bp =>
            {
                properties.Append(Format.PropertyFormat.NamedFormat(new
                {
                    PropertyType = bp.PropertyType,
                    PropertyField = bp.PropertyField,
                    ClassName = bp.ClassName,
                    PropertyName = bp.PropertyName
                }));
            });

            output.Append(Format.ClassFormat.NamedFormat(new { ClassName = Info.ClassName, Properties = properties.ToString() }));

            output.ToString().SafeWriteToFile(file.FullName);
        }

        public BreveFormat Format { get; set; }

        public static BreveGenerator Create(Type type, BreveInfo info)
        {
            return new BreveGenerator(info) { Format = type.Construct<BreveFormat>() };
        }

        public static BreveGenerator Create<T>(BreveInfo info) where T: BreveFormat, new()
        {
            BreveGenerator generator = new BreveGenerator(info)
            {
                Format = new T()
            };
            return generator;
        }
    }
}
