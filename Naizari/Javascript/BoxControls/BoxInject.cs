/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.BoxControls
{
    /// <summary>
    /// Used to designate a method defined in an ascx.cs as 
    /// a required injection method.  The method will
    /// be called by the box server prior to calling the
    /// BoxInit method of the BoxUserControl.  The codebehind should 
    /// extend BoxUserControl instead of UserControl.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BoxInject: Attribute
    {

    }
}
