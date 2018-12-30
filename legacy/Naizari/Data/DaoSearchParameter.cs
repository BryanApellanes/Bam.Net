/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;
using System.Data.Common;

namespace Naizari.Data
{
    internal class DaoSearchParameter: DaoSearchToken
    {
        public DaoSearchParameter() 
        {
            this.WhereAppender = WhereAppender.AND;
        }

        public DaoSearchParameter(WhereAppender appender)
        {
            this.WhereAppender = appender;
        }

        public DaoSearchParameter(DbSelectParameter selectParameter, Comparison whereOperator)
            : this()
        {
            Comparison = whereOperator;
            SelectParameter = selectParameter;
            this.dateValue = selectParameter.Value is DateTime ? selectParameter.Value: null;
            this.julianValue = selectParameter.Value is DateTime ? (object)JulianDate.ToJulianDate((DateTime)this.dateValue) : null;
        }

        object dateValue;
        object julianValue;
        bool useJulianDates;
        public bool UseJulianDates
        {
            get
            {
                return useJulianDates;
            }
            set
            {
                useJulianDates = value;
                if (value && dateValue != null)
                {
                    SelectParameter.Value = julianValue;
                }
                else if(dateValue != null)
                {
                    SelectParameter.Value = dateValue;
                }
            }
        }
        public Comparison Comparison { get; set; }
        public DbSelectParameter SelectParameter { get; set; }

        public override string ToString()
        {
            return string.Format(" {0} {1} ", SelectParameter.ColumnName, GetSql(Comparison, SelectParameter.DbParameter));
        }

        private string GetSql(Comparison whereOperator, DbParameter value)
        {
            bool isInt = value.Value is int || value.Value is long;
            bool isDateTime = value.Value is DateTime;
            if (value.Value == null)
                value.Value = DBNull.Value;

            if (value.Value == DBNull.Value &&
                whereOperator != Comparison.Equals &&
                whereOperator != Comparison.NotEqualTo)
            {
                throw new InvalidOperationException(string.Format(
                    "Invalid Comparison '{0}': Valid Comparisons for null are 'Equals' and 'NotEqualTo'.",
                    whereOperator.ToString()));
            }

            switch (whereOperator)
            {
                case Comparison.Equals:
                    if (value.Value == DBNull.Value)
                        return "IS NULL";

                    return string.Format("= {0}", value.ParameterName);// : string.Format("= '{0}'", value.ToString());
                case Comparison.NotEqualTo:
                    if (value.Value == DBNull.Value)
                        return "IS NOT NULL";

                    return string.Format("<> {0}", value.ParameterName);// : string.Format("<> '{0}'", value.ToString());
                case Comparison.StartsWith:
                    return string.Format("LIKE {0} + '%'", value.ParameterName);
                case Comparison.EndsWith:
                    return string.Format("LIKE '%' + {0}", value.ParameterName);
                case Comparison.Contains:
                    return string.Format("LIKE '%' + {0} + '%'", value.ParameterName);
                case Comparison.DoesntStartWith:
                    return string.Format("NOT LIKE {0} + '%'", value.ParameterName);
                case Comparison.DoesntEndWith:
                    return string.Format("NOT LIKE '%' + {0}", value.ParameterName);
                case Comparison.DoesntContain:
                    return string.Format("NOT LIKE '%' + {0} + '%'", value.ParameterName);
                case Comparison.GreaterThan:
                    return string.Format("> {0}", value.ParameterName);
                case Comparison.GreaterThanOrEqualTo:
                    return string.Format(">= {0}", value.ParameterName);
                case Comparison.LessThan:
                    return string.Format("< {0}", value.ParameterName);//value.Value.ToString());
                case Comparison.LessThanOrEqualTo:
                    return string.Format("<= {0}", value.ParameterName);//value.Value.ToString());
            }

            return string.Empty;
        }

    }
}
