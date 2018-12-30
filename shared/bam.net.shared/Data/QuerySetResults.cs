/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class QuerySetResults
    {
        List<IHasDataTable> _values;
        public QuerySetResults(IEnumerable<IHasDataTable> values, Database database)
        {
            this._values = new List<IHasDataTable>(values);
            this.Database = database;
        }

        public Database Database { get; set; }
        /// <summary>
        /// Instantiates a new instance of T and calls SetDataTable passing
        /// in the DataTable from the specified index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T As<T>(int index) where T: IHasDataTable, new()
        {
            _values[index].Database = this.Database;
            return _values[index].As<T>();
        }

        public IHasDataTable this[int index]
        {
            get
            {
                return _values[index];
            }
        }

        /// <summary>
        /// Returns the value of the specified index as the specified 
        /// generic Dao type, only valid for inserts.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T ToDao<T>(int index) where T : Dao, new()
        {
            InsertResult ir = _values[index] as InsertResult;
            if (ir == null)
            {
                throw new InvalidOperationException("The specified index was not an InsertResult");
            }

            T result = (T)ir.Value;           

            return result;
        }

        public long ToCountResult(int index)
        {
            CountResult cr = _values[index] as CountResult;
            if (cr == null)
            {
                throw new InvalidOperationException("The specified index was not CountResult");
            }

            return cr.Value;
        }
                
        public int Count
        {
            get
            {
                return _values.Count;
            }
        }
    }
}
