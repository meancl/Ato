using AtoReplayer.Models;
using AtoReplayer.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoReplayer.Controller
{
    class DataManager
    {
        DbUtil dbUtil;

        public DataManager()
        {
            dbUtil = new DbUtil();
        }

        public IEnumerable<TimeLine> getTimeLines(string type, string code, string date, int compLoc)
        {
            String codeColName = "sCode";
            if (type == "NAME") codeColName = "sCodeName";
            string partitionFormat = $"P{DateTime.Parse(date):yyyyMM}";

            string query = $"SELECT * FROM minutehistories_legacy  WHERE sDate='{date}' AND {codeColName} = '{code}' AND compLoc = {compLoc} AND nTime > '85800'";
            Console.WriteLine(query);

            return dbUtil.Query<TimeLine>(1, query);
        }
        public IEnumerable<FakeReport> getFakeReports(string type, string code, string date, int compLoc)
        {
            String codeColName = "sCode";
            if (type == "NAME") codeColName = "sCodeName";

            string query = $"SELECT dTradeTime, sCode, sCodeName, nBuyStrategyGroupNum, nBuyStrategyIdx, nBuyStrategySequenceIdx, nLocationOfComp," +
                $" nRqTime, nOverPrice, nPriceAfter1Sec, nYesterdayEndPrice" +
                $" FROM mjtradierdb.fakereports" +
                $" WHERE dTradeTime='{date}' AND {codeColName} = '{code}' AND nLocationOfComp = {compLoc} ";
            Console.WriteLine(query);

            return dbUtil.Query<FakeReport>(2, query);
        }

        public bool InsertRemarkableData(int nCompLoc, string sCodeName, DateTime dTradeTime, int nRegisterNumber, string sRegisterMemo)
        {
            string connectionString = "server=221.149.119.60;port=2023;database=MJTradierDB;user=meancl;password=1234";

            // MySQL 데이터베이스 연결 생성
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                bool isRet = false;
                try
                {
                    connection.Open();

                    // INSERT INTO 문 실행
                    string query = "INSERT INTO replayerremark (nCompLoc, sCodeName, dTradeTime, nRegisterNum, sRegisterMemo) VALUES (@nCompLoc, @sCodeName, @dTradeTime, @nRegisterNum, @sRegisterMemo)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nCompLoc", nCompLoc);
                    command.Parameters.AddWithValue("@sCodeName", sCodeName);
                    command.Parameters.AddWithValue("@dTradeTime", dTradeTime);
                    command.Parameters.AddWithValue("@nRegisterNum", nRegisterNumber);
                    command.Parameters.AddWithValue("@sRegisterMemo", sRegisterMemo);


                    // 삽입 결과 확인
                    isRet = command.ExecuteNonQuery() == 1;
                }
                catch (Exception ex)
                {
                    isRet = false;
                }
                return isRet;
            }
        }
    }


}
