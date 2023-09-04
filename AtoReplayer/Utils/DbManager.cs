using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoReplayer.Utils
{
    class DbUtil
    {
        private readonly string _connectionString = "server=221.149.119.60;port=2023;database=MJTradierDB;user=meancl;password=1234";

        private MySqlConnection dbConn;

        public DbUtil()
        {
            DBConnect();
        }

        private void DBConnect()
        {
            dbConn = new MySqlConnection(_connectionString);
            dbConn.Open();
            Console.WriteLine("DB Connect.");
        }

        private MySqlConnection GetConnection()
        {
            if (dbConn == null)
            {
                DBConnect();
            }
            return dbConn;
        }

        public IEnumerable<T> Query<T>(string query)
        {
            return GetConnection().Query<T>(query);
        }

    }
}
