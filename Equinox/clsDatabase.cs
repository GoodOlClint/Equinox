using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Data.OleDb;
namespace Equinox
{
    public class SQL
    {
        public static string Database = "Equinox.db";
        public static int ExecuteNonQuery(string query)
        {
            SQLiteConnection sqlCon = new SQLiteConnection();
            SQLiteCommand sqlCmd = default(SQLiteCommand);
            int results = 0;
            sqlCon.ConnectionString = "data source=\"" + Database + "\"";
            sqlCon.Open();
            sqlCmd = sqlCon.CreateCommand();
            sqlCmd.CommandText = query;
            results = sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlCon.Close();
            return results;
        }

        public static SQLiteDataReader ExecuteReader(string query)
        {
            SQLiteConnection sqlCon = new SQLiteConnection();
            SQLiteCommand sqlCmd = default(SQLiteCommand);
            SQLiteDataReader results = default(SQLiteDataReader);
            sqlCon.ConnectionString = "data source=\"" + Database + "\"";
            sqlCon.Open();
            sqlCmd = sqlCon.CreateCommand();
            sqlCmd.CommandText = query;
            results = sqlCmd.ExecuteReader();
            return results;
        }

        public static object ExecuteScalar(string query)
        {
            SQLiteConnection sqlCon = new SQLiteConnection();
            SQLiteCommand sqlCmd = default(SQLiteCommand);
            object results = null;
            sqlCon.ConnectionString = "data source=\"" + Database + "\"";
            sqlCon.Open();
            sqlCmd = sqlCon.CreateCommand();
            sqlCmd.CommandText = query;
            results = sqlCmd.ExecuteScalar();
            sqlCmd.Dispose();
            sqlCon.Close();
            return results;
        }
    }
}
