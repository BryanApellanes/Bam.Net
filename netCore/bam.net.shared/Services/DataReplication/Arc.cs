/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Services.DataReplication
{
    public class Arc<T>: Arc 
    {
        public Arc() : base() { }
        public Arc(T serviceProvider)
            : base()
        {
            ServiceProvider = serviceProvider;
        }

        public T GetTypedServiceProvider()
        {
            return (T)ServiceProvider;
        }
    }

    public class Arc
    {
		public Arc()
		{
			this.Index = -1;
		}

        /// <summary>
        /// The local ProxyableService proxy instance
        /// </summary>
        public object ServiceProvider { get; set; }

        public object GetServiceProvider()
        {
            return ServiceProvider;
        }

        public void SetServiceProvider(object provider)
        {
            ServiceProvider = provider;
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

		public Arc NeighborLast
		{
			get
			{
				if (Index == 0)
				{
					return Parent.Arcs[Parent.Arcs.Length - 1];
				}
				else
				{
					return Parent.Arcs[Index - 1];
				}
			}
		}

		public Arc NeighborNext
		{
			get
			{
				if(Index == Parent.Arcs.Length - 1)
				{
					return Parent.Arcs[0];
				}
				else
				{
					return Parent.Arcs[Index + 1];
				}
			}
		}

        public Ring Parent { get; protected internal set; }

        public virtual object GetValue()
        {
            return this.ServiceProvider;
        }
    }
}
