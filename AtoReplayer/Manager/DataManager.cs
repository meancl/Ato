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

            string query = $"SELECT * FROM minutehistories PARTITION ({partitionFormat}) WHERE sDate='{date}' AND {codeColName} = '{code}' AND compLoc = {compLoc}";
            Console.WriteLine(query);

            return dbUtil.Query<TimeLine>(query);
        }
    }


}
