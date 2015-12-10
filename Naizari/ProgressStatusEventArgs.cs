/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari
{
    public class ProgressStatusEventArgs: EventArgs
    {
        public ProgressStatusEventArgs(ProgressStatus status)
        {
            ProgressStatus = status;
        }

        public ProgressStatus ProgressStatus { get; set; }
    }
}
