using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoReplayer.Models
{
    class TimeLine
    {
        public int compLoc;
        public DateTime sDate;
        public string sCode;
        public string sCodeName;
        public int nIdx;

        public int nTime;
        public int nTimeIdx;
        public int nMaxFs;
        public int nMinFs;
        public int nStartFs;
        public int nLastFs;
        public int nUpFs;
        public int nDownFs;
        public int nTotalVolume;
        public double fVolumeRatio;
        public double fTotalPrice;
        public double fBuyPrice;
        public double fSellPrice;
        public int nCount;
        public double fAccumUpPower;
        public double fAccumDownPower;

        public double fMedianAngle;
        public double fHourAngle;
        public double fRecentAngle;
        public double fInitAngle;
        public double fMaxAngle;
        public double fDAngle;

        public double fOverMa0;
        public double fOverMa1;
        public double fOverMa2;
        public double fOverMa0Gap;
        public double fOverMa1Gap;
        public double fOverMa2Gap;

        public int nUpTimeOverMa0;
        public int nUpTimeOverMa1;
        public int nUpTimeOverMa2;
        public int nDownTimeOverMa0;
        public int nDownTimeOverMa1;
        public int nDownTimeOverMa2;

        public int nSummationRanking;
        public int nSummationMove;
        public int nMinuteRanking;



        //public double fTradeCompared;
        //public double fTradeStrength;
    }
}
