/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.ExceptionHandling;
using Bam.Net.Analytics;

namespace Bam.Net.Analytics.Classification
{
    public abstract class Classifier
    {
        public Classifier()
        {
        }

        /// <summary>
        /// When implemented, should return the probablity that the specified
        /// document is in the specified category
        /// </summary>
        /// <param name="documentString"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public abstract float Probability(string documentString, string category);

        public abstract string Classify(string documentString, string defaultCategory = "None");

        IFeatureExtractor _featureExtractor;
        object _featureExtractorLock = new object();
        public IFeatureExtractor FeatureExtractor
        {
            get
            {
                return _featureExtractorLock.DoubleCheckLock(ref _featureExtractor, () => new WordFeatureExtractor());
            }
            set
            {
                _featureExtractor = value;
            }
        }

        /// <summary>
        /// Train the classifier assigning the specified doc to the 
        /// specified category
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="category"></param>
        public void Train(string doc, string category)
        {
            string[] features = ExtractFeatures(doc);
            for (int i = 0; i < features.Length; i++)
            {
                IncreaseFeature(features[i], category);
            }

            IncreaseCategory(category);
        }

        public float WeightedProbability(string feature, string category, float weight = 1, float assumedProbablity = 0.5F)
        {
            return WeightedProbability(feature, category, FeatureProbability, weight, assumedProbablity);
        }

        public float WeightedProbability(string feature, string category, Func<string, string, float> featureProbability, float weight = 1, float assumedProbablity = 0.5F)
        {
            float basicProb = featureProbability(feature, category);
            long featureCountInAllCategories = 0;
            Categories().Each(cat =>
            {
                featureCountInAllCategories += FeatureCount(feature, cat);
            });
            
            float weighted = ((weight*assumedProbablity)+(featureCountInAllCategories*basicProb))/(weight+featureCountInAllCategories);
            return weighted;
        }

        public virtual float FeatureProbability(string feature, string category)
        {
            long categorycount = DocumentsInCategoryCount(category);
            float result = 0;
            if (categorycount > 0)
            {
                long featureCount = FeatureCount(feature, category);
                long categoryCount = DocumentsInCategoryCount(category);
                if (featureCount > 0 && categoryCount > 0)
                {
                    result = featureCount / categoryCount;
                }
            }

            return result;
        }

        public string[] Categories()
        {
            CategoryCollection all = Category.Where(c => c.Id != null);
            return all.Select(c => c.Value).ToArray();
        }

        /// <summary>
        /// Total number of documents (corresponds to totalcount in Collective Intelligence chapter 6)
        /// </summary>
        /// <returns></returns>
        public long DocumentCount()
        {
            CategoryCollection all = Category.Where(c => c.Id != null);
            return (long)all.Select(c => c.DocumentCount).Sum();
        }

        /// <summary>
        /// Total number of documents in a category (corresponds to catcount in Collective Intelligence chapter 6)
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public long DocumentsInCategoryCount(string category)
        {
            Category cat = GetCategory(category);
            return (long)cat.DocumentCount;
        }

        /// <summary>
        /// Increase the count of a feature/category pair
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="category"></param>
        public void IncreaseFeature(string feature, string category)
        {
            Feature fRec = GetFeature(feature, category);
            fRec.FeatureToCategoryCount++;
            fRec.Save();
        }

        public void IncreaseCategory(string category)
        {
            Category cRec = GetCategory(category);
            cRec.DocumentCount++;
            cRec.Save();
        }

        public long FeatureCount(string feature, string category)
        {
            Category cRec = GetCategory(category);
            return FeatureCount(feature, cRec);
        }

        public long FeatureCount(string feature, Category cat)
        {
            Feature rec = Feature.OneWhere(fc => fc.Value == feature && fc.CategoryId == cat.Id);
            long result = 0;
            if (rec != null)
            {
                result = (long)rec.FeatureToCategoryCount;
            }

            return result;
        }


        /// <summary>
        /// Get a Category entry for the specified category, creating it if necessary
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        protected static Category GetCategory(string category)
        {
            Category c = Category.OneWhere(col => col.Value == category);
            if (c == null)
            {
                c = new Category
                {
                    Value = category,
                    DocumentCount = 0
                };
                c.Save();
            }
            return c;
        }

        protected static Feature GetFeature(string feature, string category)
        {
            Category c = GetCategory(category);
            return GetFeature(feature, c);
        }

        protected static Feature GetFeature(string feature, Category cat)
        {
            Feature f = Feature.OneWhere(col => col.Value == feature && col.CategoryId == cat.Id);
            if (f == null)
            {
                f = new Feature
                {
                    Value = feature,
                    CategoryId = cat.Id,
                    FeatureToCategoryCount = 0
                };
                f.Save();
            }

            return f;
        }

        object _extractFeaturesLock = new object();
        Func<string, string[]> _extractFeatures;
        /// <summary>
        /// The delegate used for extracting features from a
        /// given string.  Default is ExtractWords.
        /// </summary>
        public Func<string, string[]> ExtractFeatures
        {
            get
            {
                return _extractFeaturesLock.DoubleCheckLock(ref _extractFeatures, () => FeatureExtractor.ExtractFeatures);
            }
            set
            {
                _extractFeatures = value;
            }
        }
    }
}
