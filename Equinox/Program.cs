using System;

namespace Equinox
{
    class Program
    {
        /*private static SQLiteConnection sqlCon;
        private static SQLiteTransaction sqlTran;
        private static SQLiteCommand sqlCmd;

        private static SQLiteParameter id = new SQLiteParameter();
        private static SQLiteParameter planet = new SQLiteParameter();
        private static SQLiteParameter series = new SQLiteParameter();
        private static SQLiteParameter A = new SQLiteParameter();
        private static SQLiteParameter B = new SQLiteParameter();
        private static SQLiteParameter C = new SQLiteParameter();*/

        static void Main(string[] args)
        {
            //AdvancedDateTime adt = new AdvancedDateTime();
            //adt.Year = 1;
            //adt.Month = Month.January;
            //adt.Day = 1;
            //adt.Hour = 0;
            //adt.Minute = 0;
            //adt.Second = 0;
            //adt.Millisecond = 0;
            //adt.SubtractMilliseconds(15000000);
            //TestCases.GetJulianDayTest();
            //TestCases.GetDateTest();
            //for (int i = 1996; i <= 2005; i++)
            //{

            //long L0, L1, L2, L3, L4, L5;
            //double L, T;
            //L0 = 316402122;
            //L1 = 1021353038718;
            //L2 = 50055;
            //L3 = -56;
            //L4 = -109;
            //L5 = -1;
            //L0 = 316442701;
            //L1 = 1021353011315;
            //L2 = 53724;
            //L3 = -27;
            //L4 = -113;
            //L5 = 0;
            //T = -0.007032169747;
            //L = L0;
            //L += L1 * T;
            //L += L2 * Math.Pow(T, 2);
            //L += L3 * Math.Pow(T, 3);
            //L += L4 * Math.Pow(T, 4);
            //L += L5 * Math.Pow(T, 5);
            //L = (L0 + (L1 * T) + (L2 * Math.Pow(T, 2)) + (L3 * Math.Pow(T, 3)) + (L4 * Math.Pow(T, 4)) + (L5 * Math.Pow(T, 5))) / Math.Pow(10, 8);
            ////L = -68.6592582;
            //L = MathHelper.RadiansToDegrees(L);
            //L = MathHelper.Rev(L);
            //L = MathHelper.CalculateHelocentricLongitude("SATURN", new AdvancedDateTime(1999, Month.July, 26).ToJulianDay(), 5);
            //double B = MathHelper.CalculateHelocentricLatitude("VENUS", 2448976.5, 5);
            //double JDE;
            //JDE = Equinox.GetApproximateEquinox(1962, Equinox.EquinoxType.SumerSolstice);
            //double L = MathHelper.CalculateHelocentricLongitude("EARTH", JDE, 5);
            //double B = MathHelper.CalculateHelocentricLatitude("EARTH", JDE, 4);
            //double R = MathHelper.RadiusVector("EARTH", JDE, 5);
            //double dL, dB, aberration;
            //MathHelper.CorrectLB(L, B, JDE, out dL, out  dB);
            //aberration = 20.4898 / R;
            //double ApparentGeocentricLongitude;
            //ApparentGeocentricLongitude = L - 180 + dL + aberration;
            double JDE0 = Equinox.GetApproximateEquinox(1962, Equinox.EquinoxType.SumerSolstice);
            double JDE = Equinox.CorrectEquinox(JDE0, Equinox.EquinoxType.SumerSolstice);
            AdvancedDateTime ADT = AdvancedDateTime.FromJulianDay(JDE);
            Console.WriteLine("{0}/{1}/{2} {3:00}:{4:00}:{5:00}\t{6}", ADT.Year, ADT.Month, ADT.Day, ADT.Hour, ADT.Minute, ADT.Second, ADT.Era);
            //}

            //Console.Write(Helper.GetEquinox(1962, Helper.Equinox.SumerSolstice));
            //ThreadSafeCollection<VSOP87> vc = new ThreadSafeCollection<VSOP87>();
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.mer", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.ven", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.ear", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.mar", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.jup", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.sat", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.ura", vc);
            //vc = ParseVSOP87.Load(@"C:\Users\GoodOlClint\Downloads\VSOP87\VSOP87D.nep", vc);

            //SQL.Database = "Equinox.db";
            //SQL.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS VSOP87(id, planet, series, A, B, C, PRIMARY KEY (id));");

            //sqlCon = new SQLiteConnection();
            //sqlCon.ConnectionString = "data source=\"" + SQL.Database + "\"";

            //sqlCon.Open();
            //sqlTran = sqlCon.BeginTransaction();
            //sqlCmd = sqlCon.CreateCommand();
            //sqlCmd.CommandText = "INSERT INTO VSOP87(id, planet, series, A, B, C)VALUES(?, ?, ?, ?, ?, ?);";//SELECT ?, ?, ?, ?, ?, ? WHERE NOT EXISTS (SELECT 1 FROM strongs WHERE strongsNumber=?);";

            //sqlCmd.Parameters.Add(id);
            //sqlCmd.Parameters.Add(planet);
            //sqlCmd.Parameters.Add(series);
            //sqlCmd.Parameters.Add(A);
            //sqlCmd.Parameters.Add(B);
            //sqlCmd.Parameters.Add(C);

            //foreach (VSOP87 v in vc)
            //{
            //    id.Value = Guid.NewGuid().ToString();
            //    planet.Value = v.Planet;
            //    series.Value = v.Series;
            //    A.Value = v.A;
            //    B.Value = v.B;
            //    C.Value = v.C;
            //    sqlCmd.ExecuteNonQuery();
            //}
            //sqlTran.Commit();
            //sqlCon.Close();
            //sqlTran.Dispose();
            //sqlCon.Dispose();
            //Console.Write("Done");
            Console.Read();
        }
    }
}