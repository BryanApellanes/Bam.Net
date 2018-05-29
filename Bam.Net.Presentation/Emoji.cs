using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation.Unicode
{
    public partial class Emoji
    {
        public override void OnInitialize()
        {
            DefaultSortProperty = "ShortName";
            base.OnInitialize();
        }
    }
}
