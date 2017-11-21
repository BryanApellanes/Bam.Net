/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.DataReplication;

namespace Bam.Net.Services.DataReplication
{
    public abstract class Ring<TService> : Ring where TService: IDistributedRepository
    {
        public virtual void AddArc(TService arcValue)
        {
            AddArc(new Arc<TService>(arcValue));
        }

        public virtual void AddArc(Arc<TService> arc)
        {
            base.AddArc(arc);
        }

        public Arc<TService> GetArc(int index)
        {
            return Arcs[index] as Arc<TService>;
        }

        public void AddRange(IEnumerable<Arc<TService>> slots)
        {
            List<Arc> newArcs = new List<Arc>(Arcs);
            newArcs.AddRange(slots);
            ConfigureArcs(newArcs);

            Arcs = newArcs.ToArray();            
        }
    }

    public abstract class Ring
    {
        List<Arc> _arcs;
        public Ring()
        {
            this._arcs = new List<Arc>();
        }

        public Ring(int slotCount)
        {
            this.SetArcCount(slotCount);
        }

        public void SetArcCount(int count)
        {
            InitializeArcs(count);
        }

        public int ArcCount
        {
            get
            {
                return _arcs.Count;
            }
        }

        public long ArcSize
        {
            get;
            private set;
        }

        public Arc[] Arcs
        {
            get
            {
                return _arcs.ToArray();
            }
            internal protected set
            {
                _arcs = new List<Arc>(value);
            }
        }

        public virtual void AddArc(Arc arc)
        {
            _arcs.Add(arc);
            ConfigureArcs(_arcs);
        }

        protected internal abstract Arc CreateArc();
        
        public abstract string GetHashString(object value);

        public abstract int GetRepositoryKey(object value);

        protected abstract Arc FindArcByKey(int key);

        /// <summary>
        /// When implemented by a derived class should 
        /// instantiate and initialize the Arcs.  This
        /// method is called each time the ArcCount
        /// is set.
        /// </summary>        
        protected virtual void InitializeArcs(int count)
        {
            List<Arc> arcs = new List<Arc>(count);

            count.Times((i) =>
            {
                // if there are existing arcs then don't
                // lose them since they may already have
                // configured clients
                if (_arcs != null && i < _arcs.Count)
                {
                    arcs.Add(_arcs[i]);
                }
                else
                {
                    arcs.Add(CreateArc());
                }
            });
            

            ConfigureArcs(arcs);

            Arcs = arcs.ToArray();
        }
                
        protected virtual void ConfigureArcs(IEnumerable<Arc> arcs)
        {
            int arcCount = arcs.Count();
            long keysPerArc = long.MaxValue / arcCount;
            long startKey = 0;
            long endKey = startKey + keysPerArc;

            double arcDegrees = 360 / arcCount;
            double startAngle = 0;
            double endAngle = startAngle + arcDegrees;

            ArcSize = keysPerArc;
            arcs.Each((a, i) =>
            {
				a.Index = i;
                a.Parent = this;
                a.Keyspace = new Keyspace(startKey, endKey);
                startKey = endKey + 1;
                endKey = startKey + keysPerArc;
                endKey = endKey < 0 ? int.MaxValue : endKey;

                a.StartAngle = startAngle;
                a.EndAngle = endAngle;

                if (i == arcCount - 1 && endAngle < 360)
                {
                    a.EndAngle = 360;
                }
                else
                {
                    startAngle = endAngle;
                    endAngle = endAngle + arcDegrees;
                }                
            });
        }        
    }
}
