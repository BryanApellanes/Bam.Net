using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class SectionColumns: QueryFilter<SectionColumns>, IFilterToken
    {
        public SectionColumns() { }
        public SectionColumns(string columnName)
            : base(columnName)
        { }
		
		public SectionColumns KeyColumn
		{
			get
			{
				return new SectionColumns("Id");
			}
		}	

				
        public SectionColumns Id
        {
            get
            {
                return new SectionColumns("Id");
            }
        }
        public SectionColumns Uuid
        {
            get
            {
                return new SectionColumns("Uuid");
            }
        }
        public SectionColumns Title
        {
            get
            {
                return new SectionColumns("Title");
            }
        }
        public SectionColumns Description
        {
            get
            {
                return new SectionColumns("Description");
            }
        }

        public SectionColumns InstructionSetId
        {
            get
            {
                return new SectionColumns("InstructionSetId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Section);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}