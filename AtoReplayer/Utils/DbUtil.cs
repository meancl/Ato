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

        private MySqlConnection[] dbConnPool;
        private int poolSize = 2;

        public DbUtil()
        {
            DBConnect();
        }

        private void DBConnect()
        {
            dbConnPool = new MySqlConnection[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                dbConnPool[i] = new MySqlConnection(_connectionString);
                dbConnPool[i].Open();
                Console.WriteLine("DB Connect. : " + i);
            }
        }

        private MySqlConnection GetConnection(int connPoolIdx)
        {
            if (dbConnPool == null)
            {
                DBConnect();
            }
            return dbConnPool[connPoolIdx - 1];
        }

        public IEnumerable<T> Query<T>(int connPoolIdx, string query)
        {
            return GetConnection(connPoolIdx).Query<T>(query);
        }

    }
}
