using System;
using System.Collections.Generic;
using System.Text;

namespace Equinox
{
    public class MathBase
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
        protected double Sin(double a) { return Math.Sin(a * DegRad); }
        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in degrees</param>
        /// <returns>The cosine of d</returns>
        protected double Cos(double d) { return Math.Cos(d * DegRad); }
        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in degrees</param>
        /// <returns>The tangent of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity, or System.Double.PositiveInfinity, this method returns System.Double.NaN</returns>
        protected double Tan(double a) { return Math.Tan(a * DegRad); }
        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a sine, where -1 ≤d≤ 1</param>
        /// <returns>An angle, θ, measured in degrees, such that -π/2 ≤θ≤π/2 -or- System.Double.NaN if d < -1 or d > 1</returns>
        protected double ASin(double d) { return RaDeg * Math.Asin(d); }
        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a cosine, where -1 ≤d≤ 1</param>
        /// <returns>An angle, θ, measured in degrees, such that 0 ≤θ≤π -or- System.Double.NaN if d < -1 or d > 1</returns>
        protected double ACos(double d) { return RaDeg * Math.Acos(d); }
        /// <summary>
        /// Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a tangent</param>
        /// <returns>An angle, θ, measured in degrees, such that -π/2 ≤θ≤π/2.  -or- System.Double.NaN if d equals System.Double.NaN, -π/2 rounded to double precision (-1.5707963267949) if d equals System.Double.NegativeInfinity, or π/2 rounded to double precision (1.5707963267949) if d equals System.Double.PositiveInfinity</returns>
        protected double ATan(double d) { return RaDeg * Math.Atan(d); }
        //arctan in all four quadrants
        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point</param>
        /// <param name="x">The x coordinate of a point</param>
        /// <returns>An angle, θ, measured in degrees, such that -π≤θ≤π, and tan(θ) = y / x, where (x, y) is a point in the Cartesian plane. Observe the following: For (x, y) in quadrant 1, 0 < θ < π/2.  For (x, y) in quadrant 2, π/2 < θ≤π.  For (x, y) in quadrant 3, -π < θ < -π/2.  For (x, y) in quadrant 4, -π/2 < θ < 0.  For points on the boundaries of the quadrants, the return value is the following: If y is 0 and x is not negative, θ = 0.  If y is 0 and x is negative, θ = π.  If y is positive and x is 0, θ = π/2.  If y is negative and x is 0, θ = -π/2.</returns>
        protected double ATan2(double y, double x) { return RaDeg * Math.Atan2(y, x); }
        //protected double ATan2d(double y, double x) { return RaDeg * Math.Atan(y / x) - 180 * Convert.ToInt32(x < 0); }
        #endregion
        #region Helper Functions
        /// <summary>
        /// Normalize an angle between 0 and 360 degrees
        /// </summary>
        protected double Rev(double x) { return x - Math.Floor(x / 360.0) * 360.0; }
        /// <summary>
        /// Normalize a number between 0 and <paramref name="ammt"/>
        /// </summary>
        protected int Rev(int x, int ammt) { return x - (int)Math.Floor((double)x / ammt) * ammt; }
        /// <summary>
        /// Cube Root (needed for parabolic orbits)
        /// </summary>
        protected double CbRt(double x)
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
        protected int INT(double input) { return (int)Math.Truncate(input); }
        /// <summary>
        /// Gets the portion of a number after the decimal
        /// </summary>
        /// <param name="input">a number with decimal portions</param>
        /// <returns>the decimal portion of <paramref name="input"/></returns>
        protected double DEC(double input) { return Math.Round(input - INT(input), 4); }
        /// <summary>
        /// Converts a Degree into Radians, for use with .NET trig functions
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        protected double DegreesToRadians(double angle) { return Math.PI * angle / 180.0; }
        /// <summary>
        /// Converts Radans into corrisponding degrees
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        protected double RadiansToDegrees(double angle) { return angle * (180.0 / Math.PI); }
        #endregion
    }
}