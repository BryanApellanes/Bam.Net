using Bam.Net;
using Bam.Net.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static partial class Extensions // core
    {
        public static object ToDynamicData(this object instance)
        {
            Args.ThrowIfNull(instance, "instance");
            return ToDynamicData(instance, instance.GetType().Name);
        }

        public static object ToDynamicData(this object instance, string typeName)
        {
            Args.ThrowIfNull(instance);
            Type type = instance.GetType();
            JObject jObject = new JObject();
            foreach(PropertyInfo prop in type.GetProperties().Where(PropertyDataTypeFilter))
            {
                jObject.Add(prop.Name, new JObject(prop.GetValue(instance)));
            }
            return jObject;
        }
    }
}
