/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public enum JavascriptExecutionTypes
    {
        Invalid,
        OnWindowLoad, // this is really onwindowload
        OnWindowResize,
        OnWindowScroll,
        OnParse,
        Call
    }
}
