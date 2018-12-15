/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Naizari.Testing;
using System.Threading;
using System.Data;
using Naizari.Extensions;

namespace Naizari.Data.Paging
{
    public class CustomIndex<TDao> where TDao: DaoObject, new()
    {
        public delegate void IndexReadyDelegate(CustomIndex<TDao> customIndex);

        public class IndexInfo
        {
            public IndexInfo()
            {
            }

            public long ID { get; set; }
            public string Value { get; set; }
        }

        DatabaseAgent agent;
        List<IndexInfo> index;
        public CustomIndex(DatabaseAgent agent, PropertyInfo propertyToIndex)
        {
            Expect.IsNotNull(propertyToIndex, "PropertyToIndex cannot be null, double check the spelling if using the ctor CustomIndex(DatabaseAgent agent, string propertyName).");
            this.agent = agent;
            this.index = new List<IndexInfo>();
            this.PropertyInfo = propertyToIndex;

            Type tdaoType = typeof(TDao);
            string namespaceQualifiedTypeName = tdaoType.Namespace + "." + tdaoType.Name;
            Expect.IsObjectOfType<TDao>(propertyToIndex.DeclaringType, "Specified property is invalid, must be a property from Type " + namespaceQualifiedTypeName);            
        }

        public CustomIndex(DatabaseAgent agent, string propertyName)
            : this(agent, typeof(TDao).GetProperty(propertyName))
        {
        }
        
        public PropertyInfo PropertyInfo
        {
            get;
            internal set;
        }

        public string Property
        {
            get
            {
                return this.PropertyInfo.Name;
            }
        }
        
        public List<IndexInfo> Index
        {
            get
            {
                if (!this.Ready)
                    return null;

                return this.index;
            }
        }

        public bool Ready
        {
            get;
            private set;
        }

        public void OnIndexReady()
        {
            if (this.IndexReady != null)
            {
                this.IndexReady(this);
            }
        }

        public event IndexReadyDelegate IndexReady;

        Thread indexingThread;
        /// <summary>
        /// Starts the preparation of the custom index in a separate thread.
        /// Prior to calling this method one should subscribe to the 
        /// IndexReady event to be notified when queries can be made
        /// against this CustomIndex.
        /// </summary>
        public void StartPrepIndex()
        {
            indexingThread = new Thread(new ParameterizedThreadStart(PrepIndex));
            indexingThread.IsBackground = true;
            indexingThread.Start(Property);
        }

        private void PrepIndex(object propertyToIndexObj)
        {
            Expect.IsTrue(propertyToIndexObj is string, "ProperytToIndex must be a string");

            string propertyToIndex = propertyToIndexObj as string;
            TDao proxy = new TDao();
            string q = "SELECT {0} as ID,{1} as Value FROM {2} ORDER BY {0} ASC";
            string idColumn = proxy.IdColumnName;
            string columnName = proxy.GetPropertyToColumnMap()[propertyToIndex];
            string tableName = proxy.TableName;

            Expect.IsNotNullOrEmpty(idColumn, "No ID Column was found on table " + tableName + ", regeneration may be required.");

            string sql = string.Format(q, idColumn, columnName, tableName);
            DataTable results = this.agent.GetDataTableFromSql(sql);
            foreach (DataRow row in results.Rows)
            {
                IndexInfo info = new IndexInfo();
                info.HydrateFromDataRow(row);
                this.index.Add(info);
            }

            this.Ready = true;
            this.OnIndexReady();
        }
    }
}
