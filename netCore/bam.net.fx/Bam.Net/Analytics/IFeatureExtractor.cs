using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics
{
    public interface IFeatureExtractor
    {
        string[] ExtractFeatures(string value);
    }
}
