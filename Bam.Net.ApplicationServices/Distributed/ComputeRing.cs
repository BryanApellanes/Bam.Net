/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Data;

namespace Bam.Net.ApplicationServices.Distributed
{
    public class ComputeRing: Ring<ComputeNode>, IDistributedRepository
    {
        public ComputeRing()
            : base()
        {
        }

        public ComputeRing(int slotCount)
            : base()
        {
            this.SetSlotCount(slotCount);
        }

        public void AddComputeNode(ComputeNode node)
        {
            AddSlot(new Slot<ComputeNode>(node));
        }
        

        protected internal override Slot CreateSlot()
        {
            return new Slot<ComputeNode>();
        }

        protected Slot FindSlot(object value)
        {
            int key = GetRepositoryKey(value);
			return FindSlotByKey(key);
        }

        protected override Slot FindSlotByKey(int key)
        {
            double slotIndex = Math.Floor((double)(key / SlotSize));
            Slot result = null;
            if (slotIndex < Slots.Length)
            {
                result = Slots[(int)slotIndex];
            }

            return result;
        }        

        #region IRepositoryProvider Members

        public void Create(CreateOperation value)
        {
            IDistributedRepository provider = GetRepositoryProvider(value);
            provider.Create(value);
        }

        public T Retrieve<T>(RetrieveOperation value)
        {
            IDistributedRepository provider = GetRepositoryProvider(value);
            return provider.Retrieve<T>(value);
        }

        public void Update(UpdateOperation value)
        {
            IDistributedRepository provider = GetRepositoryProvider(value);
            provider.Update(value);
        }

        public void Delete(DeleteOperation value)
        {
            IDistributedRepository provider = GetRepositoryProvider(value);
            provider.Delete(value);
        }

        public IEnumerable<T> Query<T>(QueryOperation query)
        {
            List<T> results = new List<T>();
            Parallel.ForEach<Slot>(Slots, (s) =>
            {
                IDistributedRepository provider = s.GetProvider<ComputeNode>();
                results.AddRange(provider.Query<T>(query));
            });

            return results.ToArray();
        }

		public ReplicationResult RecieveReplica(Operation operation)
		{
			throw new NotImplementedException();
		}

        #endregion

		public HubNode HubNode
		{
			get;
			protected set;
		}

        public override string GetHashString(object value)
        {
            Type type = value.GetType();
            
            PropertyInfo[] keyProperties = type.GetPropertiesWithAttributeOfType<RepositoryKey>();
            Type marker = keyProperties.Length > 0 ? typeof(RepositoryKey) : typeof(ColumnAttribute);

            StringBuilder stringToHashBuilder = GetHashStringFromProperties(value, new Type[] { marker }, type);
            string stringToHash = stringToHashBuilder.ToString();
            if (string.IsNullOrEmpty(stringToHash))
            {
                stringToHashBuilder = GetHashStringFromProperties(value, type);
                stringToHash = stringToHashBuilder.ToString();
            }

            return stringToHash.Sha1();
        }

        public override int GetRepositoryKey(object value)
        {
            int code = GetHashString(value).GetHashCode();
            code = code < 0 ? code * -1 : code;
            return code;
        }

        private static StringBuilder GetHashStringFromProperties(object value, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringToHash = new StringBuilder();
            properties.Each(prop =>
            {
                object val = prop.GetValue(value, null);
                string append = val == null ? "null" : val.ToString();
                stringToHash.Append(append);
            });

            return stringToHash;
        }

        private static StringBuilder GetHashStringFromProperties(object value, Type[] attributesAddorningPropertiesToScrape, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringToHash = new StringBuilder();
            properties.Each(prop =>
            {
                attributesAddorningPropertiesToScrape.Each(attrType =>
                {
                    if (prop.HasCustomAttributeOfType(attrType))
                    {
                        object val = prop.GetValue(value, null);
                        string append = val == null ? "null" : val.ToString();
                        stringToHash.Append(append);
                    }
                });
            });
            return stringToHash;
        }

        private IDistributedRepository GetRepositoryProvider(object value)
        {
            Slot slot = FindSlot(value);
            IDistributedRepository provider = slot.GetProvider<ComputeNode>();
            return provider;
        }
    }
}
