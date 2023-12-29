using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoIndicator.DB
{
    public class Minutehistories
    {

        public int compLoc { get; set; }
        public DateTime sDate { get; set; }
        public string sCode { get; set; }
        public string sCodeName { get; set; }
        public int nIdx { get; set; }
        public int nTime { get; set; }
        public int nStartFs { get; set; }
        public int nMaxFs { get; set; }
        public int nMinFs { get; set; }
        public int nLastFs { get; set; }
        public int nTotalVolume { get; set; }
        public double fVolumeRatio { get; set; }
        public int nCount { get; set; }
        public double fTotalPrice { get; set; }
        public double fBuyPrice { get; set; }
        public double fSellPrice { get; set; }
        public double fAccumUpPower { get; set; }
        public double fAccumDownPower { get; set; }
        public double fUpDownPer { get; set; }
        public double fInitAngle { get; set; }
        public double fMaxAngle { get; set; }
        public double fMedianAngle { get; set; }
        public double fHourAngle { get; set; }
        public double fRecentAngle { get; set; }
        public double fDAngle { get; set; }
        public double fOverMa0 { get; set; }
        public double fOverMa1 { get; set; }
        public double fOverMa2 { get; set; }
        public double fOverMa0Gap { get; set; }
        public double fOverMa1Gap { get; set; }
        public double fOverMa2Gap { get; set; }
        public int nDownTimeOverMa0 { get; set; }
        public int nDownTimeOverMa1 { get; set; }
        public int nDownTimeOverMa2 { get; set; }
        public int nUpTimeOverMa0 { get; set; }
        public int nUpTimeOverMa1 { get; set; }
        public int nUpTimeOverMa2 { get; set; }
        public int nSummationRanking { get; set; }
        public int nSummationMove { get; set; }
        public int nMinuteRanking { get; set; }

        public Minutehistories()
        {
            compLoc = 0;
            sDate = DateTime.Today;
            sCode = "";
            sCodeName = "";
            nIdx = 0;
            nTime = 0;
            nStartFs = 0;
            nMaxFs = 0;
            nMinFs = 0;
            nLastFs = 0;
            nTotalVolume = 0;
            fVolumeRatio = 0;
            nCount = 0;
            fTotalPrice = 0;
            fBuyPrice = 0;
            fSellPrice = 0;
            fAccumUpPower = 0;
            fAccumDownPower = 0;
            fUpDownPer = 0;
            fInitAngle = 0;
            fMaxAngle = 0;
            fMedianAngle = 0;
            fHourAngle = 0;
            fRecentAngle = 0;
            fDAngle = 0;
            fOverMa0 = 0;
            fOverMa1 = 0;
            fOverMa2 = 0;
            fOverMa0Gap = 0;
            fOverMa1Gap = 0;
            fOverMa2Gap = 0;
            nDownTimeOverMa0 = 0;
            nDownTimeOverMa1 = 0;
            nDownTimeOverMa2 = 0;
            nUpTimeOverMa0 = 0;
            nUpTimeOverMa1 = 0;
            nUpTimeOverMa2 = 0;
            nSummationRanking = 0;
            nSummationMove = 0;
            nMinuteRanking = 0;
        }
    }
}
