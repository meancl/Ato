using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoReplayer.Models
{
    class StrategyNames
    {
        public List<string> arrFakeBuyStrategyName;
        public List<string> arrFakeResistStrategyName;
        public List<string> arrFakeAssistantStrategyName;
        public List<string> arrFakeVolatilityStrategyName;
        public List<string> arrFakeDownStrategyName;
        public List<string> arrPaperBuyStrategyName;

        public StrategyNames()
        {
            arrFakeBuyStrategyName = new List<string>();
            arrFakeResistStrategyName = new List<string>();
            arrFakeAssistantStrategyName = new List<string>();
            arrFakeVolatilityStrategyName = new List<string>();
            arrFakeDownStrategyName = new List<string>();
            arrPaperBuyStrategyName = new List<string>();

            try
            {
                arrFakeBuyStrategyName.Add("분당 거래대금 10억이상 매수 > 매도 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 거래대금 30억이상 매수 > 매도 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 거래대금 20억이상 매수 > 매도 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 매수대금 5억이상 5번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 10억이상 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 매수대금 20억이상 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 매수대금 10억이상 4번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 30억이상 3번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 15억이상 3번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 20억이상 3번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 10억이상 5번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 거래대금 20억이상 매수 > 매도 3번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 거래대금 30억이상 매수 > 매도 2번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 거래대금 40억이상 매수 > 매도 2번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 거래대금 15억이상 매수 > 매도 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 매수대금 15억이상 5번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 15억이상 .. 1분제한");
                arrFakeBuyStrategyName.Add("분당 매수대금 20억이상 2번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 15억이상 4번 .. 리사이클");
                arrFakeBuyStrategyName.Add("분당 매수대금 10억이상 3번 .. 리사이클");
            }
            catch (Exception indexError)
            {

            }

            try
            {
                arrFakeAssistantStrategyName.Add("누적상승 + 누적하락 120퍼 이상 .. 1분제한");
                arrFakeAssistantStrategyName.Add("누적상승 + 누적하락 200퍼 이상 .. 1분제한");
                arrFakeAssistantStrategyName.Add("누적상승 + 누적하락 120퍼 이상 3번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("누적상승 + 누적하락 100퍼 이상 4번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당속도 1000건 이상 .. 1분제한");
                arrFakeAssistantStrategyName.Add("분당속도 600이상 7번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당속도 600이상 5번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당 매수대금 5억이상 매수>매도 .. 1분제한");
                arrFakeAssistantStrategyName.Add("분당 매수대금 5억이상 매수>매도 3번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당 매수대금 2억이상 매수>매도 4번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당 매수대금 2억이상 매수>매도 3번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당순위2위권 3번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당순위5위권 4번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("분당순위1위 2번 .. 리사이클");
                arrFakeAssistantStrategyName.Add("전고점돌파(실시간) .. 리사이클");
                arrFakeAssistantStrategyName.Add("전고점돌파(분봉기준) .. 리사이클");

            }
            catch (Exception indexError)
            {

            }

            try
            {
                arrFakeResistStrategyName.Add("갭3퍼 1번 1반복.. 단한번");
                arrFakeResistStrategyName.Add("갭4퍼 1번 1반복.. 단한번");
                arrFakeResistStrategyName.Add("갭5퍼 1번 2반복.. 단한번");
                arrFakeResistStrategyName.Add("갭6퍼 1번 2반복.. 단한번");
                arrFakeResistStrategyName.Add("갭7퍼 1번 2반복.. 단한번");
                arrFakeResistStrategyName.Add("갭10퍼 1번 3반복.. 단한번");
                arrFakeResistStrategyName.Add("윗꼬리 1퍼 .. 1분제한");
                arrFakeResistStrategyName.Add("윗꼬리 2퍼 .. 1분제한");
                arrFakeResistStrategyName.Add("실시간 양봉 5퍼 .. 1분제한");
                arrFakeResistStrategyName.Add("지나간 양봉 4퍼 .. 1분제한");
                arrFakeResistStrategyName.Add("실시간 양봉 4퍼 .. 1분제한");
                arrFakeResistStrategyName.Add("지나간 양봉 3퍼 .. 1분제한");
                arrFakeResistStrategyName.Add("실시간 양봉 3퍼 .. 1분제한");
            }
            catch (Exception indexError)
            {

            }


            try
            {
                arrFakeVolatilityStrategyName.Add("차분 5 0.03 5분주기");
                arrFakeVolatilityStrategyName.Add("차분 5 0.05 5분주기");
                arrFakeVolatilityStrategyName.Add("차분 10 0.03 11분주기");
                arrFakeVolatilityStrategyName.Add("차분 10 0.04 6분주기");
                arrFakeVolatilityStrategyName.Add("차분 20 0.05 13분주기");
                arrFakeVolatilityStrategyName.Add("차분 20 0.04 15분주기");
                arrFakeVolatilityStrategyName.Add("차분 15 0.04 12분주기");
                arrFakeVolatilityStrategyName.Add("차분 5 0.07 8분주기");
                arrFakeVolatilityStrategyName.Add("차분 3 0.05 9분주기");
                arrFakeVolatilityStrategyName.Add("차분 4 0.04 10분주기");
                arrFakeVolatilityStrategyName.Add("차분 37 0.04 21분주기");
                arrFakeVolatilityStrategyName.Add("차분 35 0.06 23분주기");
                arrFakeVolatilityStrategyName.Add("차분 30 0.04 30분주기");
                arrFakeVolatilityStrategyName.Add("차분 30 0.03 26분주기");
                arrFakeVolatilityStrategyName.Add("차분 7 0.04 20분주기");
                arrFakeVolatilityStrategyName.Add("차분 23 0.045 17분주기");
                arrFakeVolatilityStrategyName.Add("차분 20 0.05 16분주기");
                arrFakeVolatilityStrategyName.Add("차분 13 0.033 12분주기");
                arrFakeVolatilityStrategyName.Add("차분 20 0.03 14분주기");
                arrFakeVolatilityStrategyName.Add("차분 36 0.06 22분주기");
                arrFakeVolatilityStrategyName.Add("차분 22 0.06 14분주기");

            }
            catch (Exception IdxError)
            {

            }


            try
            {
                arrFakeDownStrategyName.Add("차분 1 -0.025 1분주기");
                arrFakeDownStrategyName.Add("차분 5 -0.03 5분주기");
                arrFakeDownStrategyName.Add("차분 8 -0.04 8분주기");
                arrFakeDownStrategyName.Add("차분 12 -0.05 10분주기");
                arrFakeDownStrategyName.Add("차분 14 -0.035 12분주기");
                arrFakeDownStrategyName.Add("차분 15 -0.025 14분주기");
                arrFakeDownStrategyName.Add("차분 20 -0.04 15분주기");
                arrFakeDownStrategyName.Add("차분 22 -0.045 20분주기");
                arrFakeDownStrategyName.Add("차분 17 -0.025 15분주기");
                arrFakeDownStrategyName.Add("차분 15 -0.03 9분주기");
                arrFakeDownStrategyName.Add("차분 3 -0.033 7분주기");
                arrFakeDownStrategyName.Add("차분 13 -0.036 20분주기");
            }
            catch (Exception indexError)
            {

            }

            try
            {
                arrPaperBuyStrategyName.Add("5분전 갭포함 6퍼 .. 단한번");
                arrPaperBuyStrategyName.Add("10분전 갭포함 8.5퍼 .. 단한번");
                arrPaperBuyStrategyName.Add("5퍼돌파후 전고점(매매:전고점) .. 11분 주기");
                arrPaperBuyStrategyName.Add("p7-m7>=15 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p7-m7>=25 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p7+m7>=30 and p7-m7>=15 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p7+m7>=50 and p7-m7>=15 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p9+m9>=50 and p9-m9>=15 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p9+m9>=70 and p9-m9>=15 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p9+m9>=90 and p9-m9>=10 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p9+m9>=90 and p9-m9>=20 .. 11분 주기");
                arrPaperBuyStrategyName.Add("p9-m9>=30 .. 11분 주기");
                arrPaperBuyStrategyName.Add("파워자 2퍼 .. 11분 주기");
                arrPaperBuyStrategyName.Add("파워자 3퍼 .. 11분 주기");
                arrPaperBuyStrategyName.Add("파워자 4퍼 .. 11분 주기");
                arrPaperBuyStrategyName.Add("총 순위 1위 .. 11분 주기");
                arrPaperBuyStrategyName.Add("현재 분당 파워 순위 1위 .. 11분 주기");
                arrPaperBuyStrategyName.Add("총 순위 2위 .. 11분 주기");
                arrPaperBuyStrategyName.Add("현재 분당 파워 순위 2위 .. 11분 주기");
                arrPaperBuyStrategyName.Add("분당 속도 1000이상 p7-m7>= 15 .. 11분 주기");
                arrPaperBuyStrategyName.Add("전고점 총순위 30위 이전(매매:전고점) .. 11분 주기");
                arrPaperBuyStrategyName.Add("전고점 총순위 10위 이전(매매:전고점) .. 11분 주기");
                arrPaperBuyStrategyName.Add("분당 순위 1위 .. 11분 주기");
                arrPaperBuyStrategyName.Add("R각도 50도 이상 .. 11분 주기");
                arrPaperBuyStrategyName.Add("botUp 421 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 432 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 642 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 643 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 732 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 743 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 953 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 421 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 432 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 642 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 643 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 732 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 743 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 953 전고점 일점돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 421 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 432 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 642 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 643 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 732 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 743 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("botUp 953 전고점 돌파 .. 반복");
                arrPaperBuyStrategyName.Add("갭제외 +6.5퍼 .. 단한번");
                arrPaperBuyStrategyName.Add("갭제외 +8퍼 .. 단한번");
                arrPaperBuyStrategyName.Add("갭제외 +11퍼 .. 단한번");
                arrPaperBuyStrategyName.Add("onlyUpPowerJar 4퍼 .. 11분주기");
                arrPaperBuyStrategyName.Add("9시 30분 전 7퍼 상승 .. 단한번");
                arrPaperBuyStrategyName.Add("9시 30분 전 10퍼 상승 .. 단한번");
                arrPaperBuyStrategyName.Add("9시 30분 전 12퍼 상승 .. 단한번");
                arrPaperBuyStrategyName.Add("10시 전 8퍼 상승 .. 단한번");
                arrPaperBuyStrategyName.Add("10시 전 12퍼 상승 .. 단한번");
                arrPaperBuyStrategyName.Add("10시 전 15퍼 상승 .. 단한번");
            }
            catch (Exception IdxError)
            {

            }


        }
    }
}
