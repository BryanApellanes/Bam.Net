/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.BoxControls;

namespace Naizari.Javascript.JsonControls
{
    public interface IJsonRightClickable
    {
        string RightClickMenuJsonId { get; set; }
        JsonContextMenu RightClickMenu { get; set; }
        bool IsRightClickable { get; }
    }
}
