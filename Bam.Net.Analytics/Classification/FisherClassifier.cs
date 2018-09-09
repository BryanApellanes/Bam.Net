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
    public class FisherClassifier: Classifier
    {
        Dictionary<string, float> _minimums;
        public FisherClassifier()
        {
            this._minimums = new Dictionary<string, float>();
        }

        public override string Classify(string documentString, string defaultCategory = "None")
        {
            string best = defaultCategory;
            float max = 0.0F;
            foreach(string category in Categories())
            {
                float prob = Probability(documentString, category);
                if (prob > GetMinimum(category) && prob > max)
                {
                    best = category;
                    max = prob;
                }
            }

            return best;
        }

        /// <summary>
        /// Sets the minimum probability score for the specified
        /// category.  The probablity score for a document must
        /// be greater than the specified minimum for the category
        /// for the document to be classified as being in 
        /// the category.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="min"></param>
        public void SetMinimum(string category, float min)
        {
            if (!_minimums.ContainsKey(category))
            {
                _minimums.Add(category, min);
            }
            else
            {
                _minimums[category] = min;
            }
        }

        public float GetMinimum(string category)
        {
            return _minimums.ContainsKey(category) ? _minimums[category] : 0;
        }

        private float InverseChiSquare(float chi, int df)
        {
            float m = chi / 2.0F;
            float sum = (float)Math.Exp(-m);
            float term = sum;
            for (int i = 1; i < Math.Floor(df / 2F); i++)
            {
                term *= m / i;
                sum += term;
            }

            return Math.Min(sum, 1.0F);
        }

        public override float Probability(string documentString, string category)
        {
            float prob = 1F;
            string[] features = ExtractFeatures(documentString);
            features.Each(feature =>
            {
                prob *= (WeightedProbability(feature, category, CategoryProbability));
            });

            float score = (float)(-2 * Math.Log(prob));
            return InverseChiSquare(score, features.Length * 2);
        }

        public float CategoryProbability(string feature, string category)
        {            
            float freq = FeatureProbability(feature, category);
            if(freq == 0) return 0;

            float freqSum = 0.0F;
            Categories().Each(cat =>
            {
                freqSum += FeatureProbability(feature, category);
            });

            float prob = freq / freqSum;
            return prob;
        }
    }
}
