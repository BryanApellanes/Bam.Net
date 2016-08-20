/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;
using System.Data;
using System.Data.Common;

namespace Bam.Net.Data.Qi
{
    public class QiClause: IParameterInfo
    {
        public QiClause()
        {
            property = string.Empty;
            val = string.Empty;
			ColumnNameFormatter = (c) => c;
			ParameterPrefix = string.Empty;
        }
        public bool hasValue { get; set; }
        public int num { get; set; }
        public string operator_ { get; set; }
        public string parameterName { get; set; }
        public object val { get; set; }
        public string property { get; set; }

        public QiOperator QiOperator
        {
            get
            {
                return (QiOperator)Enum.Parse(typeof(QiOperator), operator_);
            }
        }

        protected string ResolveOperator()
        {
            string val = "=";
            switch (QiOperator)
            {
                case QiOperator.Invalid:
                    break;
                case QiOperator.Equals:
                    break;
                case QiOperator.NotEqualTo:
                    val = "!=";
                    break;
                case QiOperator.GreaterThan:
                    val = ">";
                    break;
                case QiOperator.LessThan:
                    val = "<";
                    break;
                case QiOperator.StartsWith:
                    val = "LIKE";
                    break;
                case QiOperator.DoesntStartWith:
                    val = "NOT LIKE";
                    break;
                case QiOperator.EndsWith:
                    val = "LIKE";
                    break;
                case QiOperator.DoesntEndWith:
                    val = "NOT LIKE";
                    break;
                case QiOperator.Contains:
                    val = "LIKE";
                    break;
                case QiOperator.DoesntContain:
                    val = "LIKE";
                    break;
                case QiOperator.OpenParen:
                    val = "(";
                    break;
                case QiOperator.CloseParen:
                    val = ")";
                    break;
                case QiOperator.AND:
                    val = "AND";
                    break;
                case QiOperator.OR:
                    val = "OR";
                    break;
                default:
                    break;
            }

            return val;
        }

        public Comparison ToComparison()
        {
            Comparison val = new Comparison(this.property, this.ResolveOperator(), this.val, this.num);
            if (this.hasValue)
            {
                string oper = this.ResolveOperator();
                if (oper.Equals("LIKE") || oper.Equals("NOT LIKE"))
                {
                    if (QiOperator == QiOperator.StartsWith)
                    {
                        val = new StartsWithComparison(property, this.val, this.num);
                    }
                    else if (QiOperator == QiOperator.EndsWith)
                    {
                        val = new EndsWithComparison(property, this.val, this.num);
                    }
                    else if (QiOperator == QiOperator.Contains)
                    {
                        val = new ContainsComparison(property, this.val, this.num);
                    }
                    else if (QiOperator == QiOperator.DoesntStartWith)
                    {
                        val = new DoesntStartWithComparison(property, this.val, this.num);
                    }
                    else if (QiOperator == QiOperator.DoesntEndWith)
                    {
                        val = new DoesntEndWithComparison(property, this.val, this.num);
                    }
                    else if (QiOperator == QiOperator.DoesntContain)
                    {
                        val = new DoesntContainComparison(property, this.val, this.num);
                    }
                }
            }

            return val;
        }


        #region IParameterInfo Members

		public Func<string, string> ColumnNameFormatter { get; set; }
		public string ParameterPrefix { get; set; }
        public string ColumnName
        {
            get
            {
                return this.property;
            }
            set
            {
                //
            }
        }

        public int? Number
        {
            get
            {
                return this.num;
            }
            set
            {
            }
        }

        public int? SetNumber(int? value)
        {
            return this.num + 1;
        }

        public object Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value == null ? "null": value.ToString();
            }
        }

        #endregion

        #region IFilterToken Members

        public string Operator
        {
            get
            {
                return this.ResolveOperator();
            }
            set
            {
                //
            }
        }

        #endregion
    }

}
