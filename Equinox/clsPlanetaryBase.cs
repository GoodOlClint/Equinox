using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Equinox
{
    public abstract class PlanetaryBase : MathBase
    {
        protected ThreadSafeCollection<PeriodicTerm> PeriodicTerms = new ThreadSafeCollection<PeriodicTerm>();
        public abstract string PlanetName { get; }

        public PlanetaryBase()
        {
            this.BuildLatitude();
            this.BuildLongitude();
            this.BuildRadiusVector();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Planet"></param>
        /// <param name="JDE">Julian Ephemeris Day</param>
        /// <param name="acuracyLevel">the Level of accuracy required for the given planet</param>
        /// <returns></returns>
        public double CalculateHelocentricLongitude(double JDE, int acuracyLevel)
        {
            int count = (from PeriodicTerm term in PeriodicTerms
                         where term.Series.StartsWith("L")
                         select term.Series).Distinct().Count() - 1;
            if (acuracyLevel < 0 || acuracyLevel > count)
            { throw new IndexOutOfRangeException("acuracyLevel must be greater then zero and less then " + count + " for the Helocentric Longitude of " + PlanetName); }
            return Rev(RadiansToDegrees(QueryDatabase(JDE, acuracyLevel, SeriesType.L)));
        }
        /// <summary>
        /// Corrects the Helocentric Longitude <paramref name="L"/> and the Helocentric Latitude <paramref name="B"/> to the FK5 System
        /// </summary>
        /// <param name="L">The helocentric Longitude, obtained through CalculateHelocentricLongitude</param>
        /// <param name="B">The helocentric Latitude, obtained through CalculateHelocentricLatitude</param>
        /// <param name="T">The time in centuries from 2000.0</param>
        /// <param name="dL">The delta of correction to <paramref name="L"/></param>
        /// <param name="dB">The delta of correction to <paramref name="B"/></param>
        public void CorrectLB(double L, double B, double JDE, out double dL, out double dB)
        {
            double T = 10 * ((JDE - 2451545.0) / 365250);
            double Lp = L - DegreesToRadians((1.397 * T) - (0.00031 * T));
            dL = (-0.09033 + 0.03916 * (Cos(Lp) + Sin(Lp)) * Tan(B));
            dB = 0.03916 * (Cos(Lp) - Sin(Lp));
        }

        public double CalculateHelocentricLatitude(double JDE, int acuracyLevel)
        {
            int count = (from PeriodicTerm term in PeriodicTerms
                         where term.Series.StartsWith("B")
                         select term).Distinct().Count() - 1;
            if (acuracyLevel < 0 || acuracyLevel > count)
            { throw new IndexOutOfRangeException("acuracyLevel must be greater then zero and less then " + count + " for the Helocentric Latitude of " + PlanetName); }
            return Rev(RadiansToDegrees(QueryDatabase(JDE, acuracyLevel, SeriesType.B)));
        }

        public double CalculateRadiusVector(double JDE, int acuracyLevel)
        {
            int count = (from PeriodicTerm term in PeriodicTerms
                         where term.Series.StartsWith("R")
                         select term).Distinct().Count() - 1;
            if (acuracyLevel < 0 || acuracyLevel > count)
            { throw new IndexOutOfRangeException("acuracyLevel must be greater then zero and less then " + count + " for the Radius Vector of " + PlanetName); }
            return QueryDatabase(JDE, acuracyLevel, SeriesType.R);
        }

        protected double QueryDatabase(double JDE, int acuracyLevel, SeriesType series)
        {
            List<long> lst = new List<long>();
            double T = (JDE - 2451545.0) / 365250;
            double ret = 0.0;
            for (int i = 0; i <= acuracyLevel; i++)
            {
                lst.Add(0);
                var query = from PeriodicTerm term in PeriodicTerms
                            where term.Series == (series.ToString() + i)
                            select term;
                foreach (PeriodicTerm Term in query) { lst[i] += (long)Term.Calculate(T); }
                if (i == 0)
                { ret = lst[i]; }
                else if (i == 1)
                { ret += lst[i] * T; }
                else
                { ret += lst[i] * Math.Pow(T, i); }

            }
            return ret / Math.Pow(10, 8);
        }

        protected abstract void BuildLatitude();
        protected abstract void BuildLongitude();
        protected abstract void BuildRadiusVector();

        protected enum SeriesType
        {
            L,
            B,
            R
        }
        protected class PeriodicTerm
        {
            private Dictionary<string, double> Values = new Dictionary<string, double>();
            private string series;
            public PeriodicTerm() { }
            public PeriodicTerm(string Series, double A, double B, double C) { this.series = Series; this.A = A; this.B = B; this.C = C; }
            public double Calculate(double T) { return MathHelper.CalculatePeriodicTerms(A * Math.Pow(10, 8), B, C, T); }
            public double A { get { return this.Values["A"]; } set { this.Values["A"] = value; } }
            public double B { get { return this.Values["B"]; } set { this.Values["B"] = value; } }
            public double C { get { return this.Values["C"]; } set { this.Values["C"] = value; } }
            public string Series { get { return this.series; } set { this.series = value; } }
        }
    }
}
