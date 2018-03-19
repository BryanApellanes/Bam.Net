using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;

namespace Bam.Net.Data.Schema.GraphQL
{
    public abstract class GraphQLRazorTemplate<TModel> : RazorTemplate<TModel>
    {
        public void WriteGraphQLEndpointDefinition(GraphQLEndpoint endpointDefinition)
        {

        }
    }
}
