using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.OAuth.Data.Dao
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
        public OAuthSettingsDataColumns ApplicationCuid
        {
            get
            {
                return new OAuthSettingsDataColumns("ApplicationCuid");
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
        public OAuthSettingsDataColumns AuthCallbackUrl
        {
            get
            {
                return new OAuthSettingsDataColumns("AuthCallbackUrl");
            }
        }
        public OAuthSettingsDataColumns TokenCallbackUrl
        {
            get
            {
                return new OAuthSettingsDataColumns("TokenCallbackUrl");
            }
        }
        public OAuthSettingsDataColumns AuthorizationEndpointFormat
        {
            get
            {
                return new OAuthSettingsDataColumns("AuthorizationEndpointFormat");
            }
        }
        public OAuthSettingsDataColumns TokenEndpointFormat
        {
            get
            {
                return new OAuthSettingsDataColumns("TokenEndpointFormat");
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