/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.ExceptionHandling
{
    /// <summary>
    /// A Class responsible for arbitrating exceptions
    /// </summary>
    public class ExceptionArbiter
    {
        public ExceptionArbiter()
        {
            Logger = Log.Default;
        }
        public ILogger Logger { get; set; }
        public void Catch(object thrower, Exception ex)
        {
            Logger.AddEntry("Exception: {0}", ex, ex.Message);

            CaughtException?.Invoke(thrower, new ExceptionEventArgs(ex));
            Arbitrate(thrower, ex);
        }

        public virtual void Arbitrate(object thrower, Exception ex)
        {
            throw ex;
        }

        public event ExceptionHandler CaughtException;
    }
}
