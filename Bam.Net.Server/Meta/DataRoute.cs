using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    public class DataQueryRoute: DataRoute
    {
        public DataQueryRoute()
        {
            Route = "{TypeName}.{Ext}?{QueryString}";
        }
        
        public string QueryString { get; set; }
        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "TypeName", "Ext", "QueryString" };
            }
        }
    }

    public class DataInstanceRoute: DataRoute
    {
        public DataInstanceRoute()
        {
            Route = "{TypeName}/{Id}.{Ext}";
        }
        public string Id { get; set; }
        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "TypeName", "Ext", "Id" };
            }
        }
    }

    public class DataListRoute: DataRoute
    {
        public DataListRoute()
        {
            Route = "{TypeName}/{Id}/{ChildListProperty}.{Ext}";
        }
        public string Id { get; set; }
        public string ChildListProperty { get; set; }
        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "TypeName", "Ext", "Id", "ChildListProperty" };
            }
        }
    }

    public abstract class DataRoute: IHasRequiredProperties
    {
        // /{Type}.{ext}?{Query}
        // /{Type}/{Id}.{ext}
        // /{Type}/{Id}/{ChildListProperty}.{ext} 
        public string Route { get; protected set; }
        public string TypeName { get; set; }
        public string Ext { get; set; }
        public abstract string[] RequiredProperties { get; }
        public bool IsValid()
        {
            bool allGood = true;
            foreach(string property in RequiredProperties)
            {
                if (!allGood)
                {
                    break;
                }
                allGood = !string.IsNullOrEmpty((string)this.Property(property));
            }
            return allGood;
        }
        public bool Parse(string pathAndQuery)
        {
            Type currentType = GetType();
            DataRoute instance = (DataRoute)currentType.Construct();
            RouteParser parser = new RouteParser(instance.Route);
            Dictionary<string, string> values = parser.ParseRouteInstance(pathAndQuery);
            instance = (DataRoute)values.ToInstance(currentType);
            if (instance.IsValid())
            {
                this.CopyProperties(instance);
                return true;
            }
            return false;
        }
    }
}
