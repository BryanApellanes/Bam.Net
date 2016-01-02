/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Naizari.Data.Access;
namespace Naizari.Javascript.DataControls
{
    public interface IDataControl
    {
        Naizari.Data.Access.SQLiteAgent ApplicationDatabase { get; }
        string ApplicationPath { get; }
        CrossSessionMode CrossSessionMode { get; set; }
        string PageUrl { get; }
        Naizari.Data.Access.SQLiteAgent UserDatabase { get; }
        string JsonId { get; }
        DatabaseAgent GetAgent();
    }
}
