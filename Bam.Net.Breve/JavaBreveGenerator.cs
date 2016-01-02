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
        string _classFormat;
        string _propertyFormat;
        public JavaBreveGenerator(BreveInfo breve)
            : base(breve)
        {
            this._classFormat = @"public class {ClassName}{{
{Properties}
}}";
            this._propertyFormat = @"
    {PropertyType} {PropertyField};
    public {PropertyType} {PropertyName}(){{
        return {PropertyField};
    }}

    public {ClassName} {PropertyName}({PropertyType} value){{
        {PropertyField} = value;
        return this;
    }}
";
        }

        public override void Go(string outputFile)
        {
            FileInfo file = new FileInfo(outputFile);
            StringBuilder output = new StringBuilder();
            StringBuilder properties = new StringBuilder();
            Info.Properties.Each(bp =>
            {
                properties.Append(_propertyFormat.NamedFormat(new { 
                    PropertyType = bp.PropertyType, 
                    PropertyField = bp.PropertyField,  
                    ClassName = bp.ClassName,
                    PropertyName = bp.PropertyName
                }));
            });

            output.Append(_classFormat.NamedFormat(new { ClassName = Info.ClassName, Properties = properties.ToString() }));

            output.ToString().SafeWriteToFile(file.FullName);
        }
    }
}
