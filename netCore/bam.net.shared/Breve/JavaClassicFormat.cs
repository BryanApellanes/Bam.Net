using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Breve
{
    public class JavaClassicFormat : BreveFormat
    {
        public JavaClassicFormat()
        {
            ClassFormat = @"public class {ClassName}{{
{Properties}
}}";
            PropertyFormat = @"
    {PropertyType} {PropertyField};
    public {PropertyType} get{PropertyName}(){{
        return {PropertyField};
    }}

    public void set{PropertyName}({PropertyType} value){{
        {PropertyField} = value;
    }}
";
        }
    }
}

