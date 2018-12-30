/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    internal class DaoParen : DaoSearchToken
    { }

    internal class DaoOpenParen: DaoParen
    {
        public override string ToString()
        {
            return "(";
        }
    }

    internal class DaoCloseParen : DaoSearchToken
    {
        public override string ToString()
        {
            return ")";
        }
    }
}
