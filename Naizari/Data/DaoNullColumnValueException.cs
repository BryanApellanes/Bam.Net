/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Naizari.Data
{
    public class DaoNullColumnValueException: Exception
    {
        public DaoNullColumnValueException(SqlException innerException)
            : base(
                string.Format("{0}\r\n ***** \r\n If this is a PrimaryKeyColumn that needs to be manually set try setting the AllowModifyPrimaryKeyValue property to true.\r\n ***** \r\n", innerException.Message), 
            innerException)
        {
        }
    }
}
