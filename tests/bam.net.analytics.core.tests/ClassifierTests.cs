using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Analytics.Classification;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Analytics.Tests
{
    [Serializable]
    public class ClassifierTests: CommandLineTool
    {
        [UnitTest]
        public void ClassifierTest()
        {
            Db.For<Feature>(new SQLiteDatabase(nameof(ClassifierTest)));
            Db.EnsureSchema<Feature>();

            When.A<NaiveBayesClassifier>("is trained", (classifier) =>
            {
                classifier.Train("The quick brown fox jumps over the lazy dog", "good");
                classifier.Train("make quick money in the online casino", "bad");
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
    }
}
