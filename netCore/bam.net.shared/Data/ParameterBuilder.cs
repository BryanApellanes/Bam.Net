/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Bam.Net.Data;
using Bam.Net.Incubation;
using System.Data.SqlClient;

namespace Bam.Net.Data
{
    public abstract class ParameterBuilder: IParameterBuilder
    {
        public abstract DbParameter BuildParameter(string name, object value);
        public abstract DbParameter BuildParameter(IParameterInfo c);

        public DbParameter[] BuildParamters(InComparison c)
        {
            DbParameter[] results = new DbParameter[c.Parameters.Length];

            for (int i = 0; i < c.Parameters.Length; i++)
            {
                results[i] = BuildParameter(c.Parameters[i]);
            }

            return results;
        }

        #region IParameterBuilder<T> Members

        public DbParameter[] GetParameters(IHasFilters filter)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (IFilterToken token in filter.Filters)
            {
                if (token is IParameterInfo c)
                {
                    if (c is InComparison inC)
                    {
                        parameters.AddRange(BuildParamters(inC));
                    }
                    else
                    {
                        parameters.Add(this.BuildParameter(c));
                    }
                }
            }

            return parameters.ToArray();
        }

        #endregion
    }
}
