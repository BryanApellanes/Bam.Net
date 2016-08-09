using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class InsertInheritanceFormat : SetFormat
    {
        public override string Parse()
        {
            AssignNumbers();
            string idColumn = ColumnNameFormatter("Id");
            string idVariable = $"{ParameterPrefix}Id";
            string columns = this.Parameters.ToArray().ToDelimited(p => ColumnNameFormatter(p.ColumnName));
            string values = this.Parameters.ToArray().ToDelimited(p => string.Format("{0}{1}{2}", ParameterPrefix, p.ColumnName, p.Number));
            return string.Format("({0}, {1}) VALUES ({2}, {3})",
                idColumn,
                columns,
                idVariable,
                values
            );

        }
    }
}
