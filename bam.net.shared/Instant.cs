/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    /// <summary>
    /// A portable moment in time down to the millisecond
    /// </summary>
    public class Instant
    {
        public Instant()
        {
            this.Initialize();
        }

        public Instant(DateTime value)
        {
            this.Initialize(value);
        }

        private void Initialize()
        {
            Initialize(DateTime.UtcNow);
        }

        private void Initialize(DateTime value)
        {
            this.Month = value.Month;
            this.Day = value.Day;
            this.Year = value.Year;
            this.Hour = value.Hour;
            this.Minute = value.Minute;
            this.Second = value.Second;
            this.Millisecond = value.Millisecond;
        }

        public static implicit operator DateTime(Instant instant)
        {
            return instant.ToDateTime();
        }

        public override string ToString()
        {
            return $"{Month}/{Day}/{Year};{Hour}.{Minute}.{Second}.{Millisecond}";
        }

        public string ToString(Func<Instant, string> toStringer)
        {
            return toStringer(this);
        }

        public static string ToString(DateTime dateTime, Func<Instant, string> toStringer)
        {
            return new Instant(dateTime).ToString(toStringer);
        }

        public static Instant FromDateString(string dateString)
        {
            string[] monthDayYear = dateString.DelimitSplit("/");
            if (monthDayYear.Length != 3)
            {
                Throw();
            }
            if (!int.TryParse(monthDayYear[0], out int month))
            {
                Throw();
            }
            if (!int.TryParse(monthDayYear[1], out int day))
            {
                Throw();
            }
            if(!int.TryParse(monthDayYear[2], out int year))
            {
                Throw();
            }
            return new Instant(new DateTime(year, month, day));
        }

        public static Instant FromString(string instantString)
        {
            Parse(instantString, out int month, out int day, out int year, out int hour, out int minute, out int second, out int millisecond);

            return new Instant(new DateTime(year, month, day, hour, minute, second, millisecond));
        }

        private static void Parse(string instantString, out int month, out int day, out int year, out int hour, out int minute, out int second, out int millisecond)
        {
            string[] dateAndTime = instantString.DelimitSplit(";");
            if (dateAndTime.Length != 2)
            {
                Throw();
            }

            string dateString = dateAndTime[0];
            string timeString = dateAndTime[1];
            string[] monthDayYear = dateString.DelimitSplit("/");
            if (monthDayYear.Length != 3)
            {
                Throw();
            }

            string[] hourMinSecMil = timeString.DelimitSplit(".");
            if (hourMinSecMil.Length != 4)
            {
                Throw();
            }

            if (!int.TryParse(monthDayYear[0], out month))
            {
                Throw();
            }

            if (!int.TryParse(monthDayYear[1], out day))
            {
                Throw();
            }

            if (!int.TryParse(monthDayYear[2], out year))
            {
                Throw();
            }

            if (!int.TryParse(hourMinSecMil[0], out hour))
            {
                Throw();
            }

            if (!int.TryParse(hourMinSecMil[1], out minute))
            {
                Throw();
            }

            if (!int.TryParse(hourMinSecMil[2], out second))
            {
                Throw();
            }

            if (!int.TryParse(hourMinSecMil[3], out millisecond))
            {
                Throw();
            }
        }

        private static void Throw()
        {
            throw new ArgumentException("The specified string was not a recognized instant string");
        }

        public int DiffInMinutes(Instant value)
        {
            return DiffInMinutes(value.ToDateTime());
        }

        public int DiffInMinutes(DateTime value)
        {
            return TimeSpan.FromMilliseconds(DiffInMilliseconds(value)).Minutes;
        }

        /// <summary>
        /// Returns the difference between the current instant 
        /// and the specified value in milliseconds
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int DiffInMilliseconds(Instant value)
        {
            return DiffInMilliseconds(value.ToDateTime());
        }
        
        /// <summary>
        /// Returns the difference between the current instant 
        /// and the specified value in milliseconds
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int DiffInMilliseconds(DateTime value)
        {
            int diff = (int)this.ToDateTime().Subtract(value).TotalMilliseconds;
            if (diff < 0)
            {
                diff = diff * -1;
            }

            return diff;
        }

        public int DiffInSeconds(Instant value)
        {
            return DiffInSeconds(value.ToDateTime());
        }

        public int DiffInSeconds(DateTime value)
        {
            int diff = (int)this.ToDateTime().Subtract(value).TotalSeconds;
            if (diff < 0)
            {
                diff = diff * -1;
            }

            return diff;
        }

        /// <summary>
        /// Return a DateTime object representing only the Date
        /// </summary>
        /// <returns></returns>
        public DateTime ToDate()
        {
            return ToDateTime().Date;
        }

        /// <summary>
        /// Returns the current instant in the format "{Month}/{Day}/{Year}"
        /// </summary>
        /// <returns></returns>
        public string ToDateString()
        {
            return $"{Month}/{Day}/{Year}";
        }

        /// <summary>
        /// Return a DateTime object representing the current Instant instance
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime(DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond, kind);
        }

        public DateTime ToLocalTime()
        {
            return ToDateTime(DateTimeKind.Utc).ToLocalTime();
        }

        public string ToJavascriptDate()
        {
            return $"new Date({Year}, {Month}, {Day}, {Hour}, {Minute}, {Second}, {Millisecond});";
        }

        public int Month
        {
            get;
            set;
        }

        public int Day
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }

        public int Hour
        {
            get;
            set;
        }

        public int Minute
        {
            get;
            set;
        }

        public int Second
        {
            get;
            set;
        }
        public int Millisecond
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }
    }
}
