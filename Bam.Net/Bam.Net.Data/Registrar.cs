/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class Registrar
    {
        static Action<string> _current;
        static object _currentLock = new object();
        public static Action<string> Default
        {
            get
            {
                if (_current == null)
                {
                    throw new InvalidOperationException("Default Registrar not specified");
                }

                return _current;
            }
            set
            {
                _current = value;
            }
        }     
    }
}
