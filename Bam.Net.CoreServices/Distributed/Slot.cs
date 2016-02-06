/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.CoreServices.Distributed
{
    public class Slot<T>: Slot where T: IDistributedRepository
    {
        public Slot() : base() { }
        public Slot(T value)
            : base()
        {
            this.SetProvider(value);
        }

        public new T GetValue()
        {
            return (T)base.GetValue();
        }
    }

    public class Slot
    {
		public Slot()
		{
			this.Index = -1;
		}

        public IDistributedRepository Provider { get; set; }

        public T GetProvider<T>() where T: IDistributedRepository
        {
            return (T)Provider;
        }

        public void SetProvider(IDistributedRepository provider)
        {
            this.Provider = provider;
        }

        public Keyspace Keyspace
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// Gets the starting angle that this slot is valid for
        /// </summary>
        public double StartAngle { get; internal set; }

        /// <summary>
        /// Gets the ending angle that this slot is valid for
        /// </summary>
        public double EndAngle { get; internal set; }

        public double Degrees
        {
            get
            {
                return EndAngle - StartAngle;
            }
        }

        public double Radians
        {
            get
            {
                return Degrees * (Math.PI / 180);
            }
        }

		public int Index { get; protected internal set; }

		public Slot NeighborLast
		{
			get
			{
				if (Index == 0)
				{
					return Parent.Slots[Parent.Slots.Length - 1];
				}
				else
				{
					return Parent.Slots[Index - 1];
				}
			}
		}

		public Slot NeighborNext
		{
			get
			{
				if(Index == Parent.Slots.Length - 1)
				{
					return Parent.Slots[0];
				}
				else
				{
					return Parent.Slots[Index + 1];
				}
			}
		}

        public Ring Parent { get; protected internal set; }

        public virtual object GetValue()
        {
            return this.Provider;
        }
    }
}
