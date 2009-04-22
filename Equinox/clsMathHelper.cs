using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Equinox
{
    public class MathHelper
    {
        #region Constants
        /// <summary>
        /// The mathmatical constant π
        /// </summary>
        public const double Pi = Math.PI;
        /// <summary>
        /// The ratio of Radians to Degrees
        /// </summary>
        public const double RaDeg = 180.0 / Pi;
        /// <summary>
        /// The ratio of Degrees to Radians
        /// </summary>
        public const double DegRad = Pi / 180.0;
        #endregion
        #region Trig. functions in degrees
        /// <summary>
        /// Returns the sine of a specified angle
        /// </summary>
        /// <param name="a">an angle, measured in degrees</param>
        /// <returns>The sine of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity, or System.Double.PositiveInfinity, this method returns System.Double.NaN.</returns>
        public static double Sin(double a) { return Math.Sin(a * DegRad); }
        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in degrees</param>
        /// <returns>The cosine of d</returns>
        public static double Cos(double d) { return Math.Cos(d * DegRad); }
        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in degrees</param>
        /// <returns>The tangent of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity, or System.Double.PositiveInfinity, this method returns System.Double.NaN</returns>
        public static double Tan(double a) { return Math.Tan(a * DegRad); }
        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a sine, where -1 ≤d≤ 1</param>
        /// <returns>An angle, θ, measured in degrees, such that -π/2 ≤θ≤π/2 -or- System.Double.NaN if d < -1 or d > 1</returns>
        public static double ASin(double d) { return RaDeg * Math.Asin(d); }
        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a cosine, where -1 ≤d≤ 1</param>
        /// <returns>An angle, θ, measured in degrees, such that 0 ≤θ≤π -or- System.Double.NaN if d < -1 or d > 1</returns>
        public static double ACos(double d) { return RaDeg * Math.Acos(d); }
        /// <summary>
        /// Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a tangent</param>
        /// <returns>An angle, θ, measured in degrees, such that -π/2 ≤θ≤π/2.  -or- System.Double.NaN if d equals System.Double.NaN, -π/2 rounded to double precision (-1.5707963267949) if d equals System.Double.NegativeInfinity, or π/2 rounded to double precision (1.5707963267949) if d equals System.Double.PositiveInfinity</returns>
        public static double ATan(double d) { return RaDeg * Math.Atan(d); }
        //arctan in all four quadrants
        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point</param>
        /// <param name="x">The x coordinate of a point</param>
        /// <returns>An angle, θ, measured in degrees, such that -π≤θ≤π, and tan(θ) = y / x, where (x, y) is a point in the Cartesian plane. Observe the following: For (x, y) in quadrant 1, 0 < θ < π/2.  For (x, y) in quadrant 2, π/2 < θ≤π.  For (x, y) in quadrant 3, -π < θ < -π/2.  For (x, y) in quadrant 4, -π/2 < θ < 0.  For points on the boundaries of the quadrants, the return value is the following: If y is 0 and x is not negative, θ = 0.  If y is 0 and x is negative, θ = π.  If y is positive and x is 0, θ = π/2.  If y is negative and x is 0, θ = -π/2.</returns>
        public static double ATan2(double y, double x) { return RaDeg * Math.Atan2(y, x); }
        //public static double ATan2d(double y, double x) { return RaDeg * Math.Atan(y / x) - 180 * Convert.ToInt32(x < 0); }
        #endregion
        #region Helper Functions
        /// <summary>
        /// Normalize an angle between 0 and 360 degrees
        /// </summary>
        public static double Rev(double x) { return x - Math.Floor(x / 360.0) * 360.0; }
        public static int Rev(int x, int ammt) { return x - (int)Math.Floor((double)x / ammt) * ammt; }
        /// <summary>
        /// Cube Root (needed for parabolic orbits)
        /// </summary>
        public static double CbRt(double x)
        {
            if (x > 0.0)
            { return Math.Exp(Math.Log(x) / 3); }
            else if (x < 0.0)
            { return -CbRt(-x); }
            else
            { return 0.0; }
        }
        /// <summary>
        /// Gets the portion of a number before the decimal
        /// </summary>
        /// <param name="input">a whole number with decimal portions</param>
        /// <returns>the whole number portion of <paramref name="input"/></returns>
        public static int INT(double input) { return (int)Math.Truncate(input); }
        /// <summary>
        /// Gets the portion of a number after the decimal
        /// </summary>
        /// <param name="input">a number with decimal portions</param>
        /// <returns>the decimal portion of <paramref name="input"/></returns>
        public static double DEC(double input) { return Math.Round(input - INT(input), 4); }
        /// <summary>
        /// Converts a Degree into Radians, for use with .NET trig functions
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double DegreesToRadians(double angle) { return Math.PI * angle / 180.0; }
        /// <summary>
        /// Converts Radans into corrisponding degrees
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double RadiansToDegrees(double angle) { return angle * (180.0 / Math.PI); }

        /// <summary>
        /// A helper function 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="τ"></param>
        /// <returns><paramref name="A"/> Cos(<paramref name="B" />+<paramref name="C"/><paramref name="T"/>)</returns>
        public static double CalculatePeriodicTerms(double A, double B, double C, double τ) { return A * Math.Cos(B + (C * τ)); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Planet"></param>
        /// <param name="JDE">Julian Ephemeris Day</param>
        /// <param name="acuracyLevel">the Level of accuracy required for the given planet</param>
        /// <returns></returns>
        public static double CalculateHelocentricLongitude(string Planet, double JDE, int acuracyLevel)
        {
            int count = Convert.ToInt32(SQL.ExecuteScalar("SELECT  COUNT(DISTINCT series) FROM vsop87 WHERE planet = '" + Planet + "' AND series LIKE('L%');"));
            count--;
            if (acuracyLevel < 0 || acuracyLevel > count)
            { throw new IndexOutOfRangeException("acuracyLevel must be greater then zero and less then " + count + " for " + Planet); }
            List<long> lst = new List<long>();
            double T = (JDE - 2451545.0) / 365250;
            SQL.Database = "Equinox.db";
            double L = 0.0;
            for (int i = 0; i <= acuracyLevel; i++)
            {
                SQLiteDataReader dr = SQL.ExecuteReader("SELECT A, B, C FROM VSOP87 WHERE planet = '" + Planet + "' AND series = 'L" + i + "';");
                bool nextResult = true;
                while (nextResult)
                {
                    lst.Add(0);
                    while (dr.Read())
                    {
                        double A, B, C;
                        A = Convert.ToDouble((string)dr[0]) * Math.Pow(10, 8);
                        B = Convert.ToDouble((string)dr[1]);
                        C = Convert.ToDouble((string)dr[2]);
                        lst[i] += (long)MathHelper.CalculatePeriodicTerms(A, B, C, T);
                    }
                    nextResult = dr.NextResult();
                }
                if (i == 0)
                { L = lst[i]; }
                else if (i == 1)
                { L += lst[i] * T; }
                else
                { L += lst[i] * Math.Pow(T, i); }
            }
            return Rev(RadiansToDegrees(L / Math.Pow(10, 8)));
        }
        /// <summary>
        /// Corrects the Helocentric Longitude <paramref name="L"/> and the Helocentric Latitude <paramref name="B"/> to the FK5 System
        /// </summary>
        /// <param name="L">The helocentric Longitude, obtained through CalculateHelocentricLongitude</param>
        /// <param name="B">The helocentric Latitude, obtained through CalculateHelocentricLatitude</param>
        /// <param name="T">The time in centuries from 2000.0</param>
        /// <param name="dL">The delta of correction to <paramref name="L"/></param>
        /// <param name="dB">The delta of correction to <paramref name="B"/></param>
        public static void CorrectLB(double L, double B, double JDE, out double dL, out double dB)
        {
            double T = 10 * ((JDE - 2451545.0) / 365250);
            double Lp = L - DegreesToRadians((1.397 * T) - (0.00031 * T));
            dL = (-0.09033 + 0.03916 * (Cos(Lp) + Sin(Lp)) * Tan(B));
            dB = 0.03916 * (Cos(Lp) - Sin(Lp));
        }

        public static double CalculateHelocentricLatitude(string Planet, double JDE, int acuracyLevel)
        {
            int count = Convert.ToInt32(SQL.ExecuteScalar("SELECT  COUNT(DISTINCT series) FROM vsop87 WHERE planet = '" + Planet + "' AND series LIKE('B%');"));
            count--;
            if (acuracyLevel < 0 || acuracyLevel > count)
            { throw new IndexOutOfRangeException("acuracyLevel must be greater then zero and less then " + count + " for " + Planet); }
            List<double> lst = new List<double>();
            double T = (JDE - 2451545.0) / 365250;
            SQL.Database = "Equinox.db";
            double B = 0.0;
            for (int i = 0; i <= acuracyLevel; i++)
            {
                SQLiteDataReader dr = SQL.ExecuteReader("SELECT A, B, C FROM VSOP87 WHERE planet = '" + Planet + "' AND series = 'B" + i + "';");
                bool nextResult = true;
                while (nextResult)
                {
                    lst.Add(0);
                    while (dr.Read())
                    {
                        double A, b, C;
                        A = Convert.ToDouble((string)dr[0]) * Math.Pow(10, 8);
                        b = Convert.ToDouble((string)dr[1]);
                        C = Convert.ToDouble((string)dr[2]);
                        lst[i] += MathHelper.CalculatePeriodicTerms(A, b, C, T);
                    }
                    nextResult = dr.NextResult();
                }
                if (i == 0)
                { B = lst[i]; }
                else if (i == 1)
                { B += lst[i] * T; }
                else
                { B += lst[i] * Math.Pow(T, i); }
            }
            //double Bp = DegreesToRadians(B - (1.397 * T) - (0.00031 * T));
            return Rev(RadiansToDegrees(B / Math.Pow(10, 8)));
        }

        public static double RadiusVector(string Planet, double JDE, int acuracyLevel)
        {
            int count = Convert.ToInt32(SQL.ExecuteScalar("SELECT  COUNT(DISTINCT series) FROM vsop87 WHERE planet = '" + Planet + "' AND series LIKE('R%');"));
            count--;
            if (acuracyLevel < 0 || acuracyLevel > count)
            { throw new IndexOutOfRangeException("acuracyLevel must be greater then zero and less then " + count + " for " + Planet); }
            List<double> lst = new List<double>();
            double T = (JDE - 2451545.0) / 365250;
            SQL.Database = "Equinox.db";
            double R = 0.0;
            for (int i = 0; i <= acuracyLevel; i++)
            {
                SQLiteDataReader dr = SQL.ExecuteReader("SELECT A, B, C FROM VSOP87 WHERE planet = '" + Planet + "' AND series = 'R" + i + "';");
                bool nextResult = true;
                while (nextResult)
                {
                    lst.Add(0);
                    while (dr.Read())
                    {
                        double A, B, C;
                        A = Convert.ToDouble((string)dr[0]) * Math.Pow(10, 8);
                        B = Convert.ToDouble((string)dr[1]);
                        C = Convert.ToDouble((string)dr[2]);
                        lst[i] += MathHelper.CalculatePeriodicTerms(A, B, C, T);
                    }
                    nextResult = dr.NextResult();
                }
                if (i == 0)
                { R = lst[i]; }
                else if (i == 1)
                { R += lst[i] * T; }
                else
                { R += lst[i] * Math.Pow(T, i); }
            }
            return R / Math.Pow(10, 8);
        }
        #endregion
    }
    public class RectangularCords
    {
        #region Constructors
        public RectangularCords() { }
        public RectangularCords(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        #endregion
        #region Private Members
        private double x, y, z;
        #endregion
        #region Public Properties
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Z { get { return z; } set { z = value; } }
        #endregion
        #region Public Functions
        public SphericalCords ToSphericalCords()
        {
            double ra, decl, r;
            r = Math.Sqrt(x * x + y * y + z * z);
            ra = MathHelper.ATan2(y, x);
            decl = MathHelper.ASin(z / r);// = Math.Atan2(z, Math.Sqrt(x * x + y * y));
            return new SphericalCords(ra, decl, r);
        }
        #endregion
        #region Public Static Functions
        public static RectangularCords FromSphericalCords(SphericalCords sc) { return sc.ToRectangularCords(); }
        #endregion
    }
    public class SphericalCords
    {
        #region Constructors
        public SphericalCords() { }
        public SphericalCords(double ra, double decl, double r) : this(new HoursMinutesSeconds(ra), decl, r) { }
        public SphericalCords(HoursMinutesSeconds ra, double decl, double r)
        {
            this.RA = ra;
            this.Decl = decl;
            this.R = r;
        }
        #endregion
        #region Private Members
        private double decl, r;
        private HoursMinutesSeconds ra;
        #endregion
        #region Public Properties
        public HoursMinutesSeconds RA { get { return ra; } set { ra = value; } }
        public double Decl { get { return decl; } set { decl = value; } }
        public double R { get { return r; } set { r = value; } }
        #endregion
        #region Public Functions
        public RectangularCords ToRectangularCords()
        {
            double x, y, z;
            x = r * MathHelper.Cos(RA.ToDecimal()) * MathHelper.Cos(Decl);
            y = r * MathHelper.Sin(RA.ToDecimal()) * MathHelper.Cos(Decl);
            z = r * MathHelper.Sin(Decl);
            return new RectangularCords(x, y, z);
        }
        #endregion
        #region Public Static Functions
        public static SphericalCords FromRectangularCords(RectangularCords rc) { return rc.ToSphericalCords(); }
        #endregion
    }
    public class HoursMinutesSeconds
    {
        #region Constructors
        public HoursMinutesSeconds() { }
        public HoursMinutesSeconds(double d)
        {
            Hours = MathHelper.INT(d);
            d -= MathHelper.INT(d);
            d *= 60;
            Minutes = MathHelper.INT(d);
            d -= MathHelper.INT(d);
            d *= 60;
            Seconds = MathHelper.INT(d);
        }
        #endregion
        #region Private Members
        private int hours, minutes, seconds;
        #endregion
        #region Public Properties
        public int Hours { get { return hours; } set { hours = MathHelper.Rev(value, 24); } }
        public int Minutes
        {
            get { return minutes; }
            set
            {
                int v = MathHelper.Rev(value, 60);
                if (value != v)
                {
                    if (value < 0)
                    {
                        int d = (Math.Abs(value) / 60);
                        if (v != 0)
                        { d = d + 1; }
                        this.Hours -= d;
                    }
                    else { int d = value / 60; this.Hours += d; }
                }
                minutes = v;
            }
        }
        public int Seconds
        {
            get { return seconds; }
            set
            {
                int v = MathHelper.Rev(value, 60);
                if (value != v)
                {
                    if (value < 0)
                    {
                        int d = (Math.Abs(value) / 60);
                        if (v != 0)
                        { d = d + 1; }
                        this.Minutes -= d;
                    }
                    else { int d = value / 60; this.Minutes += d; }
                }
                seconds = v;
            }

        }
        #endregion
        #region Public Functions
        public double ToDecimal() { return Hours + (Minutes / 60) + (Seconds / 3600); }
        public double ToAngle() { return MathHelper.Rev(this.ToDecimal() * 15); }
        #endregion
        #region Public Static Functions
        public static HoursMinutesSeconds FromDecimal(double d) { return new HoursMinutesSeconds(d); }
        #endregion
    }
}