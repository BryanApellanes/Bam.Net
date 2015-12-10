/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;

namespace Naizari.Data.Paging
{
    public class CustomIndexes
    {
        // propertyName, TDao, CustomIndex<TDao>
        Dictionary<string, Dictionary<Type, object>> indexesByProperty;
        public CustomIndexes()
        {
            indexesByProperty = new Dictionary<string, Dictionary<Type, object>>();
        }

        public CustomIndex<TDao> GetIndex<TDao>(DatabaseAgent agent, string propertyName) where TDao : DaoObject, new()
        {
            if (!this.indexesByProperty.ContainsKey(propertyName))
                this.indexesByProperty.Add(propertyName, new Dictionary<Type, object>());

            if (!this.indexesByProperty[propertyName].ContainsKey(typeof(TDao)))
                this.indexesByProperty[propertyName].Add(typeof(TDao), new CustomIndex<TDao>(agent, propertyName));

            return (CustomIndex<TDao>)this.indexesByProperty[propertyName][typeof(TDao)];
        }

        public static CustomIndexes Current
        {
            get
            {
                return SingletonHelper.GetApplicationProvider<CustomIndexes>(new CustomIndexes());
            }
        }
    }
}
