/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class DaoObjectInsertResult
    {
        public static implicit operator int(DaoObjectInsertResult result)
        {
            return (int)result.Id;
        }

        public static implicit operator long(DaoObjectInsertResult result)
        {
            return result.Id;
        }

        public DaoObjectInsertResult(int id)
            : this((long)id)
        {
        }

        public DaoObjectInsertResult(long id)
        {
            this.Id = id;
            this.Status = DaoObjectInsertStatus.Success;
        }

        public DaoObjectInsertResult(long id, Exception ex)
        {
            this.Id = id;
            this.Exception = ex;
            this.Status = DaoObjectInsertStatus.Error;
        }

        public DaoObjectInsertStatus Status { get; set; }
        public long Id { get; set; }
        public Exception Exception { get; set; }
    }
}
