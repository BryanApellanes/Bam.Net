using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Incubation
{
    public class FluentCtorContext<I>
    {
        public FluentCtorContext(Incubator inc, string parametrerName)
        {
            Incubator = inc;
            ParameterName = parametrerName;
        }
        public Incubator Use(object value)
        {
            Incubator inc = Incubator ?? new Incubator();
            inc.SetCtorParam(typeof(I), ParameterName, value);
            return inc;
        }
        protected string ParameterName { get; set; }
        protected Incubator Incubator
        {
            get;
            set;
        }
    }
}
