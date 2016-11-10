/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Distributed
{
    public abstract class Ring<T> : Ring where T: IDistributedRepository
    {
        public void AddArc(T arcValue)
        {
            AddArc(new Arc<T>(arcValue));
        }

        public void AddArc(Arc<T> arc)
        {
            base.AddArc(arc);
        }

        public Arc<T> GetArc(int index)
        {
            return Arcs[index] as Arc<T>;
        }

        public void AddRange(IEnumerable<Arc<T>> slots)
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
            this.SetSlotCount(slotCount);
        }

        public void SetSlotCount(int count)
        {
            InitializeArcs(count);
        }

        public int SlotCount
        {
            get
            {
                return _arcs.Count;
            }
        }

        public int ArcSize
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

        public void AddArc(Arc arc)
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
        /// instantiate and initialize the Slots.  This
        /// method is called each time the SlotCount
        /// is set.
        /// </summary>        
        protected virtual void InitializeArcs(int count)
        {
            List<Arc> arcs = new List<Arc>(count);

            count.Times((i) =>
            {
                // if there are existing slots then don't
                // lose them since they may already have
                // configured providers
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
            int keysPerArc = int.MaxValue / arcCount;
            int startKey = 0;
            int endKey = startKey + keysPerArc;

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
