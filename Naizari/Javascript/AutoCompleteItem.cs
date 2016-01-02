/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript
{
    /// <summary>
    /// An array of objects of this type should be
    /// returned by IAutoCompleteSearcher implementations.
    /// </summary>
    public class AutoCompleteItem
    {
        public AutoCompleteItem(string dataId, string displayName, bool isNonResult)
        {
            DataId = dataId;
            DisplayName = displayName;
            IsNonResult = isNonResult;
        }

        public string DataId { get; set; }
        public string DisplayName { get; set; }
        public bool IsNonResult { get; set; }
    }
}
