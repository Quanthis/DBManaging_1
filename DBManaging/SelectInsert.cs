using System;
using System.Data.SqlClient;
using static System.Console;

namespace DBManaging
{
    public class SelectInsert
    {
        private string CSource;
        private string CInitialCatalog;
        private bool CTrusted;


        public SelectInsert()
        {

        }
        public SelectInsert(string Sauce, string InitialCatalog, bool trusted)
        {
            CSource = Sauce;
            CInitialCatalog = InitialCatalog;
            CTrusted = trusted;
        }

        private string ConnectionSetup()
        {
            if (CTrusted)
            {
                return $"Data Source={CSource};Initial Catalog={CInitialCatalog};Trusted_Connection=True;";
            }
            else return $"Data Source={CSource};Initial Catalog={CInitialCatalog};Trusted_Connection=False;";
        }

        /*public static string ConectionSetup()          //this one gets DB connection info from user
        {
            string dbName, InitialCatalog;
            bool Trusted;

            WriteLine("If you will make a mistake, program will throw exception! \nWhat is Database Server name?");
            dbName = ReadLine();
            WriteLine("What is DataBase name?");
            InitialCatalog = ReadLine();
            WriteLine("Is this connection trusted? Answer yes or 1 to confirm.");
            string s = ReadLine();
            s.ToUpper();
            if ((s == "YES") || (s == "1"))
            {
                Trusted = true;
            }
            else
            {
                Trusted = false;
            }
            return $"Data Source={dbName};Initial Catalog={InitialCatalog};Trusted_Connection = {Trusted};";
        }*/

        public void SelectSQL(string command, string column, string table)
        {
            string connectionString = ConnectionSetup();
            string fullCommand = command + " " + column + " FROM " + table;

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = connectionString;
                    con.Open();
                    WriteLine($"State: {con.State}");
                    WriteLine($"ConnectionString: {con.ConnectionString}");


                    SqlCommand com = new SqlCommand(fullCommand, con);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        WriteLine(String.Format("{0}", reader[column]));
                    }

                    con.Close();
                }
            }
            catch (Exception sex)
            {
                WriteLine("Exception code:" + sex);
            }
        }

        public void ModifySQL(string command)
        {
            string connectionString = ConnectionSetup();
            string fullCommand = command;

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = connectionString;
                    con.Open();
                    WriteLine($"State: {con.State}");
                    WriteLine($"ConnectionString: {con.ConnectionString}");


                    SqlCommand com = new SqlCommand(fullCommand, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Exception code: {ex}");
            }
        }
    }
}