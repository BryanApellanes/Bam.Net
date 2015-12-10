/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    public delegate void DaoValueChangedEventHandler(DaoObject sender, DaoValueChangedEventArgs e);

    public delegate void DaoObjectEventHandler(DaoObject sender);

    public delegate void DaoObjectEventHandler<T>(T sender) where T: DaoObject, new();
}
