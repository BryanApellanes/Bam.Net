using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.Services.Distributed.Data
{
    [Serializable]
    public class DataPropertyCollection: ObservableCollection<DataProperty>
    {
        public DataPropertyCollection()
        {
            CollectionChanged += (s, a) =>
            {
                if(a.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    ThrowOnDuplicateNames();
                }
            };
        }
        public DataPropertyCollection Add(string name, object value)
        {
            Add(new DataProperty { Name = name, Value = value });
            return this;
        }
        public DataPropertyCollection Prop(string name, object value)
        {
            DataProperty ignore;
            return Prop(name, value, out ignore);
        }

        public DataPropertyCollection Prop(string name, object value, out DataProperty dataProperty)
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

        public static DataPropertyCollection FromDao(Dao dao)
        {
            DataPropertyCollection dpc = new DataPropertyCollection();
            typeof(Dao).GetProperties().Where(pi => pi.HasCustomAttributeOfType<ColumnAttribute>()).Each(pi => dpc.Add(pi.Name, pi.GetValue(dao)));
            return dpc;
        }

        public static DataPropertyCollection FromInstance(object instance)
        {
            DataPropertyCollection result = new DataPropertyCollection();
            instance.EachDataProperty((pi, obj) => new DataProperty { Name = pi.Name, Value = obj }).Each(dp => result.Add(dp));
            return result;
        }

        public T ToInstanceOf<T>() where T: class, new()
        {
            T result = new T();
            foreach(DataProperty prop in this)
            {
                result.Property(prop.Name, prop.Value);
            }
            return result;
        }

        private void ThrowOnDuplicateNames()
        {
            List<string> names = new List<string>();
            foreach(DataProperty dp in this)
            {
                Args.ThrowIf<InvalidOperationException>(names.Contains(dp.Name), "Multiple DataProperties found with the same name: {0}", dp.Name);
                names.Add(dp.Name);
            }
        }
    }
}
