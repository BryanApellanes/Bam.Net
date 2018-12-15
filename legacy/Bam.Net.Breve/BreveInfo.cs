/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Bam.Net.Breve
{
    public class BreveInfo
    {
        public BreveInfo(string className)
        {
            this.ClassName = className;
            this._properties = new List<BreveProperty>();
        }

        public BreveInfo(string className, JObject obj, Languages lang)
            : this(className)
        {
            this.AddProperties(obj.Properties());
            this.Language = lang;
        }
        public Languages Language { get; set; }
        public string ClassName { get; set; }

        List<BreveProperty> _properties;
        public BreveProperty[] Properties
        {
            get
            {
                return _properties.ToArray();
            }
            private set
            {
                _properties = new List<BreveProperty>(value);
            }
        }

        public void AddProperties(IEnumerable<JProperty> properties)
        {
            properties.Each(p =>
            {
                AddProperty(p);
            });
        }

        public void AddProperty(JProperty property)
        {
            string propertyType = (string)property.Value;
            if (!propertyType.Equals("object", StringComparison.InvariantCultureIgnoreCase))
            {
                Data.DataTypes dataType = propertyType.ToEnum<Data.DataTypes>();
                propertyType = BreveTypes.Map[Language, dataType];
            }
            string propertyName = property.Name;
            BreveProperty breveProperty = new BreveProperty(ClassName, propertyName, propertyType);
            _properties.Add(breveProperty);
        }
    }
}
