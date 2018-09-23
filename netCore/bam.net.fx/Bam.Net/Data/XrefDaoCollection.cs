/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Bam.Net;

namespace Bam.Net.Data
{
    /// <summary>
    /// A collection that represents a cross reference between its
    /// parents and the table represented by L.
    /// </summary>
    /// <typeparam name="X">The Xref type</typeparam>
    /// <typeparam name="L">The list type</typeparam>
    public class XrefDaoCollection<X, L> : PagedEnumerator<L>, IEnumerable<L>, ILoadable, IHasDataTable, IAddable
        where X : Dao, new()
        where L : Dao, new()
    {
        List<L> _values;
        Book<L> _book;

        public XrefDaoCollection(Dao parent, bool load = true)
        {
            Parent = parent;
            _values = new List<L>();
            _book = new Book<L>();
            if (parent != null && !parent.IsNew)
            {
                Database = parent?.Database;

                if (load && Database != null)
                {
                    Load(Database);
                }
            }
            _setDatabases = true;
        }

        protected Dao Parent
        {
            get;
            set;
        }

        protected string ParentColumnName
        {
            get
            {
                return string.Format("{0}Id", Dao.TableName(Parent));
            }
        }

        protected string ListColumnName
        {
            get
            {
                return string.Format("{0}Id", Dao.TableName(typeof(L)));
            }
        }

        protected Dictionary<ulong, X> XrefsByListId
        {
            get;
            set;
        }

        public bool Loaded
        {
            get
            {
                return _loaded;
            }
        }

        bool _loaded;
        public void Reload()
        {
            _loaded = false;
            Load();
        }

        Database _database;
        public Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Db.For<L>();
                }

                return _database;
            }
            set
            {
                _database = value;
                SetEachDatabase(_database);
            }
        }

        public void Load()
        {
            Load(Database);
        }

        object _loadLock = new object();
        public void Load(Database db)
        {
            if (!_loaded && Parent != null)
            {
                lock (_loadLock)
                {
                    if (!_loaded)
                    {
                        XrefsByListId = new Dictionary<ulong, X>();

                        QuerySet q = Dao.GetQuerySet(db);
                        q.Select<X>().Where(new AssignValue(ParentColumnName, Parent.IdValue, q.ColumnNameFormatter));
                        q.Execute(db);

                        // should have all the ids of L that should be retrieved
                        if (q.Results[0].DataTable.Rows.Count > 0)
                        {
                            List<ulong> ids = new List<ulong>();

                            foreach (DataRow row in q.Results[0].DataTable.Rows)
                            {
                                ulong id = Convert.ToUInt64(row[ListColumnName]);
                                ids.Add(id);
                                X xref = new X
                                {
                                    DataRow = row
                                };
                                XrefsByListId.Add(id, xref);
                            }

                            QuerySet q2 = Dao.GetQuerySet(db);
                            QueryFilter filter = new QueryFilter(Dao.GetKeyColumnName<L>());
                            filter.In(ids.Select(i => (object)i).ToArray(), db.ParameterPrefix);
                            q2.Select<L>().Where(filter);
                            q2.Execute(db);

                            Initialize(q2.Results[0].DataTable, db);
                        }

                        _loaded = true;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                return _book.ItemCount;
            }
        }

        public void Save()
        {
            Commit();
        }

        public void Save(Database db)
        {
            Commit(db);
        }

        /// <summary>
        /// Adds a new value to the collection in memory.  Does
        /// not commit to the database until Save is called.
        /// </summary>
        /// <returns></returns>
        public L AddNew()
        {
            L val = new L();
            Add(val);
            return val;
        }

        /// <summary>
        /// Sets the specified item's Database property to that of the current
        /// XrefDaoCollection and adds the value to the collection in memory.  Does
        /// not commit to the database until Save is called.
        /// </summary>
        /// <param name="item"></param>
        public void Add(L item)
        {
            item.Database = Database;
            _values.Add(item);
            _book = new Book<L>(_values);
        }

        /// <summary>
        /// Sets the Database property of each value specified and adds them to the 
        /// XrefDaoCollection in memory.  Does not commit to the database until Save is
        /// called.
        /// </summary>
        /// <param name="values"></param>
        public void AddRange(IEnumerable<L> values)
        {
            values.Each(v => v.Database = Database);
            _values.AddRange(values);
            _book = new Book<L>(_values);
        }

        public L this[int index]
        {
            get
            {
                return _values[index];
            }
        }

        /// <summary>
        /// Removes the specified item from this collection, deletes the xref entry but
        /// does not delete the item from the database
        /// </summary>
        /// <param name="item"></param>
        public void Remove(L item, Database db = null)
        {
            if (_values.Contains(item))
            {
                _values.Remove(item);
                _book = new Book<L>(_values);
            }

            DeleteXrefItem(item, db);
        }

        private void DeleteXrefItem(L item, Database db)
        {
            if (XrefsByListId.ContainsKey(item.IdValue.Value))
            {
                XrefsByListId[item.IdValue.Value].Delete(db);
            }
        }

        /// <summary>
        /// Deletes all cross reference entries representing associations
        /// for the objects in this collection.  The objects themselves
        /// are not deleted.
        /// </summary>
        /// <param name="db"></param>
        public void Clear(Database db = null)
        {
            db = db ?? Database;
            foreach(L item in _values)
            {
                DeleteXrefItem(item, db);
            }
            _values = new List<L>();
            _book = new Book<L>();
        }

        public override bool MoveNextPage()
        {
            CurrentPageIndex++;
            if (CurrentPageIndex >= _book.PageCount)
            {
                return false;
            }

            this.CurrentPage = this._book[CurrentPageIndex];
            return true;
        }

        private void Initialize(DataTable table, Database db = null)
        {
            db = db ?? Database;
            ConstructorInfo _ctor = typeof(L).GetConstructor(new Type[] { typeof(Database), typeof(DataRow) });
            _values = new List<L>();
            foreach (DataRow row in table.Rows)
            {
                L dao = (L)_ctor.Invoke(new object[] { db, row });
                _values.Add(dao);
            }
            _book = new Book<L>(_values);
            DataTable = table;
        }

        #region IEnumerable<L> Members

        public IEnumerator<L> GetEnumerator()
        {
            Load();
            return this;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            Load();
            return this;
        }

        #endregion

        #region ICommittable Members

        public void Commit()
        {
            Commit(Database);
        }

        public event ICommittableDelegate AfterCommit;
        public void Commit(Database db = null)
        {
            db = db ?? Database;
            SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
            WriteCommit(sql, db);

            sql.Execute(db);
            AfterCommit?.Invoke(db, this);
        }

        public void WriteCommit(SqlStringBuilder sql, Database db = null)
        {
            db = db ?? Database;
            List<L> children = new List<L>();
            foreach (L item in this._values)
            {
                EnsureXref(item, db);
                item.WriteCommit(sql, db);
                children.Add(item);
            }

            sql.Executed += (s, d) =>
            {
                children.Each(dao => dao.OnAfterCommit(d));
                AfterCommit?.Invoke(d, this);
            };
        }

        private X EnsureXref(L item, Database db = null)
        {
            db = db ?? Database;
            if (item.IdValue != null && XrefsByListId.ContainsKey(item.IdValue.Value))
            {
                return XrefsByListId[item.IdValue.Value];
            }
            else
            {
                if (item.IsNew)
                {
                    item.Save(db);
                }

                X result = null;
                QuerySet q = Dao.GetQuerySet(db);
                q.Select<X>().Where(new QueryFilter(ListColumnName) == item.IdValue.Value && new QueryFilter(ParentColumnName) == Parent.IdValue);

                q.Execute(db);
                if (q.Results[0].DataTable.Rows.Count > 0)
                {
                    result = new X();
                    result.DataRow = q.Results[0].DataTable.Rows[0];
                }
                else
                {
                    result = new X();
                    result.SetValue(string.Format("{0}Id", Parent.GetType().Name), Parent.IdValue);
                    result.SetValue(string.Format("{0}Id", typeof(L).Name), item.IdValue);
                    result.Save(db);

                    XrefsByListId.Add(item.IdValue.Value, result);
                }

                return result;
            }
        }

        #endregion

        #region IDeleteable Members

        /// <summary>
        /// Delete all the entries in this collection as 
        /// well as all Xref entries if any.
        /// </summary>
        /// <param name="db"></param>
        public void Delete(Database db = null)
        {
            db = db ?? Database;
            SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
            WriteDelete(sql);
            sql.Execute(db);
        }

        /// <summary>
        /// Write a sql script that can be used to delete all 
        /// the entries in this collection as well as all the
        /// Xref entries if any.
        /// </summary>
        /// <param name="sql"></param>
        public void WriteDelete(SqlStringBuilder sql)
        {
            foreach (L item in this._values)
            {
                if (item.IsNew)
                {
                    _values.Remove(item);
                }
                else
                {
                    item.WriteDelete(sql);
                    sql.Go();
                    XrefsByListId[item.IdValue.Value].WriteDelete(sql);
                }
                sql.Go();
            }
        }

        #endregion

        #region IHasDataTable Members

        public DataTable DataTable
        {
            get;
            private set;
        }

        public void SetDataTable(DataTable table)
        {
            Initialize(table, Database);
        }

        public T As<T>() where T : IHasDataTable, new()
        {
            T val = new T();
            val.SetDataTable(this.DataTable);
            return val;
        }

        #endregion

        #region IHasDataRow Members

        public DataRow DataRow
        {
            get
            {
                return DataTable.Rows[0];
            }
            set { }
        }

        #endregion

        public object[] ToJsonSafe()
        {
            object[] results = new object[this.Count];
            this.Each((o, i) =>
            {
                results[i] = o.ToJsonSafe();
            });

            return results;
        }

        #region IAddable Members

        public void Add(object value)
        {
            this.Add((L)value);
        }


        #endregion

        bool _setDatabases; // set to true after ctor completes
        private void SetEachDatabase(Database db)
        {
            if (_setDatabases)
            {
                foreach (Dao dao in this)
                {
                    dao.Database = db;
                }
            }
        }
    }
}
