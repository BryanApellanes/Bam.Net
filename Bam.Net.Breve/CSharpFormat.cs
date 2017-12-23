using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Breve
{
    public class CSharpFormat : BreveFormat
    {
        public CSharpFormat()
        {
            ClassFormat = @"public class {ClassName}{{
{Properties}
}}";
            PropertyFormat = @"
    public {PropertyType} {PropertyName}
    {{
        get;
        set;
    }}

    public {ClassName} Prop{PropertyName}({PropertyType} value){{
        {PropertyName} = value;
        return this;
    }}
";
        }
    }
}
