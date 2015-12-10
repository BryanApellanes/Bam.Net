/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net.Automation
{
    public class MultiFileCopyWorker: Worker, IEnumerable<string>
    {
        List<string> _filePaths;
        public MultiFileCopyWorker() : base() { }

        public MultiFileCopyWorker(string name)
            : base(name)
        {
            this._filePaths = new List<string>();
        }

        public void AddFile(string path)
        {
            _filePaths.Add(path);
        }

        public string[] FilePaths
        {
            get
            {
                return _filePaths.ToArray();
            }
        }

        public string Destination
        {
            get;
            set;
        }

        public void RemoveFile(string path)
        {
            _filePaths.Remove(path);
        }


        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            return _filePaths.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return _filePaths.GetEnumerator();
        }

        #endregion

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name", "Destination" }; }
        }

        protected override WorkState Do()
        {
            throw new NotImplementedException();
        }
    }
}
