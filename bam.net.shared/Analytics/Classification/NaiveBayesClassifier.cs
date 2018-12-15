/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics.Classification
{
    public class NaiveBayesClassifier: Classifier
    {
        Dictionary<string, float> _thresholds;
        public NaiveBayesClassifier()
        {
            this._thresholds = new Dictionary<string, float>();
        }

        /// <summary>
        /// Attempts to classify the specified document taking into 
        /// consideration the threshold set by SetThreshold, default is 1.
        /// </summary>
        /// <param name="documentString"></param>
        /// <param name="defaultCategory"></param>
        /// <returns></returns>
        public override string Classify(string documentString, string defaultCategory = "None")
        {
            float max = -1F; // ensure that best and max will always at least be set to the first value
            string best = defaultCategory;
            Dictionary<string, float> probabilities = new Dictionary<string, float>();
            Categories().Each(cat =>
            {
                probabilities.Add(cat, Probability(documentString, cat));
                if (probabilities[cat] > max)
                {
                    max = probabilities[cat];
                    best = cat;
                }
            });

            foreach (string category in probabilities.Keys)
            {
                if (category.Equals(best))
                {
                    continue;
                }
                if (probabilities[category] * GetThreshold(best) > probabilities[best])
                {
                    return defaultCategory;
                }
            }

            return best;
        }
        
        /// <summary>
        /// For a new document to be classified into a particular category, its 
        /// probability must be a specified amount larger than the probablity
        /// for any other category.  This specified amount is the threshold.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="threshold"></param>
        public void SetThreshold(string category, float threshold)
        {
            if (!_thresholds.ContainsKey(category))
            {
                _thresholds.Add(category, threshold);
            }
            else
            {
                _thresholds[category] = threshold;
            }
        }

        public float GetThreshold(string category)
        {
            return _thresholds.ContainsKey(category) ? _thresholds[category] : 1.0F;
        }

        public override float Probability(string documentString, string category)
        {
            long documentsInCategoryCount = DocumentsInCategoryCount(category);
            long documentCount = DocumentCount();
            float catProb = (float)documentsInCategoryCount / (float)documentCount;
            float docProb = DocumentProbability(documentString, category);
            return docProb * catProb;
        }

        protected float DocumentProbability(string documentString, string category)
        {
            string[] features = ExtractFeatures(documentString);
            float probablity = 1F;
            features.Each(feature => probablity *= WeightedProbability(feature, category));
            return probablity;
        }
    }
}
