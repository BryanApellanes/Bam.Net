using Bam.Net.Data.Schema.Handlebars;
using System;
using System.Collections.Generic;
using Bam.Net.Presentation.Handlebars;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public partial class DaoGenerator
    {
        public DaoGenerator(IDaoCodeWriter codeWriter = null, IDaoTargetStreamResolver targetStreamResolver = null)
        {
            DisposeOnComplete = true;
            SubscribeToEvents();

            Namespace = "DaoGenerated";
            TargetStreamResolver = targetStreamResolver ?? new DaoTargetStreamResolver();
            DaoCodeWriter = codeWriter ?? new HandlebarsDaoCodeWriter(new HandlebarsDirectory("./Handlebars"), new HandlebarsEmbeddedResources(GetType().Assembly));
        }
    }
}
