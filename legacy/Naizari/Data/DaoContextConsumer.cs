/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Naizari.Data
{
    public abstract class DaoContextConsumer : Naizari.Data.IDaoContextConsumer
    {
        public event DaoContextConnectionStringSetEventHandler DaoContextConnectionStringSet;

        string serverName;
        public string ServerName
        {
            get { return serverName; }
            set
            {
                serverName = value;
                if (!string.IsNullOrEmpty(databaseName) &&
                    !string.IsNullOrEmpty(contextName))
                    SetConnectionString();//DaoContext.Init(contextName, GetConnectionString());
            }
        }

        string databaseName;
        public string DatabaseName
        {
            get { return databaseName; }
            set
            {
                databaseName = value;
                if (!string.IsNullOrEmpty(serverName) &&
                    !string.IsNullOrEmpty(contextName))
                    SetConnectionString();
            }
        }

        public void SetConnectionString()
        {
            DaoContext context = DaoContext.Get(contextName, GetConnectionString());
            OnDaoContextConnectionStringSet(context, ContextName);
        }

        private void OnDaoContextConnectionStringSet(DaoContext context, string logicalName)
        {
            if (DaoContextConnectionStringSet!= null)
                DaoContextConnectionStringSet(context, logicalName);
        }

        string contextName;
        public string ContextName
        {
            get { return contextName; }
            set
            {
                contextName = value;
                if (!string.IsNullOrEmpty(serverName) &&
                    !string.IsNullOrEmpty(databaseName))
                    SetConnectionString();
            }
        }

        string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                if (!string.IsNullOrEmpty(userName))
                    IntegratedSecurity = string.Empty;
            }
        }

        string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                if (!string.IsNullOrEmpty(userName))
                    IntegratedSecurity = string.Empty;
            }
        }

        string integratedSecurity;
        public string IntegratedSecurity
        {
            get { return integratedSecurity; }
            set
            {
                integratedSecurity = value;
                if (!string.IsNullOrEmpty(integratedSecurity))
                {
                    UserName = string.Empty;
                    Password = string.Empty;
                }
            }
        }

        public virtual string GetConnectionString()
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = serverName;
            conn.InitialCatalog = databaseName;
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
                conn.IntegratedSecurity = true;
            else
            {
                conn.UserID = UserName;
                conn.Password = Password;
            }
            return conn.ConnectionString;
        }
    }
}
