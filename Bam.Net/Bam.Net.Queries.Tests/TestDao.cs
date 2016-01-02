/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class TestDao: Dao
    {
        public TestDao() { }
        public TestDao(DataRow row)
            : base(row)
        {
        }

        [KeyColumn(Name="Id")]
        public long Id { get; set; }

        [ForeignKey(Name="Fk")]
        public long Fk { get; set; }

        [Column(Name="Column")]
        public string Column { get; set; }

        public new void SetValue(string columnName, object value)
        {
            base.SetValue(columnName, value);
        }

        public override IQueryFilter GetUniqueFilter()
        {
            return null;
            //throw new NotImplementedException();
        }

        public override void Delete(Database db = null)
        {
            throw new NotImplementedException();
        }
    }
}
