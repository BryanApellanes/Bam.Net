/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net.Razor
{
    public abstract class RazorBaseTemplate
    {
        static readonly Dictionary<ProcessModes, Action<string>> _inspectors;        
        static RazorBaseTemplate()
        {
            _inspectors = new Dictionary<ProcessModes, Action<string>>
            {
                { ProcessModes.Dev, (s) => { Console.WriteLine(s); } },
                { ProcessModes.Test, (s) => {Console.WriteLine(s); } },
                { ProcessModes.Prod, (s) => {Console.WriteLine("Parsing Razor Template..."); } }
            };
        }

        public StringBuilder Generated { get; protected set; }

        public abstract void Execute();

        public virtual void Write(object value)
        {
            WriteLiteral(value);
        }

        public virtual void WriteLiteral(object value)
        {
            Generated.Append(value);
        }

        static Action<string> _defaultInspector;
        static object _defaultInspectorLock = new object();
	    /// <summary>
	    /// The default Action that will be given razor parse results; primarily for
		/// debugging
	    /// </summary>
        public static Action<string> DefaultInspector
        {
            get
            {
                return _defaultInspectorLock.DoubleCheckLock(ref _defaultInspector, () => _inspectors[ProcessMode.Current.Mode]);
            }
            set
            {
                _defaultInspector = value;
            }
        }        
    }
}
