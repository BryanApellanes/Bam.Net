/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KLGates.Data.Access;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using KLGates.Extensions;
using KLGates.Helpers;
using KLGates.Test;
using KLGates.Javascript.JsonControls;
using System.Web;
using KLGates.Data.Common;

namespace KLGates.Javascript.DataControls
{
    public abstract class DataControl<T>: JsonControl, IStateControl<T>
    {
        public DataControl()
            : base()
        {
        }

        #region IStateControl Members

        public virtual string Name
        {
            get
            {
                Expect.IsNotNullOrEmpty(this.jsonId, "The JsonId property was not set: " + this.ToString());
                if (HttpContext.Current != null)
                    return HttpContextHelper.AbsolutePageUrl + "?" + this.jsonId;
                else
                    return this.JsonId;
            }            
        }

        public T UserState
        {
            get
            {
                Expect.IsNotNullOrEmpty(this.Name);
                return SerializedObjectManager.Current.RetrieveUserObject<T>(this.Name);
            }
            set
            {
                Expect.IsNotNullOrEmpty(this.Name);
                SerializedObjectManager.Current.SaveUserObject(this.Name, value);
            }
        }

        public T AppState
        {
            get
            {
                Expect.IsNotNullOrEmpty(this.Name);
                return SerializedObjectManager.Current.RetrieveAppObject<T>(this.Name);
            }
            set
            {
                Expect.IsNotNullOrEmpty(this.Name);
                SerializedObjectManager.Current.RetrieveAppObject<T>(this.Name);
            }
        }

        #endregion
    }
}
