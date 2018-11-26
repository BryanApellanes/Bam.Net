using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.OAuth.Data.Dao
{
    public class OAuthProviderSettingsDataColumns: QueryFilter<OAuthProviderSettingsDataColumns>, IFilterToken
    {
        public OAuthProviderSettingsDataColumns() { }
        public OAuthProviderSettingsDataColumns(string columnName)
            : base(columnName)
        { }
		
		public OAuthProviderSettingsDataColumns KeyColumn
		{
			get
			{
				return new OAuthProviderSettingsDataColumns("Id");
			}
		}	

				
        public OAuthProviderSettingsDataColumns Id
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Id");
            }
        }
        public OAuthProviderSettingsDataColumns Uuid
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Uuid");
            }
        }
        public OAuthProviderSettingsDataColumns Cuid
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Cuid");
            }
        }
        public OAuthProviderSettingsDataColumns ApplicationName
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("ApplicationName");
            }
        }
        public OAuthProviderSettingsDataColumns ApplicationIdentifier
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("ApplicationIdentifier");
            }
        }
        public OAuthProviderSettingsDataColumns ProviderName
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("ProviderName");
            }
        }
        public OAuthProviderSettingsDataColumns State
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("State");
            }
        }
        public OAuthProviderSettingsDataColumns Code
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Code");
            }
        }
        public OAuthProviderSettingsDataColumns ClientId
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("ClientId");
            }
        }
        public OAuthProviderSettingsDataColumns ClientSecret
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("ClientSecret");
            }
        }
        public OAuthProviderSettingsDataColumns AuthorizationUrl
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("AuthorizationUrl");
            }
        }
        public OAuthProviderSettingsDataColumns AuthorizationCallbackEndpoint
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("AuthorizationCallbackEndpoint");
            }
        }
        public OAuthProviderSettingsDataColumns AuthorizationEndpointFormat
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("AuthorizationEndpointFormat");
            }
        }
        public OAuthProviderSettingsDataColumns AuthorizationCallbackEndpointFormat
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("AuthorizationCallbackEndpointFormat");
            }
        }
        public OAuthProviderSettingsDataColumns CreatedBy
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("CreatedBy");
            }
        }
        public OAuthProviderSettingsDataColumns ModifiedBy
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("ModifiedBy");
            }
        }
        public OAuthProviderSettingsDataColumns Modified
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Modified");
            }
        }
        public OAuthProviderSettingsDataColumns Deleted
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Deleted");
            }
        }
        public OAuthProviderSettingsDataColumns Created
        {
            get
            {
                return new OAuthProviderSettingsDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(OAuthProviderSettingsData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}