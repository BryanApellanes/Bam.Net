using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    public class Hydrator : IHydrator
    {
        static Hydrator()
        {
            DefaultHydrator = new Hydrator();
        }

        public Hydrator()
        {
            Logger = Log.Default;
        }

        public static Hydrator DefaultHydrator { get; set; }

        public ILogger Logger { get; set; }

        public bool TryHydrate(Dao dao, Database database = null)
        {
            try
            {
                Hydrate(dao, database);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception hydrating dao of type ({0}): {1}", ex, dao?.GetType()?.Name, ex.Message);
                return false;
            }
        }

        public void Hydrate(Dao dao, Database database = null)
        {
            foreach (string key in dao.ChildCollections.Keys)
            {
                dao.ChildCollections?[key].Load(database);
            }
        }
    }
}
