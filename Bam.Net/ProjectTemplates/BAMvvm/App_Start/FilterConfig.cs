/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Web;
using System.Web.Mvc;

namespace BAMvvm
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}