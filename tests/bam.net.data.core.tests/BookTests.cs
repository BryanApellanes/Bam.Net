using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    public class BookTests : CommandLineTool
    {
        [UnitTest]
        public void BookPageCountTest()
        {
            Book<int?> testBook = new Book<int?>(GetTestObjects(13))
            {
                PageSize = 10
            };
            Expect.AreEqual(2, testBook.PageCount);
            Expect.AreEqual(10, testBook[0].Count);
            Expect.AreEqual(3, testBook[1].Count);
        }

        private List<int?> GetTestObjects(int count)
        {
            List<int?> result = new List<int?>();
            for (int i = 0; i < count; i++)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
