/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Permissions;
using System.ComponentModel;

namespace Naizari.Roles
{
    public class RunAsContext: IDisposable
    {
        public RunAsContext(WindowsImpersonationContext context, IntPtr tokenHandle)
        {
            Context = context;
            TokenHandle = tokenHandle;
        }

        public WindowsImpersonationContext Context
        {
            get;
            set;
        }

        public IntPtr TokenHandle
        {
            get;
            set;
        }

        #region IDisposable Members

        public void Dispose()
        {
            RunAs.UndoImpersonate(this);
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        #endregion
    }
}
