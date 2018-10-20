using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Bam.Net;
using Bam.Net.Incubation;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    /// <summary>
    /// Data Access Object
    /// </summary>
    public abstract partial class Dao
    {
        static Dictionary<Type, object> _dynamicTypeLocks = new Dictionary<Type, object>();
        /// <summary>
        /// Creates an in memory dynamic type representing
        /// the current Dao's Columns only.
        /// </summary>
        /// <returns></returns>
        public object ToJsonSafe()
        {
            Type thisType = this.GetType();
            _dynamicTypeLocks.AddMissing(thisType, new object());
            lock (_dynamicTypeLocks[thisType])
            {
                Type jsonSafeType = this.BuildDynamicType<ColumnAttribute>(false);
                ConstructorInfo ctor = jsonSafeType.GetConstructor(new Type[] { });
                object jsonSafeInstance = ctor.Invoke(null);
                jsonSafeInstance.CopyProperties(this);
                return jsonSafeInstance;
            }
        }

        /// <summary>
        /// Creates an in memory dynamic type representing
        /// the current Dao's Columns only.
        /// </summary>
        /// <param name="includeExtras">Include anything added through the Value method</param>
        /// <returns></returns>
        public object ToJsonSafe(bool includeExtras = false)
        {
            object jsonSafe = ToJsonSafe();
            object result = jsonSafe;
            if (includeExtras)
            {
                object extras = ToDynamic();
                Type mergedType = new List<object>() { jsonSafe, extras }.MergeToDynamicType(Dao.TableName(this), 0);
                result = mergedType.Construct();
                result.CopyProperties(extras);
                result.CopyProperties(jsonSafe);
            }
            return result;
        }

        /// <summary>
        /// Create an in memory dynamic type representing 
        /// all the values in DataRow including anything 
        /// added through the Value method
        /// </summary>
        /// <returns></returns>
        public object ToDynamic()
        {
            return DataRow.ToDynamic();
        }

    }
}
