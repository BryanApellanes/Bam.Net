/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    public enum UpdateResult
    {
        None,
        /// <summary>
        /// Update was successful.
        /// </summary>
        Success,

        /// <summary>
        /// An error occurred, check the property DaoObject.LastException for more info
        /// </summary>
        Error,

        /// <summary>
        /// The record in the database is different from the current DaoObject instance.
        /// </summary>
        RecordAltered,
        NoChangesToCommit
    }
}
