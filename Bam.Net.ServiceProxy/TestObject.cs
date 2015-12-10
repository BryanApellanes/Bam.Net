/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bam.Net.ServiceProxy
{
    public class TestObject
    {
        public TestObject()
        {
        }

        public string Name { get; set; }
        public int Number { get; set; }
        public int SubNumber { get; set; } // testing to see if the SubObject.SubNumber overrides this value on form post

        SubObject _sub;
        object _subLock = new object();
        public SubObject SubObject
        {
            get
            {
                return _subLock.DoubleCheckLock(ref _sub, () => new SubObject());
            }
            set
            {
                _sub = value;
            }
        }
    }

    public class SubObject
    {
        public string SubName { get; set; }
        public int SubNumber { get; set; }
    }
}