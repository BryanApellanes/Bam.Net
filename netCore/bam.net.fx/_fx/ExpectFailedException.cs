/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace Bam.Net
{
    [Serializable]
    public partial class ExpectFailedException: Exception, ISerializable
    {  
        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Message", this.Message);
        }

        #endregion
    }
}
