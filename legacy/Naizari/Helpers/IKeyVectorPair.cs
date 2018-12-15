/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Naizari.Helpers
{
    interface IKeyVectorPair
    {
        string IV { get; set; }
        string Key { get; set; }
        void Save(string filePath);
    }
}
