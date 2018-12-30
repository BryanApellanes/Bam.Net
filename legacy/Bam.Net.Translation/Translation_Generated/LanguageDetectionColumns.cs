using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class LanguageDetectionColumns: QueryFilter<LanguageDetectionColumns>, IFilterToken
    {
        public LanguageDetectionColumns() { }
        public LanguageDetectionColumns(string columnName)
            : base(columnName)
        { }
		
		public LanguageDetectionColumns KeyColumn
		{
			get
			{
				return new LanguageDetectionColumns("Id");
			}
		}	

				
        public LanguageDetectionColumns Id
        {
            get
            {
                return new LanguageDetectionColumns("Id");
            }
        }
        public LanguageDetectionColumns Uuid
        {
            get
            {
                return new LanguageDetectionColumns("Uuid");
            }
        }
        public LanguageDetectionColumns Cuid
        {
            get
            {
                return new LanguageDetectionColumns("Cuid");
            }
        }
        public LanguageDetectionColumns Detector
        {
            get
            {
                return new LanguageDetectionColumns("Detector");
            }
        }
        public LanguageDetectionColumns ResponseData
        {
            get
            {
                return new LanguageDetectionColumns("ResponseData");
            }
        }

        public LanguageDetectionColumns LanguageId
        {
            get
            {
                return new LanguageDetectionColumns("LanguageId");
            }
        }
        public LanguageDetectionColumns TextId
        {
            get
            {
                return new LanguageDetectionColumns("TextId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LanguageDetection);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}