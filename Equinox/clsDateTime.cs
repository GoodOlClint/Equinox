using System;

namespace Equinox
{
    public class AdvancedDateTime
    {
        #region Private Members
        private int year, day, hour, minute, second, millisecond;
        private Month month;
        private Era era;
        #endregion

        #region Constructors
        public AdvancedDateTime() { }
        public AdvancedDateTime(int year, Month month, int day) : this(year, month, day, 0, 0, 0) { }
        public AdvancedDateTime(int year, Month month, int day, int hour, int minute, int second) : this(year, month, day, hour, minute, second, 0) { }
        public AdvancedDateTime(int year, Month month, int day, int hour, int minute, int second, int millisecond)
        {
            if (year <= 0)
            {
                this.era = Era.BC;
                this.year = Math.Abs(year) + 1;
            }
            else
            {
                this.era = Era.AD;
                this.year = year;
            }
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.millisecond = millisecond;
        }

        public AdvancedDateTime(Era era, int year, Month month, int day, int hour, int minute, int second, int millisecond)
        {
            if (year <= 0)
            {
                throw new InvalidCastException("a nagative year is not allowed when the era is set");
            }

            this.era = era;
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.millisecond = millisecond;
        }
        #endregion

        #region Public Properties
        public Era Era { get { return this.era; } set { this.era = value; } }
        public int Year
        {
            get { return this.year; }
            set
            {
                this.year = value;
            }
        }
        public Month Month
        {
            get { return this.month; }
            set
            {
                this.month = value;
            }
        }
        public int Day
        {
            get { return this.day; }
            set
            {
                this.day = value;
            }
        }
        public int Hour
        {
            get { return this.hour; }
            set
            {
                this.hour = value;
            }
        }
        public int Minute { get { return this.minute; } set { this.minute = value; } }
        public int Second { get { return this.second; } set { this.second = value; } }
        public int Millisecond { get { return this.millisecond; } set { this.millisecond = value; } }
        #endregion

        #region Public Static Functions
        public static AdvancedDateTime Now()
        {
            DateTime gd = DateTime.Now;
            return new AdvancedDateTime(gd.Year, (Month)gd.Month, gd.Day, gd.Hour, gd.Minute, gd.Second, gd.Millisecond);
        }

        public static AdvancedDateTime FromDateTime(DateTime dateTime)
        {
            return new AdvancedDateTime(dateTime.Year, (Month)dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }

        public static AdvancedDateTime FromJ2000Day(double JD) { return FromJulianDay(JD + 2451543.5); }
        public static AdvancedDateTime FromModifiedJulianDay(double JD) { return FromJulianDay(JD + 2400000.5); }
        public static AdvancedDateTime FromJulianDay(double JD)
        {
            int Z, A, B, C, D, E;
            double F;
            JD = JD + 0.5;
            Z = (int)Math.Truncate(JD);
            F = Math.Round(JD - Math.Truncate(JD), 4);
            if (Z < 2299161)
            { A = Z; }
            else
            {
                int a = (int)Math.Truncate((Z - 1867216.25) / 36524.25);
                A = Z + 1 + a - (int)Math.Truncate(a / 4d);
            }
            B = A + 1524;
            C = (int)Math.Truncate((B - 122.1) / 365.25);
            D = (int)Math.Truncate(365.25 * C);
            E = (int)Math.Truncate((B - D) / 30.6001);
            double day = Math.Round(B - D - (int)Math.Truncate(30.6001 * E) + F, 4);
            int month, year;
            if (E < 14)
                month = E - 1;
            else
                month = E - 13;
            if (month > 2)
                year = C - 4716;
            else
                year = C - 4715;
            double hour, minute, second, f;
            double dayFrac;
            dayFrac = JD - (int)Math.Truncate(JD);
            hour = Math.Floor(dayFrac * 24.0);
            minute = Math.Floor((dayFrac * 24.0 - hour) * 60.0);
            f = ((dayFrac * 24.0 - hour) * 60.0 - minute) * 60.0;
            second = Math.Floor(f);
            if (f > 0.5) second++;
            return new AdvancedDateTime(year, (Month)month, (int)day, (int)hour, (int)minute, (int)second);
        }
        public static int MonthLength(Month month, int year)
        {
            int ret = 0;
            switch (month)
            {
                case Month.January:
                case Month.March:
                case Month.May:
                case Month.July:
                case Month.August:
                case Month.October:
                case Month.December:
                    ret = 31;
                    break;
                case Month.April:
                case Month.June:
                case Month.September:
                case Month.November:
                    ret = 30;
                    break;
                case Month.February:
                    if (IsLeapYear(year))
                    { ret = 29; }
                    else { ret = 28; }
                    break;
            }
            return ret;
        }
        public static bool IsLeapYear(int year) { return (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0)); }
        #endregion

        #region Public Functions
        public double ToJ200Day() { return ToJulianDay() - 2451543.5; }
        public double ToModifiedJulianDay() { return ToJulianDay() - 2400000.5; }
        public double ToJulianDay()
        {
            int Y, M;
            double D;
            if (this.era == Era.BC)
            {
                Y = (this.year * (int)this.era) - 1;
            }
            else { Y = this.year; }
            M = (int)this.month;
            D = this.day;
            if (M <= 2)
            {
                Y = Y - 1;
                M = M + 12;
            }
            double A, B = 0;
            if ((Y == 1582) && (M == 10) && (D > 5) && (D < 15))
            {
                throw new IndexOutOfRangeException("the dates October 5th, through October 15th 1582 AD are invalid");
            }
            if ((Y > 1582) || ((Y == 1582) && M >= 10 && (D >= 15)))
            {
                A = (int)Math.Truncate(Y / 100d);
                B = 2 - A + (int)Math.Truncate(A / 4);
            }
            double JD;
            JD = (int)Math.Truncate(365.25 * (Y + 4716)) + (int)Math.Truncate(30.6001 * (M + 1)) + D + B - 1524.5;
            return JD;
        }

        public DateTime ToDateTime()
        {
            DateTime ret = DateTime.Now;
            switch (this.era)
            {
                case Era.AD:
                    ret = new DateTime(this.year, (int)this.month, this.day, this.hour, this.minute, this.second);
                    break;
                case Era.BC:
                    throw new InvalidCastException("BC dates are not representable in a System.DateTime Type");
            }
            return ret;
        }

        public bool IsLeapYear() { return (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0)); }

        public void AddMilliseconds(int milliseconds)
        {
            int value = this.Millisecond + milliseconds;
            int v = MathHelper.Rev(value, 1000);
            if (value != v)
            {
                if (value < 0)
                {
                    int d = (Math.Abs(value) / 1000);
                    if (v != 0)
                    { d = d + 1; }
                    this.SubtractSeconds(d);
                }

                else { int d = value / 1000; this.AddSeconds(d); }
            }
            this.Millisecond = v;
        }
        public void AddSeconds(int seconds)
        {
            int value = this.Second + seconds;
            int v = MathHelper.Rev(value, 60);
            if (value != v)
            {
                if (value < 0)
                {
                    int d = (Math.Abs(value) / 60);
                    if (v != 0)
                    { d = d + 1; }
                    this.SubtractMinutes(d);
                }

                else { int d = value / 60; this.AddMinutes(d); }
            }
            this.Second = v;
        }
        public void AddMinutes(int minutes)
        {
            int value = this.Minute + minutes;
            int v = MathHelper.Rev(value, 60);
            if (value != v)
            {
                if (value < 0)
                {
                    int d = (Math.Abs(value) / 60);
                    if (v != 0)
                    { d = d + 1; }
                    this.SubtractHours(d);
                }

                else { int d = value / 60; this.AddHours(d); }
            }
            this.Minute = v;
        }
        public void AddHours(int hours)
        {
            int value = this.Hour + hours;
            int v = MathHelper.Rev(value, 24);
            if (value != v)
            {
                if (value < 0)
                {
                    int d = (Math.Abs(value) / 24);
                    if (v != 0)
                    { d = d + 1; }
                    this.SubtractDays(d);
                }

                else { int d = value / 24; this.AddDays(d); }
            }
            this.Hour = v;
        }
        public void AddDays(int days)
        {
            int value = this.Day + days;
            int monthLength = MonthLength(this.Month, this.Year);
            int v = MathHelper.Rev(value, monthLength + 1);
            if (v == 0) v++;
            if (value != v)
            {
                if (value <= 0)
                {
                    if (v == 1) v -= 2;
                    int d = (Math.Abs(value) / monthLength);
                    if (v != 0)
                    { d = d + 1; }
                    this.SubtractMonths(d);
                }

                else { int d = value / monthLength; this.AddMonths(d); }
            }
            this.Day = MathHelper.Rev(v, monthLength + 1);
        }
        public void AddMonths(int months)
        {
            int value = (int)this.Month + months;
            int v = MathHelper.Rev(value, 13);
            if (v == 0) v++;
            if (value != v)
            {
                if (value <= 0)
                {
                    if (v == 1) v -= 2;
                    int d = (Math.Abs(value) / 12);
                    if (v != 0)
                    { d = d + 1; }
                    this.SubtractYears(d);
                }

                else { int d = value / 12; this.AddYears(d); }
            }
            this.Month = (Month)MathHelper.Rev(v, 13);
        }
        public void AddYears(int years)
        { this.Year = this.Year + years; }
        public void SubtractMilliseconds(int milliseconds)
        { AddMilliseconds(-milliseconds); }
        public void SubtractSeconds(int seconds)
        { AddSeconds(-seconds); }
        public void SubtractMinutes(int minutes)
        { AddMinutes(-minutes); }
        public void SubtractHours(int hours)
        { AddHours(-hours); }
        public void SubtractDays(int days)
        { AddDays(-days); }
        public void SubtractMonths(int months)
        { AddMonths(-months); }
        public void SubtractYears(int years)
        { AddYears(-years); }

        #endregion
    }

    #region Public Enums
    public enum Era : int
    {
        AD = 1,
        BC = -1
    }

    public enum Month : int
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    #endregion

}