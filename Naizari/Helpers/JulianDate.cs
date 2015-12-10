/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Naizari.Helpers
{
    public static class JulianDate
    {
        public static DateTime ToDateTime(this double jd)
        {
            double j1, j2, j3, j4, j5; //scratch

            //
            // get the date from the Julian day number
            //
            double intgr = Math.Floor(jd);
            double frac = jd - intgr;
            double gregjd = 2299161;
            if (intgr >= gregjd)
            { //Gregorian calendar correction
                double tmp = Math.Floor(((intgr - 1867216) - 0.25) / 36524.25);
                j1 = intgr + 1 + tmp - Math.Floor(0.25 * tmp);
            }
            else
            {
                j1 = intgr;
            }
            //correction for half day offset
            double dayfrac = frac + 0.5;
            if (dayfrac >= 1.0)
            {
                dayfrac -= 1.0;
                ++j1;
            }

            j2 = j1 + 1524;
            j3 = Math.Floor(6680.0 + ((j2 - 2439870) - 122.1) / 365.25);
            j4 = Math.Floor(j3 * 365.25);
            j5 = Math.Floor((j2 - j4) / 30.6001);

            double d = Math.Floor(j2 - j4 - Math.Floor(j5 * 30.6001));
            double m = Math.Floor(j5 - 1);
            if (m > 12) m -= 12;
            double y = Math.Floor(j3 - 4715);
            if (m > 2) --y;
            if (y <= 0) --y;

            //
            // get time of day from day fraction
            //
            double hr = Math.Floor(dayfrac * 24.0);
            double mn = Math.Floor((dayfrac * 24.0 - hr) * 60.0);
            double f = ((dayfrac * 24.0 - hr) * 60.0 - mn) * 60.0;
            double sc = Math.Floor(f);
            f -= sc;
            if (f > 0.5) ++sc;
            if (sc == 60)
            {
                sc = 0;
                ++mn;
            }
            if (mn == 60)
            {
                mn = 0;
                ++hr;
            }
            if (hr == 24)
            {
                hr = 0;
                ++d; 
            }

            return new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d), Convert.ToInt32(hr), Convert.ToInt32(mn), Convert.ToInt32(sc));

        }

        public static double ToJulianDate(this DateTime dateTime)
        {
            return ToJulianDate(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        private static double ToJulianDate(int y, int m, int d, int h, int mn, int s)
        {
            double jy;
            double ja;
            double jm;

            if (m > 2)
            {
                jy = y;
                jm = m + 1;
            }
            else
            {
                jy = y - 1;
                jm = m + 13;
            }

            double intgr = Math.Floor(Math.Floor(365.25 * jy) + Math.Floor(30.6001 * jm) + d + 1720995);

            //check for switch to Gregorian calendar  
            int gregcal = 15 + 31 * (10 + 12 * 1582);
            if (d + 31 * (m + 12 * y) >= gregcal)
            {
                ja = Math.Floor(0.01 * jy);
                intgr += 2 - ja + Math.Floor(0.25 * ja);
            }

            //correct for half-day offset  
            double dayfrac = h / 24.0 - 0.5;
            if (dayfrac < 0.0)
            {
                dayfrac += 1.0;
                --intgr;
            }

            //now set the fraction of a day  
            double frac = dayfrac + (mn + s / 60.0) / 60.0 / 24.0;

            //round to nearest second  
            double jd0 = (intgr + frac) * 100000;
            double jd = Math.Floor(jd0);
            if (jd0 - jd > 0.5) ++jd;

            return jd / 100000;
        }
    }
}
