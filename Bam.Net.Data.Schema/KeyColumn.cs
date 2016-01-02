/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A key/identity column.
    /// </summary>
    public class KeyColumn: Column
    {
		public KeyColumn() { }

        public KeyColumn(Column c)
            : base(c.TableName)
        {
            this.Name = c.Name;
            this.DataType = c.DataType;
            this.DbDataType = c.DbDataType;
            this.MaxLength = c.MaxLength;
        }

        public KeyColumn(string name, DataTypes type, bool allowNull = true)
        {
            this.Name = name;
            this.DataType = type;
            this.AllowNull = allowNull;
        }

        public override bool AllowNull
        {
            get
            {
                return false;
            }
            set
            {
                // no nulls if key column
            }
        }

        public override bool Key
        {
            get
            {
                return true;
            }
            set
            {
                // setter for deserialization only, key is always true
            }
        }

        static KeyColumn _default = new KeyColumn("Id", DataTypes.Long);
        public static KeyColumn Default
        {
            get
            {
                return _default;
            }
        }

        protected internal override string RenderClassProperty()
        {
            return Render<KeyColumn>("KeyProperty.tmpl");
        }
    }
}
