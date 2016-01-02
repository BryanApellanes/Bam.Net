/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class DaoObjectCommitResult
    {
        public DaoObjectInsertResult InsertResult { get; set; }

        public UpdateResult UpdateResult { get; set; }

        public Exception Exception { get; set; }

        public long Id { get; set; }
    }
}
