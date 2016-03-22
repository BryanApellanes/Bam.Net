using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class InstructionSetColumns: QueryFilter<InstructionSetColumns>, IFilterToken
    {
        public InstructionSetColumns() { }
        public InstructionSetColumns(string columnName)
            : base(columnName)
        { }
		
		public InstructionSetColumns KeyColumn
		{
			get
			{
				return new InstructionSetColumns("Id");
			}
		}	

				
        public InstructionSetColumns Id
        {
            get
            {
                return new InstructionSetColumns("Id");
            }
        }
        public InstructionSetColumns Uuid
        {
            get
            {
                return new InstructionSetColumns("Uuid");
            }
        }
        public InstructionSetColumns Name
        {
            get
            {
                return new InstructionSetColumns("Name");
            }
        }
        public InstructionSetColumns Description
        {
            get
            {
                return new InstructionSetColumns("Description");
            }
        }
        public InstructionSetColumns Author
        {
            get
            {
                return new InstructionSetColumns("Author");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(InstructionSet);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}