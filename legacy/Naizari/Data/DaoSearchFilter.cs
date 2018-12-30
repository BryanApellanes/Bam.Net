/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Naizari.Data
{
    public class DaoSearchFilter: DaoSearchToken
    {
        protected List<DaoSearchToken> searchParameters;
        List<DbParameter> dbParameters;

        public DaoSearchFilter()
        {
            searchParameters = new List<DaoSearchToken>();
            this.WhereAppender = WhereAppender.AND;
            this.IncludeOutterParens = false;
            this.dbParameters = new List<DbParameter>();
        }

        bool useJulianDates;
        public bool UseJulianDates 
        {
            get
            {
                return this.useJulianDates;
            }
            set
            {
                this.useJulianDates = value;
                foreach (DaoSearchToken token in this.searchParameters)
                {
                    if (token is DaoSearchParameter)
                        ((DaoSearchParameter)token).UseJulianDates = value;
                }
            }
        }
        
        public void AddParameter(Enum columnName, object value)
        {
            this.AddParameter(columnName.ToString(), value);
        }

        public void AddParameter(string columnName, object value)
        {
            AddParameter(columnName, value, Comparison.Equals);
        }

        public void AddParameter<T>(Enum columnName, object value) where T : DaoObject, new()
        {
            this.AddParameter<T>(columnName, value, Comparison.Equals);
        }

        public void AddParameter<T>(Enum columnName, object value, Comparison whereOperator) where T : DaoObject, new()
        {
            this.AddParameter(DatabaseAgent.EnumToColumnName<T>(columnName), value, whereOperator);
        }

        public void AddParameter(Enum columnName, object value, Comparison whereOperator)
        {
            this.AddParameter(columnName.ToString(), value, whereOperator);
        }

        public void AddParameter<T>(object columName, object value, Comparison compare) where T : DaoObject, new()
        {
            this.AddParameter<T>((Enum)columName, value, compare);
        }
        
        public void AddParameter(string columnName, object value, Comparison whereOperator)
        {
            this.AddParameter(columnName, value, whereOperator, WhereAppender.AND);
        }

        /// <summary>
        /// This method should not be used if it can be avoided.  Use an
        /// equivalent Field enum method instead.  
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="whereOperator"></param>
        public void AddParameter(string columnName, object value, Comparison whereOperator, WhereAppender appender)
        {
            if (appender == Data.WhereAppender.None)
                appender = Data.WhereAppender.AND;
            DaoSearchParameter param = new DaoSearchParameter(appender);
            param.Comparison = whereOperator;
            param.SelectParameter = new DbSelectParameter(columnName, value);
            AddParameter(param);
        }

        WhereAppender next = WhereAppender.AND;
        private void AddParameter(DaoSearchParameter parameter)
        {
            dbParameters.Add(parameter.SelectParameter.DbParameter);
            
            if (searchParameters.Count > 0)
            {
                searchParameters.Add(new DaoWhereAppenderSearchToken(next));
                
            }
            next = parameter.WhereAppender;
            searchParameters.Add(parameter);
        }

        public DbParameter[] DbParameters
        {
            get
            {
                return dbParameters.ToArray();
            }
        }

        protected internal virtual void AppendFilter(Enum columnName, object value, Comparison whereOperator, WhereAppender appender)
        {
            this.AppendFilter(columnName.ToString(), value, whereOperator, appender);
        }

        private void AppendFilter(string columnName, object value, Comparison whereOperator, WhereAppender appender)
        {
            DaoSearchFilter toAdd = new DaoSearchFilter();
            toAdd.AddParameter(columnName, value, whereOperator);
            this.AppendFilter(toAdd, appender);
        }

        public virtual void AppendFilter(DaoSearchFilter filter, WhereAppender appender)
        {
            if (filter == this)
                throw new InvalidOperationException("DaoSearchFilter cannot be added to itself");
            if (appender == WhereAppender.None)
                throw new InvalidOperationException("appender cannot be WhereAppender.None");

            searchParameters.Insert(0, new DaoOpenParen());
            searchParameters.Add(new DaoCloseParen());
            searchParameters.Add(new DaoWhereAppenderSearchToken(appender));
            filter.IncludeOutterParens = true;
            dbParameters.AddRange(filter.dbParameters);
            searchParameters.Add(filter);
        }

        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();

            for(int i = 0; i < searchParameters.Count; i++)
            {
                DaoSearchToken token = searchParameters[i];
        
                returnString.Append(token.ToString());
                
            }

            if (IncludeOutterParens)
                return "(" + returnString.ToString() + ")";
            else
                return returnString.ToString();
        }

        private bool IncludeOutterParens { get; set; }
    }

    
}
