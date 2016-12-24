using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ProtoBuf
{
    /// <summary>
    /// An IPropertyNumberer implementation that keeps
    /// a map of property numbers in memory
    /// </summary>
    public class InMemoryPropertyNumberer : IPropertyNumberer
    {
        Dictionary<Type, int> _typePropertyNumbers;
        public InMemoryPropertyNumberer()
        {
            _typePropertyNumbers = new Dictionary<Type, int>();
        }
        public int GetNumber(Type type, PropertyInfo prop) // prop not used in this implementation
        {
            if (!_typePropertyNumbers.ContainsKey(type))
            {
                _typePropertyNumbers.Add(type, 0);
            }

            return ++_typePropertyNumbers[type];
        }
    }
}
