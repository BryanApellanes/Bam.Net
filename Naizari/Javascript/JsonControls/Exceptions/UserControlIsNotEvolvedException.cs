/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class UserControlIsNotBoxUserControlException: JsonException
    {
        public UserControlIsNotBoxUserControlException(string ascxPath)
            : base(string.Format("The specified ascx file ['{0}'] is not a BoxUserControl.  Make sure the specified ascx file extends BoxUserControl instead of UserControl.", ascxPath))
        { }
    }
}
