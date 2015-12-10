/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Extensions
{
    public static class EnumExtensions
    {
        public static bool TryParseAs<EnumType>(string stringToParse, out EnumType enumInstance)
        {
            try
            {
                enumInstance = (EnumType)Enum.Parse(typeof(EnumType), stringToParse);
                return true;
            }
            catch// (Exception ex)
            {
                enumInstance = default(EnumType);
                return false;
            }
        }
    }
}
