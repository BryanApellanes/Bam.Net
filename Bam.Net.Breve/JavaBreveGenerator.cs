/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Breve
{
    public class JavaBreveGenerator: BreveGenerator
    {
        public JavaBreveGenerator(BreveInfo breve)
            : base(breve)
        {
            Format = new BreveJavaFormat();
        }

        public override void Go(string outputFile)
        {
            FileInfo file = new FileInfo(outputFile);
            StringBuilder output = new StringBuilder();
            StringBuilder properties = new StringBuilder();
            Info.Properties.Each(bp =>
            {
                properties.Append(Format.PropertyFormat.NamedFormat(new { 
                    PropertyType = bp.PropertyType, 
                    PropertyField = bp.PropertyField,  
                    ClassName = bp.ClassName,
                    PropertyName = bp.PropertyName
                }));
            });

            output.Append(Format.ClassFormat.NamedFormat(new { ClassName = Info.ClassName, Properties = properties.ToString() }));

            output.ToString().SafeWriteToFile(file.FullName);
        }
    }
}
