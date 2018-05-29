using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Presentation.Unicode
{
    public class EmojiColumns: QueryFilter<EmojiColumns>, IFilterToken
    {
        public EmojiColumns() { }
        public EmojiColumns(string columnName)
            : base(columnName)
        { }
		
		public EmojiColumns KeyColumn
		{
			get
			{
				return new EmojiColumns("Id");
			}
		}	

				
        public EmojiColumns Id
        {
            get
            {
                return new EmojiColumns("Id");
            }
        }
        public EmojiColumns Uuid
        {
            get
            {
                return new EmojiColumns("Uuid");
            }
        }
        public EmojiColumns Cuid
        {
            get
            {
                return new EmojiColumns("Cuid");
            }
        }
        public EmojiColumns Number
        {
            get
            {
                return new EmojiColumns("Number");
            }
        }
        public EmojiColumns Character
        {
            get
            {
                return new EmojiColumns("Character");
            }
        }
        public EmojiColumns Apple
        {
            get
            {
                return new EmojiColumns("Apple");
            }
        }
        public EmojiColumns Google
        {
            get
            {
                return new EmojiColumns("Google");
            }
        }
        public EmojiColumns Twitter
        {
            get
            {
                return new EmojiColumns("Twitter");
            }
        }
        public EmojiColumns One
        {
            get
            {
                return new EmojiColumns("One");
            }
        }
        public EmojiColumns Facebook
        {
            get
            {
                return new EmojiColumns("Facebook");
            }
        }
        public EmojiColumns Samsung
        {
            get
            {
                return new EmojiColumns("Samsung");
            }
        }
        public EmojiColumns Windows
        {
            get
            {
                return new EmojiColumns("Windows");
            }
        }
        public EmojiColumns GMail
        {
            get
            {
                return new EmojiColumns("GMail");
            }
        }
        public EmojiColumns SoftBank
        {
            get
            {
                return new EmojiColumns("SoftBank");
            }
        }
        public EmojiColumns DoCoMo
        {
            get
            {
                return new EmojiColumns("DoCoMo");
            }
        }
        public EmojiColumns KDDI
        {
            get
            {
                return new EmojiColumns("KDDI");
            }
        }
        public EmojiColumns ShortName
        {
            get
            {
                return new EmojiColumns("ShortName");
            }
        }

        public EmojiColumns CategoryId
        {
            get
            {
                return new EmojiColumns("CategoryId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Emoji);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}