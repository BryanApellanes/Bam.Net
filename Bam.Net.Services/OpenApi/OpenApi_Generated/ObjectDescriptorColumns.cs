using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class ObjectDescriptorColumns: QueryFilter<ObjectDescriptorColumns>, IFilterToken
    {
        public ObjectDescriptorColumns() { }
        public ObjectDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ObjectDescriptorColumns KeyColumn
		{
			get
			{
				return new ObjectDescriptorColumns("Id");
			}
		}	

				
        public ObjectDescriptorColumns Id
        {
            get
            {
                return new ObjectDescriptorColumns("Id");
            }
        }
        public ObjectDescriptorColumns Uuid
        {
            get
            {
                return new ObjectDescriptorColumns("Uuid");
            }
        }
        public ObjectDescriptorColumns Cuid
        {
            get
            {
                return new ObjectDescriptorColumns("Cuid");
            }
        }
        public ObjectDescriptorColumns Name
        {
            get
            {
                return new ObjectDescriptorColumns("Name");
            }
        }
        public ObjectDescriptorColumns Description
        {
            get
            {
                return new ObjectDescriptorColumns("Description");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ObjectDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}