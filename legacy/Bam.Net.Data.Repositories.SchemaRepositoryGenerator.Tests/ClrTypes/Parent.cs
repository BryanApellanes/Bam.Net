﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes
{


    [Serializable]
    public class Parent
    {
        public ulong Id { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public virtual House[] Houses { get; set; } // many to many
        public virtual Daughter[] Daughters { get; set; } //one to many
        public virtual List<Son> Sons { get; set; }
    }
}
