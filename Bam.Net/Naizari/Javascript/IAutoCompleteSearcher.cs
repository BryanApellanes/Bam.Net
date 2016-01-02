/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Naizari.Javascript
{
    public interface IAutoCompleteSearcher
    {
        [JsonMethod]
        AutoCompleteItem[] Search(string input);
    }
}
