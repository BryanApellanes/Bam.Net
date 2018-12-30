using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public class Month
    {
        static Dictionary<int, string> _months = new Dictionary<int, string>
        {
            {1, "January" },
            {2, "February"},
            {3, "March" },
            {4, "April" },
            {5, "May" },
            {6, "June" },
            {7, "July" },
            {8, "August" },
            {9, "September" },
            {10, "October" },
            {11, "November" },
            {12, "December" }
        };

        public static string Name(int number)
        {
            Args.ThrowIf(number < 1 || number > 12, "Invalid month number specified");
            return _months[number];
        }

    }
}
