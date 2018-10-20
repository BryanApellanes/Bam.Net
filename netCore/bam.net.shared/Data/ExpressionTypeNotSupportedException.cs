using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class ExpressionTypeNotSupportedException: Exception
    {
        public ExpressionTypeNotSupportedException(ExpressionType expressionType) : 
            base($"Unsupported NodeType ({expressionType.ToString()}): for conditionals use QueryFilter.Or() and QueryFilter.And()")
        { }
    }
}
