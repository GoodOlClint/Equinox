using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Equinox
{
    class ParseVSOP87
    {
        public static ThreadSafeCollection<VSOP87> Load(string Path, ThreadSafeCollection<VSOP87> vc)
        {
            TextReader tr = new StreamReader(Path);
            //ThreadSafeCollection<VSOP87> vc = new ThreadSafeCollection<VSOP87>();
            bool reading = true;
            string currentSeries = "";
            string currentPlanet = "";
            while (reading)
            {
                string s = tr.ReadLine();
                if (s != null)
                {
                    s = s.Trim();
                    s = s.Replace("  ", " ");
                    s = s.Replace("   ", " ");
                    s = s.Replace("  ", " ");
                    string[] str = s.Split(new Char[] { ' ' });
                    if (str[0] == "VSOP87")
                    {
                        currentPlanet = str[3];
                        currentSeries = createSeries(str[5], str[7]);
                    }
                    else
                    {
                        int len = str.Length;

                        VSOP87 v87 = new VSOP87();
                        v87.Planet = currentPlanet;
                        v87.Series = currentSeries;
                        v87.A = Convert.ToDecimal(str[len - 3]);
                        v87.B = Convert.ToDecimal(str[len - 2]);
                        v87.C = Convert.ToDecimal(str[len - 1]);
                        vc.Add(v87);
                    }
                }
                else
                { reading = false; }
            }
            return (ThreadSafeCollection<VSOP87>)vc.Clone();
        }
        static string createSeries(string Var, string T)
        {
            string Variable;
            switch (Convert.ToInt32(Var))
            {
                case 1:
                    Variable = "L";
                    break;
                case 2:
                    Variable = "B";
                    break;
                case 3:
                    Variable = "R";
                    break;
                default:
                    Variable = "L";
                    break;
            }
            return Variable + T.Replace("*T**", "");
        }
    }

    class VSOP87
    {
        #region Private Members
        private decimal a, b, c;
        private string series, planet;
        #endregion

        #region Public Properties
        public decimal A
        {
            get { return this.a; }
            set { this.a = value; }
        }
        public decimal B
        {
            get { return this.b; }
            set { this.b = value; }
        }
        public decimal C
        {
            get { return this.c; }
            set { this.c = value; }
        }
        public string Planet
        {
            get { return this.planet; }
            set { this.planet = value; }
        }
        public string Series
        {
            get { return this.series; }
            set { this.series = value; }
        }
        #endregion

    }
}
