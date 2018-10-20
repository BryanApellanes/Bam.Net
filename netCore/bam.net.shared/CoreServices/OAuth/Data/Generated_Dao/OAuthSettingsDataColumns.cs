using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.OAuth.Data.Dao
{
    public class OAuthSettingsDataColumns: QueryFilter<OAuthSettingsDataColumns>, IFilterToken
    {
        public OAuthSettingsDataColumns() { }
        public OAuthSettingsDataColumns(string columnName)
            : base(columnName)
        { }
		
		public OAuthSettingsDataColumns KeyColumn
		{
			get
			{
				return new OAuthSettingsDataColumns("Id");
			}
		}	

				
        public OAuthSettingsDataColumns Id
        {
            get
            {
                return new OAuthSettingsDataColumns("Id");
            }
        }
        public OAuthSettingsDataColumns Uuid
        {
            get
            {
                return new OAuthSettingsDataColumns("Uuid");
            }
        }
        public OAuthSettingsDataColumns Cuid
        {
            get
            {
                return new OAuthSettingsDataColumns("Cuid");
            }
        }
        public OAuthSettingsDataColumns ApplicationName
        {
            get
            {
                return new OAuthSettingsDataColumns("ApplicationName");
            }
        }
        public OAuthSettingsDataColumns ApplicationIdentifier
        {
            get
            {
                return new OAuthSettingsDataColumns("ApplicationIdentifier");
            }
        }
        public OAuthSettingsDataColumns ProviderName
        {
            get
            {
                return new OAuthSettingsDataColumns("ProviderName");
            }
        }
        public OAuthSettingsDataColumns State
        {
            get
            {
                return new OAuthSettingsDataColumns("State");
            }
        }
        public OAuthSettingsDataColumns Code
        {
            get
            {
                return new OAuthSettingsDataColumns("Code");
            }
        }
        public OAuthSettingsDataColumns ClientId
        {
            get
            {
                return new OAuthSettingsDataColumns("ClientId");
            }
        }
        public OAuthSettingsDataColumns ClientSecret
        {
            get
            {
                return new OAuthSettingsDataColumns("ClientSecret");
            }
        }
        public OAuthSettingsDataColumns AuthorizationUrl
        {
            get
            {
                return new OAuthSettingsDataColumns("AuthorizationUrl");
            }
        }
        public OAuthSettingsDataColumns AuthorizationCallbackUrl
        {
            get
            {
                return new OAuthSettingsDataColumns("AuthorizationCallbackUrl");
            }
        }
        public OAuthSettingsDataColumns AuthorizationUrlFormat
        {
            get
            {
                return new OAuthSettingsDataColumns("AuthorizationUrlFormat");
            }
        }
        public OAuthSettingsDataColumns AuthorizationCallbackUrlFormat
        {
            get
            {
                return new OAuthSettingsDataColumns("AuthorizationCallbackUrlFormat");
            }
        }
        public OAuthSettingsDataColumns CreatedBy
        {
            get
            {
                return new OAuthSettingsDataColumns("CreatedBy");
            }
        }
        public OAuthSettingsDataColumns ModifiedBy
        {
            get
            {
                return new OAuthSettingsDataColumns("ModifiedBy");
            }
        }
        public OAuthSettingsDataColumns Modified
        {
            get
            {
                return new OAuthSettingsDataColumns("Modified");
            }
        }
        public OAuthSettingsDataColumns Deleted
        {
            get
            {
                return new OAuthSettingsDataColumns("Deleted");
            }
        }
        public OAuthSettingsDataColumns Created
        {
            get
            {
                return new OAuthSettingsDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(OAuthSettingsData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}