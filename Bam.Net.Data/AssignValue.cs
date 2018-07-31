/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net.Data
{
    /// <summary>
    /// Statement used to assign a value to a variable or parameter
    /// </summary>
    public class AssignValue: IParameterInfo
    {
        public AssignValue(string columnName, object value, Func<string, string> columnNameformatter = null)
        {
            this.ColumnName = columnName;
            this.Value = value;
            this.number = new int?();
            this.ColumnNameFormatter = columnNameformatter ?? (Func<string, string>)((c) => c);
			this.ParameterPrefix = "@";
        }

		public Func<string, string> ColumnNameFormatter { get; set; }
		public string ParameterPrefix { get; set; }
        public string ColumnName
        {
            get;
            set;
        }

        int? number;
        public int? Number
        {
            get { return number; }
            set
            {
                number = value;
            }
        }

        public int? SetNumber(int? value)
        {
            number = value;
            return ++value;
        }

        public object Value
        {
            get;
            set;
        }

        public string Operator
        {
            get
            {
                return "=";
            }
            set { }
        }        

        public override string ToString()
        {
            return string.Format("{0} {1} {2} ", ColumnNameFormatter(ColumnName), this.Operator, string.Format("{0}{1}{2}", ParameterPrefix, ColumnName, Number));
        }

        public static IEnumerable<AssignValue> FromDynamic(dynamic obj, Func<string, string> columnNameFormatter = null)
        {
            Args.ThrowIfNull(obj, "obj");
            Type type = obj.GetType();
            foreach(PropertyInfo prop in type.GetProperties())
            {
                yield return new AssignValue(prop.Name, prop.GetValue(obj), columnNameFormatter);
            }
        }

        public static IEnumerable<AssignValue> FromDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Func<string, string> columnNameFormatter = null)
        {
            Args.ThrowIfNull(dictionary);
            foreach(TKey key in dictionary.Keys)
            {
                yield return new AssignValue(key.ToString(), dictionary[key]?.ToString(), columnNameFormatter);
            }
        }
    }
}
