using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Equinox
{
    class Equinox
    {

        float a; //Semimajor axis of and orbit
        float e; //Eccentricity of and orbit
        float h; //Altitude above the horizon
        float i; //Orbital inclination
        float n; //Mean daily motion
        float q; //Perihelion distance, in AU
        float r; //Radius vector, or distance of a body to the Sun, in AU
        float v; //True anomaly

        float A; //Azimuth
        float H; //Hour angle
        float M; //Mean anomaly
        float R; //Distance from Earth to Sun, in AU
        float T; //Time in Julian centuries (36535 days) from J2000.0

        float α; //Right ascension
        float δ; //Declination
        float ε; //Obliquity of the ecliptic (ε0 is used for the mean obliquity)
        float θ; //Sideral time (θ0 is the sideral time at Greenwish)
        float π; //Parallax
        float πp; //Longitude of perihelion
        float τ; //Time in Julian millennia (365250 days) from J2000.0
        float φ; //Geographical latitude
        float φp; //Geocentric latitude

        public static double GetVernalEquinox(int year)
        {
            return GetEquinox(year, EquinoxType.VernalEquinox);
        }

        public static double GetEquinox(int year, EquinoxType type)
        {
            double Y;
            double JDE0 = 0;
            if (year < 1000)
            {
                Y = year / 1000d;
                switch (type)
                {
                    case EquinoxType.VernalEquinox:
                        JDE0 = 1721139.29189 + (365242.13740 * Y) + (0.06134 * Math.Pow(Y, 2)) + (0.00111 * Math.Pow(Y, 3)) - (0.00071 * Math.Pow(Y, 4));
                        break;
                    case EquinoxType.SumerSolstice:
                        JDE0 = 1721233.25401 + (365241.72562 * Y) - (0.05323 * Math.Pow(Y, 2)) + (0.00907 * Math.Pow(Y, 3)) + (0.00025 * Math.Pow(Y, 4));
                        break;
                    case EquinoxType.AutumnEquinox:
                        JDE0 = 1721325.70455 + (365242.49558 * Y) - (0.11677 * Math.Pow(Y, 2)) - (0.00297 * Math.Pow(Y, 3)) + (0.00074 * Math.Pow(Y, 4));
                        break;
                    case EquinoxType.WinterSolstice:
                        JDE0 = 1721414.39987 + (365242.88257 * Y) - (0.00769 * Math.Pow(Y, 2)) - (0.00933 * Math.Pow(Y, 3)) - (0.00006 * Math.Pow(Y, 4));
                        break;
                }
            }
            else
            {
                Y = (year - 2000d) / 1000d;
                switch (type)
                {
                    case EquinoxType.VernalEquinox:
                        JDE0 = 2451623.80984 + (365242.37404 * Y) - (0.05169 * Math.Pow(Y, 2)) - (0.00411 * Math.Pow(Y, 3)) - (0.00057 * Math.Pow(Y, 4));
                        break;
                    case EquinoxType.SumerSolstice:
                        JDE0 = 2451716.56767 + (365241.62603 * Y) + (0.00325 * Math.Pow(Y, 2)) + (0.00888 * Math.Pow(Y, 3)) - (0.00030 * Math.Pow(Y, 4));
                        break;
                    case EquinoxType.AutumnEquinox:
                        JDE0 = 2451810.21715 + (365242.01767 * Y) + (0.11575 * Math.Pow(Y, 2)) + (0.00337 * Math.Pow(Y, 3)) - (0.00078 * Math.Pow(Y, 4));
                        break;
                    case EquinoxType.WinterSolstice:
                        JDE0 = 2451900.05952 + (365242.74049 * Y) + (0.06223 * Math.Pow(Y, 2)) + (0.00823 * Math.Pow(Y, 3)) - (0.00032 * Math.Pow(Y, 4));
                        break;
                }
            }
            JDE0 = Math.Round(JDE0, 5);
            double T, W, D;
            int S;
            T = (JDE0 - 2451545.0) / 36525;
            T = Math.Round(T, 9);
            W = (DegreeToRadian(35999.373) * T) - DegreeToRadian(2.47);
            D = 1 + (0.0334 * Math.Cos(W)) + (0.0007 * Math.Cos(2 * W));
            D = Math.Round(D, 4);
            S = CalculateSolPeriodicTerms(T);
            double JDE1 = CorrectEquinox(JDE0, type);
            double JDE = Math.Round(JDE0 + ((0.00001 * S) / D), 5);
            return JDE;
        }

        public static double CorrectEquinox(double JDE0, EquinoxType Equinox)
        {
            double L = 0;
            double B = 0;
            double R = 0;
            double T = 10 * ((JDE0 - 2451545.0) / 365250);
            SQL.Database = "Equinox.db";
            SQLiteDataReader dr = SQL.ExecuteReader("SELECT A, B, C FROM VSOP87 WHERE planet = 'EARTH' AND series LIKE('L%');");
            bool nextResult = true;
            while (nextResult)
            {
                while (dr.Read())
                {
                    double A, b, C;
                    A = Convert.ToDouble((string)dr[0]) * Math.Pow(10, 8);
                    b = Convert.ToDouble((string)dr[1]);
                    C = Convert.ToDouble((string)dr[2]);
                    L += CalculatePeriodicTerms(A, b, C, JDE0);
                }
                nextResult = dr.NextResult();
            }
            dr = SQL.ExecuteReader("SELECT A, B, C FROM VSOP87 WHERE planet = 'EARTH' AND series LIKE('B%');");
            nextResult = true;
            while (nextResult)
            {
                while (dr.Read())
                {
                    double A, b, C;
                    A = Convert.ToDouble((string)dr[0]) * Math.Pow(10, 8);
                    b = Convert.ToDouble((string)dr[1]);
                    C = Convert.ToDouble((string)dr[2]);
                    B += CalculatePeriodicTerms(A, b, C, JDE0);
                }
                nextResult = dr.NextResult();
            }
            dr = SQL.ExecuteReader("SELECT A, B, C FROM VSOP87 WHERE planet = 'EARTH' AND series LIKE('R%');");
            nextResult = true;
            while (nextResult)
            {
                while (dr.Read())
                {
                    double A, b, C;
                    A = Convert.ToDouble((string)dr[0]) * Math.Pow(10, 8);
                    b = Convert.ToDouble((string)dr[1]);
                    C = Convert.ToDouble((string)dr[2]);
                    R += CalculatePeriodicTerms(A, b, C, JDE0);
                }
                nextResult = dr.NextResult();
            }
            double gLat, gLong;
            gLong = L + DegreeToRadian(180);
            gLat = -B;
            double LonP;
            LonP = gLong - (DegreeToRadian(1.397) * T) - (DegreeToRadian(0.00031) * Math.Pow(T, 2));
            return 58 * Math.Sin(DegreeToRadian(90) - LonP);
            //double gLatP, gLongP;
            //gLatP = 
            //return 0;
        }

        public static double rev(double x)
        {
            return x - Math.Floor(x / 360.0) * 360.0;
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static int CalculateSolPeriodicTerms(double T)
        {
            double S;
            S = CalculatePeriodicTerms(485, 324.96, 1934.136, T);
            S += CalculatePeriodicTerms(203, 337.23, 32964.467, T);
            S += CalculatePeriodicTerms(199, 342.08, 20.186, T);
            S += CalculatePeriodicTerms(182, 27.85, 445267.112, T);
            S += CalculatePeriodicTerms(156, 73.14, 45036.886, T);
            S += CalculatePeriodicTerms(136, 171.52, 22518.443, T);
            S += CalculatePeriodicTerms(77, 222.54, 65928.934, T);
            S += CalculatePeriodicTerms(74, 296.72, 3034.906, T);
            S += CalculatePeriodicTerms(70, 243.58, 9037.513, T);
            S += CalculatePeriodicTerms(58, 119.81, 33718.147, T);
            S += CalculatePeriodicTerms(52, 297.17, 150.678, T);
            S += CalculatePeriodicTerms(50, 21.02, 2281.226, T);
            S += CalculatePeriodicTerms(45, 247.54, 29929.562, T);
            S += CalculatePeriodicTerms(44, 325.15, 31555.956, T);
            S += CalculatePeriodicTerms(29, 60.93, 4443.417, T);
            S += CalculatePeriodicTerms(18, 155.12, 67555.328, T);
            S += CalculatePeriodicTerms(17, 288.79, 4562.452, T);
            S += CalculatePeriodicTerms(16, 198.04, 62894.029, T);
            S += CalculatePeriodicTerms(14, 199.76, 31436.921, T);
            S += CalculatePeriodicTerms(12, 95.39, 14577.848, T);
            S += CalculatePeriodicTerms(12, 287.11, 31931.756, T);
            S += CalculatePeriodicTerms(12, 230.81, 34777.259, T);
            S += CalculatePeriodicTerms(9, 227.73, 1222.114, T);
            S += CalculatePeriodicTerms(8, 15.45, 16859.074, T);
            return INT(S);
        }

        public static double CalculatePeriodicTerms(double A, double B, double C, double T)
        {
            B = DegreeToRadian(B);
            C = DegreeToRadian(C);
            return A * Math.Cos(B + (C * T));
        }

        public static int INT(double input)
        {
            int ret = (int)Math.Truncate(input);
            return ret;
        }

        public static double DEC(double input)
        {
            return Math.Round(input - INT(input), 4);
        }

        public enum EquinoxType : int
        {
            VernalEquinox = 1,
            SumerSolstice = 2,
            AutumnEquinox = 3,
            WinterSolstice = 4
        }
    }
}
