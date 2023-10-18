using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoReplayer
{
    public partial class ReplayForm
    {


        public struct MinuteRow
        {
            public int nTime;
            public int nTimeIdx;
            public int nMaxFs;
            public int nMinFs;
            public int nStartFs;
            public int nLastFs;
            public int nUpFs;
            public int nDownFs;
            public int nTotalVolume;
            public int nBuyVolume;
            public int nSellVolume;
            public long lTotalPrice;
            public long lBuyPrice;
            public long lSellPrice;
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
            public int nUpTimeOverMa0;
            public int nUpTimeOverMa1;
            public int nUpTimeOverMa2;
            public int nDownTimeOverMa0;
            public int nDownTimeOverMa1;
            public int nDownTimeOverMa2;

            public double fOverMaGap0;
            public double fOverMaGap1;
            public double fOverMaGap2;

            public int nSummationRanking;
            public int nSummationMove;
            public int nMinuteRanking;
        }

        public class ArrowRows
        {
            public ArrowRow[] arrrowRows;
        }

        public struct ArrowRow
        {
            public int nRqTime;
            public int nRqPrice;
            public int nStrategyNum;
            public int nStartegySequence;

        }
    }
}
