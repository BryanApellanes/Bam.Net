/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class Table: Dao // should be generated
    {

        public override IQueryFilter GetUniqueFilter()
        {
            return null;
            throw new NotImplementedException();
        }

        public override void Delete(Database db = null)
        {
            throw new NotImplementedException();
        }
    }
      
}
