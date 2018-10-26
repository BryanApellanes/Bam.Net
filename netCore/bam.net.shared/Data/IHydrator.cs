using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data
{
    public interface IHydrator
    {
        bool TryHydrate(Dao dao, Database database = null);
        void Hydrate(Dao dao, Database databse = null);
    }
}
