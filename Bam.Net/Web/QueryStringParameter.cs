/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;

namespace Bam.Net.Web
{
    public static class QueryStringParameterExtensions
    {
        public static QueryStringParameter[] ToQueryStringParameters(this string[] parameters)
        {
            List<QueryStringParameter> retVals = new List<QueryStringParameter>();
            if (parameters.Length == 1 && string.IsNullOrEmpty(parameters[0]))
                return retVals.ToArray();

            foreach (string parameter in parameters)
            {
                string[] each = parameter.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (each.Length != 2)
                {
                    throw new InvalidOperationException("The specified string was not a valid QueryStringParameter array");
                }
                retVals.Add(new QueryStringParameter { Name = each[0], Value = each[1] });
            }

            return retVals.ToArray();
        }

        public static string ToQueryString(this QueryStringParameter[] parameters)
        {
            string ret = "";
            for(int i = 0 ; i < parameters.Length; i++)
            {
                QueryStringParameter parm = parameters[i];
                ret += parm.Name + "=" + parm.Value;
                if (i != parameters.Length - 1)
                    ret += "&";
            }

            return ret;
        }
    }

    public class QueryStringParameter
    {
        public QueryStringParameter() { }
        public QueryStringParameter(string name, string value) { this.Name = name; this.Value = value; }
        public string Name { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return this.Name + "=" + this.Value;
        }
    }
}
