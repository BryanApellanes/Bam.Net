using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics
{
    public class WordFeatureExtractor : IFeatureExtractor
    {
        public string[] ExtractFeatures(string value)
        {
            string[] words = value.Split(new string[] { " ", "\r", "\n", "\t", ".", ",", ":", ";", "=", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", "/", ">", "<", "?", "@", "[", "]", "{", "}", "|", "`", "^", "~" }, StringSplitOptions.RemoveEmptyEntries);
            return words.Where(s => s.Length > 2 && s.Length < 20).Distinct().ToArray();
        }
    }
}
