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
            AdvancedDateTime adt = new AdvancedDateTime();
            adt.Year = 1;
            adt.Month = Month.January;
            adt.Day = 1;
            adt.Hour = 0;
            adt.Minute = 0;
            adt.Second = 0;
            adt.Millisecond = 0;
            adt.SubtractMilliseconds(15000000);
            //TestCases.GetJulianDayTest();
            //TestCases.GetDateTest();
            //for (int i = 1996; i <= 2005; i++)
            //{
            double JDE;
            JDE = Equinox.GetApproximateEquinox(1962, Equinox.EquinoxType.SumerSolstice);
            AdvancedDateTime ADT = AdvancedDateTime.FromJulianDay(JDE);
            Console.WriteLine("{0}/{1}/{2} {3:00}:{4:00}:{5:00}\t{6}", ADT.Year, (int)ADT.Month, ADT.Day, ADT.Hour, ADT.Minute, ADT.Second, ADT.Era);
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
            //Console.Read();
        }
    }
}