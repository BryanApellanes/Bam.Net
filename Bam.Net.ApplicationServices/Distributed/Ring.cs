/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ApplicationServices.Distributed
{
    public abstract class Ring<T> : Ring where T: IDistributedRepository
    {
        public void AddSlot(T slotValue)
        {
            AddSlot(new Slot<T>(slotValue));
        }

        public void AddSlot(Slot<T> slot)
        {
            base.AddSlot(slot);
        }

        public Slot<T> GetSlot(int index)
        {
            return Slots[index] as Slot<T>;
        }

        public void AddRange(IEnumerable<Slot<T>> slots)
        {
            List<Slot> newSlots = new List<Slot>(Slots);
            newSlots.AddRange(slots);
            ConfigureSlots(newSlots);

            Slots = newSlots.ToArray();            
        }
    }

    public abstract class Ring
    {
        List<Slot> _slots;
        public Ring()
        {
            this._slots = new List<Slot>();
        }

        public Ring(int slotCount)
        {
            this.SetSlotCount(slotCount);
        }

        public void SetSlotCount(int count)
        {
            InitializeSlots(count);
        }

        public int SlotCount
        {
            get
            {
                return _slots.Count;
            }
        }

        public int SlotSize
        {
            get;
            private set;
        }

        public Slot[] Slots
        {
            get
            {
                return _slots.ToArray();
            }
            internal protected set
            {
                _slots = new List<Slot>(value);
            }
        }

        public void AddSlot(Slot slot)
        {
            _slots.Add(slot);
            ConfigureSlots(_slots);
        }

        protected internal abstract Slot CreateSlot();
        
        public abstract string GetHashString(object value);

        public abstract int GetRepositoryKey(object value);

        protected abstract Slot FindSlotByKey(int key);

        /// <summary>
        /// When implemented by a derived class should 
        /// instantiate and initialize the Slots.  This
        /// method is called each time the SlotCount
        /// is set.
        /// </summary>        
        protected virtual void InitializeSlots(int count)
        {
            List<Slot> slots = new List<Slot>(count);

            count.Times((i) =>
            {
                // if there are existing slots then don't
                // lose them since they may already have
                // configured providers
                if (_slots != null && i < _slots.Count)
                {
                    slots.Add(_slots[i]);
                }
                else
                {
                    slots.Add(CreateSlot());
                }
            });
            

            ConfigureSlots(slots);

            Slots = slots.ToArray();
        }
                
        protected virtual void ConfigureSlots(IEnumerable<Slot> slots)
        {
            int slotCount = slots.Count();
            int keysPerSlot = int.MaxValue / slotCount;
            int startKey = 0;
            int endKey = startKey + keysPerSlot;

            double slotDegrees = 360 / slotCount;
            double startAngle = 0;
            double endAngle = startAngle + slotDegrees;

            SlotSize = keysPerSlot;
            slots.Each((s, i) =>
            {
				s.Index = i;
                s.Parent = this;
                s.Keyspace = new Keyspace(startKey, endKey);
                startKey = endKey + 1;
                endKey = startKey + keysPerSlot;
                endKey = endKey < 0 ? int.MaxValue : endKey;

                s.StartAngle = startAngle;
                s.EndAngle = endAngle;

                if (i == slotCount - 1 && endAngle < 360)
                {
                    s.EndAngle = 360;
                }
                else
                {
                    startAngle = endAngle;
                    endAngle = endAngle + slotDegrees;
                }                
            });
        }        
    }
}
