/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class OpenParen: FilterToken
    {
        public OpenParen()
        {
        }

        public override string ToString()
        {
            return "(";
        }
    }
}
