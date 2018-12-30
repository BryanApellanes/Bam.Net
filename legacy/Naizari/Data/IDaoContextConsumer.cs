/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Naizari.Data
{
    public interface IDaoContextConsumer
    {
        string ContextName { get; set; }
        event DaoContextConnectionStringSetEventHandler DaoContextConnectionStringSet;
        string DatabaseName { get; set; }
        string GetConnectionString();
        string ServerName { get; set; }
        void SetConnectionString();

        string UserName { get; set; }
        string Password { get; set; }
        string IntegratedSecurity { get; set; }
    }
}
