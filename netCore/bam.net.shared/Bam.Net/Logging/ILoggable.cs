/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public interface ILoggable
    {
        ILogger[] Subscribers
        {
            get;
        }
        /// <summary>
        /// When implemented in a derived class, subscribes 
        /// the specified logger to log the inner events 
        /// of the derived class.
        /// </summary>
        /// <param name="logger"></param>
        void Subscribe(ILogger logger);

        /// <summary>
        /// When implemented in a derived class, 
        /// should return true if the specified 
        /// logger is already subscribed
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        bool IsSubscribed(ILogger logger);
    }
}
