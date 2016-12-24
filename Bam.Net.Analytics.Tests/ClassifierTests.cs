using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Analytics.Classification;
using Bam.Net.Data;
using Bam.Net.Testing;

namespace Bam.Net.Analytics.Tests
{
    [Serializable]
    public class ClassifierTests: CommandLineTestInterface
    {

        [UnitTest]
        public void ClassifierTest()
        {
            SQLiteRegistrar.Register<Feature>();
            Db.EnsureSchema<Feature>();

            When.A<NaiveBayesClassifier>("is trained", (classifer) =>
            {
                classifer.Train("The quick brown fox jumps over the lazy dog", "good");
                classifer.Train("make quick money in the online casino", "bad");
            })
            .TheTest
            .ShouldPass(because =>
            {
                Classifier classifier = because.ObjectUnderTest<Classifier>();
                long quickGood = classifier.FeatureCount("quick", "good");
                long quickBad = classifier.FeatureCount("quick", "bad");
                because.ItsTrue("The good count of 'quick' is " + quickGood, quickGood > 0);
                because.ItsTrue("The bad count of 'quick' is " + quickBad, quickBad > 0);
            })
            .SoBeHappy(c =>
            {
            })
            .UnlessItFailed();
        }

        //[UnitTest]
        //public void ClassifyTest()
        //{
        //    SqlClientRegistrar.Register<Feature>();
        //    _.TryEnsureSchema<Feature>();
        //    string resultOne = "";
        //    string resultTwo = "";
        //    NaiveBayesClassifier classifier = new NaiveBayesClassifier();

        //}

        private void SampleTrain(Classifier classifier)
        {
            classifier.Train("Nobody owns the water.", "good");
            classifier.Train("the quick rabbit jumps fences", "good");
            classifier.Train("buy phaarmaceuticals now", "bad");
            classifier.Train("make quick money at the online casino", "bad");
            classifier.Train("the quick brown fox jumps", "good");
        }

        private static void Init()
        {
            SQLiteRegistrar.Register<Url>();
            Db.TryEnsureSchema<Url>();
        }
    }
}
