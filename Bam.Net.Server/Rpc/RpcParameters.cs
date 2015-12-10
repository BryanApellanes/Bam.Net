using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcParameters
    {
        /// <summary>
        /// Describes the parameters as being 
        /// by Position or by name
        /// </summary>
        public class Structure
        {
            public object[] Position { get; set; }
            public object Name { get; set; }
        }

        public RpcParameters()
        {
            this.By = new Structure();
        }

        public bool Ordered
        {
            get
            {
                return this.By.Position != null;
            }
        }

        public bool Named
        {
            get
            {
                return this.By.Name != null;
            }
        }

        public Structure By { get; set; }
    }
}
