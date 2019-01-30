using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// When implemented describes the properties
    /// of a type that comprise it's composite key
    /// which uniquely identifies it
    /// </summary>
    public interface IHasKeyHash
    {
        string[] CompositeKeyProperties { get; set; }
        int GetIntKeyHash();
        ulong GetULongKeyHash();
    }
}
