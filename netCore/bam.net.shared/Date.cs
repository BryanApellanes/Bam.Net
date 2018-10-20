using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public class Date
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }

        public static Date FromInstant(Instant instant)
        {
            return FromDateTime(instant.ToDate());
        }

        public static Date FromDateTime(DateTime dateTime)
        {
            return new Date { Month = dateTime.Month, Day = dateTime.Day, Year = dateTime.Year };
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        public override bool Equals(object obj)
        {
            if (obj is Date compareTo)
            {
                return compareTo.Month == Month && compareTo.Day == Day && compareTo.Year == Year;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(Month, Day, Year);
        }

        public override string ToString()
        {
            return $"{Month}/{Day}/{Year}";
        }
    }
}
