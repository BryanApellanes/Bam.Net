/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bam.Net;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
	public class PagedQuery<C, T>
		where C : QueryFilter, IFilterToken, new()
		where T : Dao, new()
	{
		public PagedQuery(C orderByColumn, Query<C, T> query, Database db = null)
		{
			this.Database = db;
			this.OrderByColumn = orderByColumn;
			this.SortOrder = SortOrder.Ascending;
			this.Query = query;
			this.PageSize = 5;
			this.CurrentPage = -1;
		}

		public C OrderByColumn { get; set; }
		public SortOrder SortOrder { get; set; }
		public int PageCount
		{
			get
			{
				return IdBook.PageCount;
			}
		}

		/// <summary>
		/// The number of records per page, default is 5
		/// </summary>
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
		public Query<C, T> Query { get; set; }
		Database _database;
		public Database Database
		{
			get
			{
				if (_database == null)
				{
					_database = Db.For<T>();
				}

				return _database;
			}
			set
			{
				_database = value;
			}
		}

		public bool NextPage(out IEnumerable<T> results)
		{
			LoadMeta();

			if (IdBook.PageCount >= CurrentPage + 1)
			{
				++CurrentPage;
				List<long> ids = IdBook.PageNumber(CurrentPage);
				QuerySet sql = Database.GetService<QuerySet>();
				sql.Top<T>(PageSize);
				QueryFilter queryFilter = (QueryFilter)Query.FilterDelegate.DynamicInvoke(OrderByColumn);
				sql.Where(queryFilter && new QueryFilter("Id").In(ids.Select(i => (object)i).ToArray())).OrderBy(OrderByColumn.ToString(), SortOrder);
				CurrentResults = new DaoCollection<C, T>(Database, sql.GetDataTable(Database));
			}
			SetLastEntry();
			results = CurrentResults;
			return CurrentResults.Count > 0;
		}
		private void SetLastEntry()
		{
			if (CurrentResults.Count > 0)
			{
				LastEntry = CurrentResults[CurrentResults.Count - 1];
			}
			else
			{
				LastEntry = null;
			}
		}

		public DaoCollection<C, T> CurrentResults
		{
			get;
			private set;
		}

		public long[] Ids { get; set; }

		public T LastEntry { get; set; }		

		protected Book<long> IdBook { get; set; }

		bool _metaLoaded;
		protected internal void LoadMeta()
		{
			if (!_metaLoaded)
			{
				string id = "Id";
				SqlStringBuilder sql = GetBaseIdQuery();
				DataTable table = sql.GetDataTable(Database);
				List<long> ids = new List<long>();
				foreach(DataRow row in table.Rows)
				{
					ids.Add(Database.GetLongValue(id, row).Value);
				}
				Ids = ids.ToArray();
				IdBook = new Book<long>(Ids, PageSize);

                _metaLoaded = true;
            }
		}

		private SqlStringBuilder GetBaseIdQuery()
		{
			// get the ids for the specified query
			SqlStringBuilder sql = Database.GetService<SqlStringBuilder>();
            sql.Select(Dao.TableName(typeof(T)), sql.ColumnNameFormatter(Dao.GetKeyColumnName(typeof(T))));
			SetQuery(sql);
			return sql;
		}

		private void SetQuery(SqlStringBuilder sql)
		{
			Args.ThrowIfNull(OrderByColumn, "OrderByColumn");
			IQueryFilter queryFilter = (IQueryFilter)Query.FilterDelegate.DynamicInvoke(OrderByColumn);
			sql = sql.Where(queryFilter).OrderBy(OrderByColumn.ToString(), SortOrder);
		}
	}
	
}
