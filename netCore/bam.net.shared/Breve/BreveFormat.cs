using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Breve
{
    public class BreveFormat
    {
        public BreveFormat()
        {
            ClassFormat = @"public class {ClassName}{{
{Properties}
}}";
            PropertyFormat = @"
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
        public string ClassFormat { get; set; }
        public string PropertyFormat { get; set; }
    }
}
