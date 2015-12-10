/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class WorkSequence: Worker, IEnumerable<IWorker>
    {
        public WorkSequence() : base() { }
        public WorkSequence(string name)
            : base(name)
        {
            this._workList = new List<IWorker>();
        }

        public void AddWork(IWorker work)
        {
            _workList.Add(work);
        }

        public void RemoveWork(IWorker work)
        {
            _workList.Remove(work);
        }

        public bool ContainsWork(IWorker work)
        {
            return ContainsWork(work.Name);
        }

        public bool ContainsWork(string workName)
        {
            return (from work in _workList
                    where work.Name.Equals(workName)
                    select work).FirstOrDefault() != null;
        }

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name" }; }
        }

        protected override WorkState Do()
        {
            throw new NotImplementedException();
        }

        List<IWorker> _workList;
        object _workListLock = new object();
        protected List<IWorker> WorkList
        {
            get
            {
                return _workListLock.DoubleCheckLock(ref _workList, () => new List<IWorker>());
            }
        }

        #region IEnumerable<IWork> Members

        public IEnumerator<IWorker> GetEnumerator()
        {
            return _workList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return _workList.GetEnumerator();
        }

        #endregion
    }
}
