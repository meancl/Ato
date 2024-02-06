using AtoReplayer.Models;
using AtoReplayer.Utils;
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

    }


}
