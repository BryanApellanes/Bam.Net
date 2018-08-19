using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Services.DataReplication.Data;

namespace Bam.Net.Services.DataReplication
{
    [Serializable]
    public class DataPropertySet: ObservableCollection<DataProperty>
    {
        public DataPropertySet()
        {
            CollectionChanged += (s, a) =>
            {
                if(a.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    ThrowOnDuplicateNames();
                }
            };
        }

        public DataPropertySet Add(string name, object value)
        {
            Add(new DataProperty { Name = name, Value = value });
            return this;
        }

        public DataPropertySet Prop(string name, object value)
        {
            return Prop(name, value, out DataProperty ignore);
        }

        public DataPropertySet Prop(string name, object value, out DataProperty dataProperty)
        {
            dataProperty = DataProperty(name, value);
            return this;
        }

        public T Value<T>(string name)
        {
            object value = this.Where(dp => dp.Name.Equals(name)).Select(dp => dp.Value).FirstOrDefault();
            if (value != null)
            {
                return (T)value;
            }
            return default(T);
        }

        public object this[string name]
        {
            get
            {
                return DataProperty(name).Value;
            }
            set
            {
                DataProperty(name, value);
            }
        }

        protected DataProperty DataProperty(string name, object value = null)
        {
            DataProperty result = this.Where(dp => dp.Name.Equals(name)).FirstOrDefault();
            if (result == null)
            {
                result = new Data.DataProperty { Name = name, Value = value };
                Add(result);
                return result;
            }
            result.Value = value ?? result.Value;
            return result;
        }

        public static DataPropertySet FromDao(Bam.Net.Data.Dao dao)
        {
            DataPropertySet dpc = new DataPropertySet();
            typeof(Bam.Net.Data.Dao).GetProperties().Where(pi => pi.HasCustomAttributeOfType<ColumnAttribute>()).Each(pi => dpc.Add(pi.Name, pi.GetValue(dao)));
            return dpc;
        }

        public static DataPropertySet FromInstance(object instance)
        {
            DataPropertySet result = new DataPropertySet();
            instance.EachDataProperty((pi, obj) => new DataProperty { Name = pi.Name, Value = obj }).Each(dp => result.Add(dp));
            return result;
        }

        public T ToInstanceOf<T>() where T: class, new()
        {
            T result = new T();
            foreach(DataProperty prop in this)
            {
                result.Property(prop.Name, prop.Value, false);
            }
            return result;
        }

        private void ThrowOnDuplicateNames()
        {
            HashSet<string> names = new HashSet<string>();
            foreach(DataProperty dp in this)
            {
                Args.ThrowIf<InvalidOperationException>(names.Contains(dp.Name), "Multiple DataProperties found with the same name: {0}", dp.Name);
                names.Add(dp.Name);
            }
        }
    }
}
