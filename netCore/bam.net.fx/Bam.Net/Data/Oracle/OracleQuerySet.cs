/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using Bam.Net.Data.Oracle;

namespace Bam.Net.Data
{
	public class OracleQuerySet : QuerySet, IPLSqlStringBuilder
    {
		public OracleQuerySet(): base()
		{
			GoText = "\r\n";
			TableNameFormatter = (t) => string.Format("\"{0}\"", t.ToUpperInvariant());
			ColumnNameFormatter = (c) => c;
		}

		public override SqlStringBuilder Id(string idAs)
		{
			StringBuilder.AppendFormat(" RETURNING Id INTO :{0}", idAs);
			GoText = "\r\n";
			ReturnsId = true;
			return this;
		}

		public override void Reset()
		{
			base.Reset();
			this.GoText = "\r\n";
		}

		public bool ReturnsId
		{
			get;
			set;
		}

        public int TopCount
        {
            get;
            set;
        }

		public OracleParameter IdParameter
		{
			get;
			set;
		}

        public override SqlStringBuilder SelectTop(int topCount, string tableName, params string[] columnNames)
        {
            this.TopCount = topCount;

            if (columnNames.Length == 0)
            {
                columnNames = new string[] { "*" };
            }

            string cols = columnNames.ToDelimited(s => ColumnNameFormatter(s));
            StringBuilder.AppendFormat("SELECT {0} FROM {1} ", cols, GetFormattedTableName(tableName));
            return this;
        }
		public override SqlStringBuilder Update(string tableName, params AssignValue[] values)
		{
			Builder.AppendFormat("UPDATE {0} ", GetFormattedTableName(tableName));
			SetFormat set = OracleFormatProvider.GetSetFormat(tableName, StringBuilder, NextNumber, values);
			NextNumber = set.NextNumber;
			this.parameters.AddRange(set.Parameters);
			return this;
		}

		public override SqlStringBuilder Where(IQueryFilter filter)
		{
			WhereFormat where = OracleFormatProvider.GetWhereFormat(filter, StringBuilder, NextNumber);
			NextNumber = where.NextNumber;
			this.parameters.AddRange(where.Parameters);
			return this;
		}


		public override SqlStringBuilder Where(AssignValue filter)
		{
			WhereFormat where = OracleFormatProvider.GetWhereFormat(filter, StringBuilder, NextNumber);
			NextNumber = where.NextNumber;
			this.parameters.AddRange(where.Parameters);
			return this;
		}
	
		public override SqlStringBuilder Insert(Dao instance)
		{
			ResultDataTables.Add(new InsertResult(instance, "Id"));
			return Insert(Dao.TableName(instance.GetType()), instance.GetNewAssignValues()).Id().Go();			
		}

		public override SqlStringBuilder Insert(string tableName, params AssignValue[] values)
		{
			Builder.AppendFormat("INSERT INTO {0} ", GetFormattedTableName(tableName));
			InsertFormat insert = new InsertFormat { ParameterPrefix = ":" };
			insert.ColumnNameFormatter = (c) => c;
			foreach (AssignValue value in values)
			{
				insert.AddAssignment(value);
			}

			insert.StartNumber = NextNumber;
			Builder.Append(insert.Parse());
			NextNumber = insert.NextNumber;
			this.parameters.AddRange(insert.Parameters);
			return this;
		}

		public override void Execute(Database db)
		{
			if (TopCount > 0)
			{
				Go();
			}
			OracleDatasetProvider dsp = new OracleDatasetProvider(this);
			DataSet = dsp.GetDataSet(db);
			OnExecuted(db);
			Reset();
		}

		public override SqlStringBuilder Go()
		{
			if (this.TopCount > 0)
			{
				StringBuilder.AppendFormat(" AND ROWNUM <= {0} ", this.TopCount);
			}

			base.Go();
			this.TopCount = -1;
			return this;
		}

		protected internal override void SubscribeToExecute()
		{
			this.Executed += (o, e) =>
			{
				IHasDataTable dt = ResultDataTables.FirstOrDefault();
				if(ResultDataTables.Count > 0)
				{
					ResultDataTables.Each((result, i) =>
					{
						InsertResult insertResult = result as InsertResult;
						if (insertResult != null)
						{
							long id = long.Parse(IdParameter.Value.ToString());
							insertResult.Value.Property("Id", id);
						}
						else
						{
							result.SetDataTable(DataSet.Tables[i]);
						}
					});
				}
			};
		}

		private string GetFormattedTableName(Type daoType)
		{
			string tableName = daoType.GetCustomAttributeOfType<TableAttribute>().TableName;
			return GetFormattedTableName(tableName);
		}

		private string GetFormattedTableName(string tableName)
		{
			return TableNameFormatter(GetFirstThirtyCharacters(tableName));
		}

		private static string GetFirstThirtyCharacters(string tableName)
		{
			if (tableName.Length > 30)
			{
				tableName = tableName.Substring(0, 30);
			}
			return tableName;
		}
    }
}
