/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Schema;
using Bam.Net.Server.Renderers;
using System.Reflection;
using System.IO;

namespace Bam.Net.Server
{
    /// <summary>
    /// Class used to initialize dust templates for all 
    /// Dao components
    /// </summary>
    public class DustTemplateInitializer: TemplateInitializer
    {
        public DustTemplateInitializer(BamServer server) : base(server) { }

        object _initializeLock = new object();
        public override void Initialize()
        {
            OnInitializing();
            lock (_initializeLock)
            {
                try
                {                   
					RenderCommonTemplates();
					RenderAppTemplates();
                }
                catch (Exception ex)
                {
                    OnInitializationException(ex);
                }
            }

            OnInitialized();
        }

		/// <summary>
		/// Render templates for all the currently registered application DaoProxyRegistrations
		/// </summary>
		public override void RenderAppTemplates()
		{
			//      App
			Server.DaoResponder.AppDaoProxyRegistrations.Keys.Each((appName) =>
			{
				if (Server.ContentResponder.AppContentResponders.ContainsKey(appName))
				{
					AppTemplateRenderer appRenderer = new AppTemplateRenderer(Server.ContentResponder.AppContentResponders[appName]);
					Server.DaoResponder.AppDaoProxyRegistrations[appName].Each((daoProxyReg) =>
					{
						OnInitializingAppDaoTemplates(appName, daoProxyReg);

						RenderEachTable(appRenderer, daoProxyReg);

						OnInitializedAppDaoTemplates(appName, daoProxyReg);
					});
				}
			});
		}

		/// <summary>
		/// Render templates for all the currently registered common DaoProxyRegistrations
		/// </summary>
		public override void RenderCommonTemplates()
		{ 
			// get the types that need templates
			//  from DaoResponder
			//      Common
			CommonTemplateRenderer commonRenderer = new CommonTemplateRenderer(Server.ContentResponder);

			Server.DaoResponder.CommonDaoProxyRegistrations.Values.Each((daoProxyReg) =>
			{
				OnInitializingCommonDaoTemplates(daoProxyReg);

				RenderEachTable(commonRenderer, daoProxyReg);

				OnInitializedCommonDaoTemplates(daoProxyReg);
			});
		}

    }
}
