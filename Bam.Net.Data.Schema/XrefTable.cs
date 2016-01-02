/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public class XrefTable: Table
    {
        public XrefTable()
            : base()
        { }

        public XrefTable(string leftTable, string rightTable)
            : this()
        {
            this.Left = leftTable;
            this.Right = rightTable;
        }

        string _left;
        public string Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
                SetName();
            }
        }

        string _right;        
        public string Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
                SetName();
            }
        }

        private void SetName()
        {
            Name = string.Format("{0}{1}", Left, Right);
        }
    }
}
