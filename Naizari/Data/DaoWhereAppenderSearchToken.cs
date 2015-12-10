/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    internal class DaoWhereAppenderSearchToken : DaoSearchToken
    {
        WhereAppender appender;
        public DaoWhereAppenderSearchToken(WhereAppender appender)
        {
            this.appender = appender;
        }

        public override string ToString()
        {
            return " " + appender.ToString() + " ";
        }
    }
}
