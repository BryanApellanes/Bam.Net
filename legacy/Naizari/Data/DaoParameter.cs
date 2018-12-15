/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Naizari.Data
{
    public class DaoParameter: DbParameter
    {
        public DaoParameter()
            : base()
        {
        }

        public DaoParameter(string name, object value)
        {
            if (!name.Trim().StartsWith("@"))
                name = "@" + name.Trim();

            this.ParameterName = name;
            this.Value = value;
        }


        public override DbType DbType
        {
            get;
            set;
        }

        public override ParameterDirection Direction
        {
            get;
            set;
        }

        public override bool IsNullable
        {
            get;
            set;
        }

        public override string ParameterName
        {
            get;
            set;
        }

        public override void ResetDbType()
        {
            //throw new NotImplementedException();
        }

        public override int Size
        {
            get;
            set;
        }

        public override string SourceColumn
        {
            get;
            set;
        }

        public override bool SourceColumnNullMapping
        {
            get;
            set;
        }

        public override DataRowVersion SourceVersion
        {
            get;
            set;
        }

        public override object Value
        {
            get;
            set;
        }
    }
}
