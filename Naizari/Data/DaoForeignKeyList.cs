/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Data
{
    /// <summary>
    /// Intended to replace the simple arrays used on DaoObjects to represent Foreign Key object instances.
    /// </summary>
    public class DaoForeignKeyList<T>: IEnumerable<T> where T: DaoObject, new()
    {
        string fkColumnName;
        object id;
        DatabaseAgent agent;
        List<DaoObject> updated;
        List<T> toInsert;
        DaoObject parent;
        List<T> values;
        public DaoForeignKeyList(string fkColumnName, DaoObject parent, Func<string, object, DatabaseAgent, T[]> selectFunction)
        {
            if (parent.IsNew)
                throw new InvalidOperationException("Parent object not committed");

            this.selectFunction = selectFunction;
            this.fkColumnName = fkColumnName;
            this.id = parent.GetLongValue(parent.IdColumnName);//id;
            this.agent = parent.DatabaseAgent;
            this.updated = new List<DaoObject>();
            this.parent = parent;
            this.parent.ChangesCommitted += new DaoObjectEventHandler(parent_ChangesCommitted);
            this.parent.BeforeDelete += (dao) =>
            {
                foreach (T obj in this)
                {
                    obj.AllowDelete = true;
                    obj.Delete(parent.DatabaseAgent);
                }
            };

            this.values = new List<T>();
            this.toInsert = new List<T>();
            this.Refresh();
        }


        void parent_ChangesCommitted(DaoObject sender)
        {
            this.Commit();
        }

        Func<string, object, DatabaseAgent, T[]> selectFunction;

        public DaoForeignKeyList<T> Refresh()
        {
            this.values.Clear();
            T[] results = selectFunction(this.fkColumnName, id, this.agent);
            foreach (T result in results)
            {
                result.ValueChanged += new DaoValueChangedEventHandler(ChildValueChanged);
            }

            this.values.AddRange(results);
            return this;
        }

        void ChildValueChanged(DaoObject sender, DaoValueChangedEventArgs e)
        {
            this.parent.ChangesPending = true;
            if (!this.updated.Contains(sender) && !sender.IsNew)
            {
                this.updated.Add(sender);
            }
            else if(!this.toInsert.Contains((T)sender) && sender.IsNew)
            {
                this.toInsert.Add((T)sender);
            }

        }

        internal void Commit()
        {
            if (this.updated.Count > 0 || this.toInsert.Count > 0)
            {
                foreach (DaoObject o in updated)
                {
                    if (o.Update() == UpdateResult.Error)
                    {
                        throw o.LastException;
                    }
                }

                this.updated.Clear();

                foreach (T o in toInsert)
                {
                    if (o.Insert() == -1)
                    {
                        throw o.LastException;
                    }
                }
                this.toInsert.Clear();

                this.Refresh();
            }
        }

        public T Add()
        {
            MethodInfo[] methods = typeof(T).GetMethods();
            IEnumerable<MethodInfo> methodResults = from method in methods
                                                    where method.Name.Equals("New")
                                                        && method.GetParameters().Length == 1
                                                    select method;
            T insertInstance = (T)methodResults.First().Invoke(null, new object[] { this.parent.DatabaseAgent });
            insertInstance.SetValue(fkColumnName, parent.GetValue(parent.IdColumnName));
            insertInstance.ValueChanged += new DaoValueChangedEventHandler(ChildValueChanged);
            this.values.Add(insertInstance);
            this.toInsert.Add(insertInstance);
            this.parent.ChangesPending = true;
            return insertInstance;
        }

        /// <summary>
        /// Asserts that there is only 1 value in the list returning that value.
        /// If more than 1 value exists in the list an MultipleEntriesFoundException is thrown.
        /// If there are no values an uncommitted value is returned.
        /// </summary>
        public T One()
        {
            //get
            //{
                if (this.values.Count > 1)
                {
                    throw new MultipleEntriesFoundException();
                }

                if (this.values.Count == 0)
                {
                    return this.Add();
                }
                else
                {
                    return this.values[0];
                }
            //}
        }

        public int Length
        {
            get
            {
                return this.values.Count;
            }
        }

        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.values[index];
            }
        }
        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        #endregion
    }
}
