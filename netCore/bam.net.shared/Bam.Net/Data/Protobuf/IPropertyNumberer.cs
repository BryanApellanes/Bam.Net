using System;
using System.Reflection;

namespace Bam.Net.CoreServices.ProtoBuf
{
    public interface IPropertyNumberer
    {
        int GetNumber(Type type, PropertyInfo prop);
    }
}