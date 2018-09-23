using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceTypeIdentifierColumns: QueryFilter<ServiceTypeIdentifierColumns>, IFilterToken
    {
        public ServiceTypeIdentifierColumns() { }
        public ServiceTypeIdentifierColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceTypeIdentifierColumns KeyColumn
		{
			get
			{
				return new ServiceTypeIdentifierColumns("Id");
			}
		}	

				
        public ServiceTypeIdentifierColumns Id
        {
            get
            {
                return new ServiceTypeIdentifierColumns("Id");
            }
        }
        public ServiceTypeIdentifierColumns Uuid
        {
            get
            {
                return new ServiceTypeIdentifierColumns("Uuid");
            }
        }
        public ServiceTypeIdentifierColumns Cuid
        {
            get
            {
                return new ServiceTypeIdentifierColumns("Cuid");
            }
        }
        public ServiceTypeIdentifierColumns BuildNumber
        {
            get
            {
                return new ServiceTypeIdentifierColumns("BuildNumber");
            }
        }
        public ServiceTypeIdentifierColumns Namespace
        {
            get
            {
                return new ServiceTypeIdentifierColumns("Namespace");
            }
        }
        public ServiceTypeIdentifierColumns TypeName
        {
            get
            {
                return new ServiceTypeIdentifierColumns("TypeName");
            }
        }
        public ServiceTypeIdentifierColumns AssemblyName
        {
            get
            {
                return new ServiceTypeIdentifierColumns("AssemblyName");
            }
        }
        public ServiceTypeIdentifierColumns AssemblyFileHash
        {
            get
            {
                return new ServiceTypeIdentifierColumns("AssemblyFileHash");
            }
        }
        public ServiceTypeIdentifierColumns DurableHash
        {
            get
            {
                return new ServiceTypeIdentifierColumns("DurableHash");
            }
        }
        public ServiceTypeIdentifierColumns DurableSecondaryHash
        {
            get
            {
                return new ServiceTypeIdentifierColumns("DurableSecondaryHash");
            }
        }
        public ServiceTypeIdentifierColumns Created
        {
            get
            {
                return new ServiceTypeIdentifierColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceTypeIdentifier);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}