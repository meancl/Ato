using AtoReplayer.Controller;
using AtoReplayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

/*
 todo

- 시간 플레이 바 (호가플레이 ui 참조)
 */

namespace AtoReplayer
{
    public partial class AtoReplayer : Form
    {
        private DataManager dataManager;
        private const string DATEFORMAT = "yyyy-MM-dd";

        private string searchCode = ""; // 주식 코드 or 주식명
        private string searchDate = DateTime.Today.ToString(DATEFORMAT);
        private int searchCompLoc = 1;

        TimeLine[] timelines = new TimeLine[0];
        FakeReport[] fakereports = new FakeReport[0];
        TimeLine[] displayedTimelines = new TimeLine[0];
        int delayInterval = 1000;

        int currentDrawIdx = 0;

        StrategyNames strategyNames = new StrategyNames();
        public bool isArrowGrouping = true;
        public string NEW_LINE = Environment.NewLine;

        public const int MILLION = 1000000;

        // =============================================
        // 거래 시뮬레이션을 위한 변수들 
        // =============================================
        public const int DEFAULT_DEPOSIT = 500000;
        public const double BUY_COMMISION = 0.00015;
        public const double SELL_COMMISION = 0.00015;
        public const double STOCK_TAX = 0.0018;

        public int nCurTime;
        public int nCurStartPrice;
        public int nCurHighPrice;
        public int nCurLowPrice;
        public int nCurLastPrice;
        public int nCurSpeed; // 현재속도
        public int nCurIdx; // 현재 인덱스
        public double fCurPower;

        public int nCurHitNum;
        public int nCurHitType;

        public int profitnLoss; // 손익
        public int nCurDeposit = DEFAULT_DEPOSIT; // 현재예수금
        public int nCurPrice; // 현재가
        public int nEveragePrice; // 평단가
        public double pnLRatio; // 손익률
        public int pnLPrice; // 손익금
        public int nOwnVolume;// 보유수량
        public int nOwnTotalBuyedPrice; // 보유 총 매수가
        public int nPossibleVolume; // 가능수량

        public int nOrderNum = 0;
        public class UnTradedInfo
        {
            public int nNum; // 주문번호
            public int nTime; // 주문시간
            public string sType; // 주문타입
            public int nVolume; // 주문수량
            public int nPrice; // 주문가
            public int nIdx;

            public UnTradedInfo(int nOrderNum, int aTime = 0, string aType = "", int aVolume = 0, int aPrice = 0, int aIdx = 0)
            {
                nNum = nOrderNum;
                nTime = aTime;
                sType = aType;
                nVolume = aVolume;
                nPrice = aPrice;
                nIdx = aIdx;
            }

        }


        public List<UnTradedInfo> tradedHistoryList;
        public List<UnTradedInfo> unTradedInfoList;

        public Dictionary<int, int> hitDict25;
        public Dictionary<int, int> hitDict38;
        public Dictionary<int, int> hitDict312;
        public Dictionary<int, int> hitDict410;

        public Dictionary<int, bool> hogaDictionary;
        public List<int> hogaList;

        public bool isHitView = true;
        public bool isMouseCursorView;
        public SoundPlayer buySoundPlayer;
        public SoundPlayer sellSoundPlayer;

        public struct ReserveInfo
        {
            public bool isSelected;
            public bool isChosen;
            public int nReserveTime;
            public double fTouchLine;
            public int nAdjustedPrice;
            public int nReserveVolume;
            public int nReservedIdx;

            public void Clear()
            {
                isSelected = false;
                isChosen = false;
                nReserveTime = 0;
                fTouchLine = 0;
                nAdjustedPrice = 0;
                nReserveVolume = 0;
                nReservedIdx = 0;
            }
        }
        public const int INIT_RESERVE = 3;

        public const int UP_BUY_RESERVE = 0;
        public const int DOWN_BUY_RESERVE = 1;
        public const int DOWN_SELL_RESERVE = 2;

        public ReserveInfo[] reserveArr;
        // ---------------------------------------------
        // ---------------------------------------------
        // ---------------------------------------------
        public System.Timers.Timer timer;

        public bool isViewGapMa = true;
        public bool isViewStartMa = true;

        public bool isBuyArrowVisible = true;
        public bool isSellArrowVisible = true;
        public bool isFakeBuyArrowVisible = true;
        public bool isFakeResistArrowVisible = true;
        public bool isFakeAssistantArrowVisible = true;
        public bool isFakeVolatilityArrowVisible = true;
        public bool isFakeDownArrowVisible = true;
        public bool isPaperBuyArrowVisible = true;
        public bool isPaperSellArrowVisible = true;

        public bool isRealBuyArrowVisible = true;
        public bool isRealSellArrowVisible = true;

        public bool isAllArrowVisible = true;

        public string sRegisterMessage = "";

        public struct ViewData
        {
            public DateTime dTradeTime;
            public string sCodeName;
        }
        public bool isViewSetting;
        public int nViewIdx;
        public int nViewPass;
        public List<ViewData> viewList;

        public AtoReplayer()
        {
            InitializeComponent();

            initializeDateTimePicker();
            initializeChartDesign();

            viewList = new List<ViewData>();

            dataManager = new DataManager();

            compLocButton.Text = searchCompLoc.ToString();

            this.KeyPreview = true;
            this.KeyDown += KeyDownHandler;
            this.MouseWheel += MouseWheelEventHandler;

            timer = new System.Timers.Timer(delayInterval);
            timer.Elapsed += delegate (Object sender, System.Timers.ElapsedEventArgs e)
            {
                DrawChartUntilIdx();
            };
            timer.Enabled = false; // false로 만들거라

            //처음 공백 Placeholder 지정
            sCodeTextBox.ForeColor = Color.DarkGray;
            sCodeTextBox.Text = "종목명/종목코드";

            historyChart.MouseMove += ChartMouseMoveHandler;
            historyChart.MouseClick += ChartMouseClickHandler; // 이하동문
            historyChart.Paint += ChartOnPaintHandler;

            // 컴포넌트 이벤트 핸들러
            historyChart.KeyUp += ChartControlKeyUpHandler;
            historyChart.Click += ChartClickHandler;
            dateTimePicker1.CloseUp += dateTimePicker1_ValueChanged;
            dateTimePicker1.KeyDown += dateTimePicker1_KeyDown;
            sCodeTextBox.GotFocus += RemovePlaceholder;
            sCodeTextBox.Leave += SetPlaceholder;
            sCodeTextBox.Leave += sCodeTextBox_Leave;
            sCodeTextBox.KeyPress += sCodeTextBox_KeyPress;
            playButton.Click += ViewCurTimerSet;
            compLocButton.Click += compLocButton_ClickAsync;
            back5.Click += back5_Click;
            back10.Click += back10_Click;
            back60.Click += back60_Click;
            // ToolTip toolTip1 = new ToolTip();

            forward5.Click += forward5_Click;
            forward10.Click += forward10_Click;
            forward60.Click += forward60_Click;
            nDelaySecondTextBox.KeyPress += nDelaySecondTextBox_KeyPress;
            // toolTip1.SetToolTip(forward60, "우아앙");


            buyButton.Click += TradeButtonHandler;
            sellButton.Click += TradeButtonHandler;

            buyPriceTxtBox.Click += TradeTextBoxClickHandler;
            buyVolumeTxtBox.Click += TradeTextBoxClickHandler;
            sellPriceTxtBox.Click += TradeTextBoxClickHandler;
            sellVolumeTxtBox.Click += TradeTextBoxClickHandler;

            // 테스트용 기본값
            // searchCode = "동화약품";
            // sCodeTextBox.Text = "동화약품";
            // dateTimePicker1.Value = Convert.ToDateTime("2023-11-10");
            // searchDate = "2023-06-16";
            buySoundPlayer = new SoundPlayer(@"..\..\Sounds\buyAlarm.wav");
            sellSoundPlayer = new SoundPlayer(@"..\..\Sounds\sellAlarm.wav");

            hogaDictionary = new Dictionary<int, bool>();
            hogaList = new List<int>();

            string sString = "STRING";
            string sInt = "INT";
            string sDouble = "DOUBLE";

            unTradedListView.Columns.Add(new ColumnHeader { Name = sInt, Text = "번호" });
            unTradedListView.Columns.Add(new ColumnHeader { Name = sInt, Text = "주문시간" });
            unTradedListView.Columns.Add(new ColumnHeader { Name = sString, Text = "타입" });
            unTradedListView.Columns.Add(new ColumnHeader { Name = sInt, Text = "주문가" });
            unTradedListView.Columns.Add(new ColumnHeader { Name = sInt, Text = "주문수량" });

            unTradedInfoList = new List<UnTradedInfo>();
            tradedHistoryList = new List<UnTradedInfo>();
            hitDict25 = new Dictionary<int, int>();
            hitDict38 = new Dictionary<int, int>();
            hitDict312 = new Dictionary<int, int>();
            hitDict410 = new Dictionary<int, int>();

            reserveArr = new ReserveInfo[INIT_RESERVE];

            unTradedListView.FullRowSelect = true;

            unTradedListView.View = System.Windows.Forms.View.Details;
            unTradedListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            unTradedListView.MouseClick += MouseClickHandler;
            unTradedListView.MouseDoubleClick += RowDoubleClickHandler;
            unTradedListView.KeyUp += ListViewKeyUpHandller;
        }

        // ===================================================
        // 미체결 잔고 관련 메서드
        // ===================================================
        public int nPrevOrderNum = 0;
        public void MouseClickHandler(Object sender, EventArgs e)
        {
            try
            {
                if (unTradedListView.FocusedItem != null)
                {
                    nPrevOrderNum = int.Parse(unTradedListView.FocusedItem.SubItems[0].Text);

                }
            }
            catch
            {

            }
        }

        public void RowDoubleClickHandler(Object sender, MouseEventArgs e)
        {
            try
            {
                if (unTradedListView.FocusedItem != null)
                {
                    nPrevOrderNum = int.Parse(unTradedListView.FocusedItem.SubItems[0].Text);

                    for (int i = 0; i < unTradedInfoList.Count; i++)
                    {
                        if (unTradedInfoList[i].nNum == nPrevOrderNum)
                        {
                            if (unTradedInfoList[i].sType.Equals("매수"))
                            {
                                nCurDeposit += (int)(unTradedInfoList[i].nVolume * unTradedInfoList[i].nPrice * (1 + BUY_COMMISION));
                            }
                            else if (unTradedInfoList[i].sType.Equals("매도"))
                            {
                                nPossibleVolume += unTradedInfoList[i].nVolume;
                            }
                            DisplayCurInfos();
                            unTradedInfoList.RemoveAt(i--);
                            break;
                        }
                    }
                    UpdateListView();
                }
            }
            catch
            {

            }
        }

        public void ListViewKeyUpHandller(object sender, KeyEventArgs k)
        {
            char cUp = (char)k.KeyValue;
            if (cUp == 'C')
            {
                for (int i = 0; i < unTradedInfoList.Count; i++)
                {
                    if (unTradedInfoList[i].nNum == nPrevOrderNum)
                    {
                        if (unTradedInfoList[i].sType.Equals("매수"))
                        {
                            nCurDeposit += (int)(unTradedInfoList[i].nVolume * unTradedInfoList[i].nPrice * (1 + BUY_COMMISION));
                        }
                        else if (unTradedInfoList[i].sType.Equals("매도"))
                        {
                            nPossibleVolume += unTradedInfoList[i].nVolume;
                        }
                        DisplayCurInfos();
                        unTradedInfoList.RemoveAt(i--);
                        break;
                    }
                }
                UpdateListView();


            }
        }

        public List<ListViewItem> ReturnListViewItems(List<UnTradedInfo> tradeInfos)
        {
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (UnTradedInfo info in tradeInfos)
            {
                ListViewItem listViewItem = new ListViewItem(new String[]
                {
                    info.nNum.ToString(),
                    info.nTime.ToString(),
                    info.sType,
                    info.nPrice.ToString(),
                    info.nVolume.ToString()
                });

                listViewItem.UseItemStyleForSubItems = false;
                listViewItem.SubItems[2].ForeColor = (info.sType.Equals("매수")) ? Color.Red : Color.Blue;
                listViewItems.Add(listViewItem);
            }

            return listViewItems;
        }

        public void ChartOnPaintHandler(Object sender, PaintEventArgs e)
        {
            void DrawReserveLine()
            {
                try
                {
                    if (historyChart.Series["MinuteStick"].Points.Count > 0)
                    {
                        try
                        {

                            int reservationX1 = (int)historyChart.ChartAreas[0].AxisX.ValueToPixelPosition(historyChart.ChartAreas[0].AxisX.Minimum);
                            int reservationX2 = (int)historyChart.ChartAreas[0].AxisX.ValueToPixelPosition(historyChart.ChartAreas[0].AxisX.Maximum);

                            float reservationY1;

                            string sReserveChosenMsg;

                            if (reserveArr[UP_BUY_RESERVE].isSelected && nCurReserve == UP_BUY_RESERVE)
                            {
                                if (reserveArr[UP_BUY_RESERVE].fTouchLine > 0)
                                {
                                    reservationY1 = (int)historyChart.ChartAreas[0].AxisY.ValueToPixelPosition(reserveArr[UP_BUY_RESERVE].fTouchLine);
                                    e.Graphics.DrawLine(new Pen(Color.BlueViolet, 3), reservationX1, reservationY1, reservationX2, reservationY1);
                                    priceViewLabel.Text = $"이상매수가격 : {Math.Round(reserveArr[UP_BUY_RESERVE].fTouchLine, 2)}";
                                }

                                sReserveChosenMsg = $"이상매수 채택 : {reserveArr[UP_BUY_RESERVE].isChosen}";
                                if (!reserveChosenLabel.Text.Equals(sReserveChosenMsg))
                                    reserveChosenLabel.Text = sReserveChosenMsg;
                            }
                            else if (reserveArr[DOWN_BUY_RESERVE].isSelected && nCurReserve == DOWN_BUY_RESERVE)
                            {
                                if (reserveArr[DOWN_BUY_RESERVE].fTouchLine > 0)
                                {
                                    reservationY1 = (int)historyChart.ChartAreas[0].AxisY.ValueToPixelPosition(reserveArr[DOWN_BUY_RESERVE].fTouchLine);
                                    e.Graphics.DrawLine(new Pen(Color.Gold, 3), reservationX1, reservationY1, reservationX2, reservationY1);
                                    priceViewLabel.Text = $"이하매수가격 : {Math.Round(reserveArr[DOWN_BUY_RESERVE].fTouchLine, 2)}";
                                }

                                sReserveChosenMsg = $"이하매수 채택 : {reserveArr[DOWN_BUY_RESERVE].isChosen}";
                                if (!reserveChosenLabel.Text.Equals(sReserveChosenMsg))
                                    reserveChosenLabel.Text = sReserveChosenMsg;
                            }
                            else if (reserveArr[DOWN_SELL_RESERVE].isSelected && nCurReserve == DOWN_SELL_RESERVE)
                            {
                                if (reserveArr[DOWN_SELL_RESERVE].fTouchLine > 0)
                                {
                                    reservationY1 = (int)historyChart.ChartAreas[0].AxisY.ValueToPixelPosition(reserveArr[DOWN_SELL_RESERVE].fTouchLine);
                                    e.Graphics.DrawLine(new Pen(Color.Black, 3), reservationX1, reservationY1, reservationX2, reservationY1);
                                    priceViewLabel.Text = $"이하매도가격 : {Math.Round(reserveArr[DOWN_SELL_RESERVE].fTouchLine, 2)}";
                                }

                                sReserveChosenMsg = $"이하매도채택 : {reserveArr[DOWN_SELL_RESERVE].isChosen}";
                                if (!reserveChosenLabel.Text.Equals(sReserveChosenMsg))
                                    reserveChosenLabel.Text = sReserveChosenMsg;
                            }
                        }
                        catch
                        { }
                    }
                }
                catch { }
            }

            void DrawHitEdge(Dictionary<int, int> dict, Color color)
            {
                try
                {
                    if (historyChart.Series["MinuteStick"].Points.Count > 0)
                    {
                        foreach (var key in dict.Keys)
                        {
                            Series series = historyChart.Series["MinuteStick"];

                            if (series.Points.Count >= key + 1)
                            {
                                DataPoint point = series.Points[key];

                                double pixelPosition1 = historyChart.ChartAreas[0].AxisX.ValueToPixelPosition(0.8);
                                double pixelPosition2 = historyChart.ChartAreas[0].AxisX.ValueToPixelPosition(0);

                                double barLength = Math.Abs(pixelPosition2 - pixelPosition1);

                                double xLocation = key + 1;
                                double yStartValue = point.YValues[2]; // 시가
                                double yLastValue = point.YValues[3]; // 종가

                                double yMaxValue = yLastValue > yStartValue ? yLastValue : yStartValue;
                                double yMinValue = yLastValue < yStartValue ? yLastValue : yStartValue;

                                using (Pen pen = new Pen(color, 2))
                                {
                                    e.Graphics.DrawRectangle(pen,
                                        (float)(historyChart.ChartAreas[0].AxisX.ValueToPixelPosition(xLocation) - barLength / 2),
                                        (float)historyChart.ChartAreas[0].AxisY.ValueToPixelPosition(yMaxValue),
                                        (float)barLength,
                                        (float)(historyChart.ChartAreas[0].AxisY.ValueToPixelPosition(yMinValue) - historyChart.ChartAreas[0].AxisY.ValueToPixelPosition(yMaxValue)));
                                }
                            }
                        }
                    }

                }
                catch { }
            }

            if (isHitView)
            {
                DrawHitEdge(hitDict25, Color.Orange);
                DrawHitEdge(hitDict38, Color.Green);
                DrawHitEdge(hitDict312, Color.Purple);
                DrawHitEdge(hitDict410, Color.Black);
            }

            DrawReserveLine();
        }

        // ---------------------------------------------------
        // ---------------------------------------------------
        // ---------------------------------------------------

        public int prevXMove = 0, prevYMove = 0;
        public int prevXPos = 0, prevYPos = 0;

        public void ChartMouseMoveHandler(Object o, MouseEventArgs e) // 마우스가 안움직이더라도 차트 안에 있으면 호출된다.
        {
            if (e.X != prevXMove || e.Y != prevYMove)  // 움직임이 조금이라도 있으면?
            {
                prevXMove = e.X;
                prevYMove = e.Y;
            }
            else // 전혀 움직이지 않았다면
                return;

            double xCoord = 0, yCoord = 0;

            HitTestResult hit = historyChart.HitTest(e.X, e.Y);

            if (hit.ChartArea != null && historyChart.Series["MinuteStick"].Points.Count > 0)
            {
                xCoord = historyChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                yCoord = historyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                if (double.IsNaN(xCoord))
                    xCoord = 0;
                if (double.IsNaN(yCoord))
                    yCoord = 0;
                xCoord = Math.Round(xCoord, 3);
                yCoord = Math.Round(yCoord, 3);


                curLocLabel.Text = $"현재좌표 : {xCoord} {yCoord}";
                //if (nFirstPrice != 0)
                curLocPowerLabel.Text = $"커서파워 : {Math.Round((double)(yCoord - fYesterdayPrice) / fYesterdayPrice, 3)}";
            }


            if (isMouseCursorView)
            {
                if (hit.ChartArea == null)
                {
                    prevXPos = 0;
                    prevYPos = 0;
                    historyChart.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.NotSet;
                    historyChart.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.NotSet;
                }
                else
                {
                    if (historyChart.Series["MinuteStick"].Points.Count > 0) // 전체 접근가능 조건
                    {
                        xCoord = historyChart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                        yCoord = historyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                        historyChart.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Solid;
                        historyChart.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Solid;

                        historyChart.ChartAreas[0].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), false);
                        historyChart.ChartAreas[0].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), false);


                        if (hit.Series != null && hit.Series.Name.Equals("MinuteStick"))
                        {
                            historyChart.ChartAreas[0].CursorX.LineColor = Color.Yellow;
                            historyChart.ChartAreas[0].CursorY.LineColor = Color.Yellow;
                        }
                        else
                        {
                            historyChart.ChartAreas[0].CursorX.LineColor = Color.Red;
                            historyChart.ChartAreas[0].CursorY.LineColor = Color.Red;
                        }
                    }

                }
            }

        }

        public int GetAdjustedPrice(double fPrice, bool isUpped = true)
        {
            int pTest = 0;

            if (isUpped)
            {
                for (int i = 0; i < hogaList.Count; i++)
                {
                    pTest = hogaList[i];
                    if (pTest + GetIntegratedMarketGap(pTest) >= fPrice || (i == hogaList.Count - 1))
                    {
                        break;
                    }
                }
            }
            else
            {

                for (int i = hogaList.Count - 1; i >= 0; i--)
                {
                    pTest = hogaList[i];
                    if (pTest - GetIntegratedMarketGap(pTest) <= fPrice || (i == 0))
                    {
                        break;
                    }
                }
            }
            return pTest;

        }

        public TRADE_MODE eBuyMode = TRADE_MODE.NONE_MODE;
        public const int NONE_MODE = 0;
        public const int BUY_MODE = 1;
        public const int SELL_MODE = 2;
        public enum TRADE_MODE
        {
            NONE_MODE,
            BUY_MODE,
            SELL_MODE,
        }

        public int nCurReserve;
        public void ChartMouseClickHandler(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (eBuyMode == TRADE_MODE.BUY_MODE)
                    {
                        double yCoord = historyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                        if (double.IsNaN(yCoord))
                            yCoord = 0;
                        else
                        {
                            int targetPrice = GetAdjustedPrice(yCoord);

                            // TODO 매수 미체결 삽입
                            int nVolumeToBuy = nMouseWheel;
                            int nPriceToBuy = targetPrice;
                            if ((nPriceToBuy * nVolumeToBuy * (1 + BUY_COMMISION)) > nCurDeposit)
                            {
                                nVolumeToBuy = (int)(nCurDeposit / (nPriceToBuy * (1 + BUY_COMMISION)));
                            }

                            if (nVolumeToBuy > 0)
                            {
                                AddListView(new UnTradedInfo(nOrderNum++, nCurTime, "매수", nVolumeToBuy, nPriceToBuy, nCurIdx));
                                nCurDeposit -= (int)(nVolumeToBuy * targetPrice * (1 + BUY_COMMISION));
                                DisplayCurInfos();
                                DrawChartUntilIdx(true);
                            }
                        };
                    }
                    else if (eBuyMode == TRADE_MODE.SELL_MODE)
                    {
                        double yCoord = historyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                        if (double.IsNaN(yCoord))
                            yCoord = 0;
                        else
                        {
                            int targetPrice = GetAdjustedPrice(yCoord, false);

                            int nPriceToSell = targetPrice;
                            int nVolumeToSell = nMouseWheel;

                            if (nPossibleVolume < nVolumeToSell)
                                nVolumeToSell = nPossibleVolume;

                            // TODO. 매도 미체결 삽입
                            if (nVolumeToSell > 0)
                            {
                                AddListView(new UnTradedInfo(nOrderNum++, nCurTime, "매도", nVolumeToSell, nPriceToSell, nCurIdx));
                                nPossibleVolume -= nVolumeToSell;
                                DisplayCurInfos();
                                DrawChartUntilIdx(true);
                            }
                        }
                    }
                    else // 예약이 있는 지
                    {
                        if (historyChart.Series["MinuteStick"].Points.Count > 0)
                        {
                            double yCoord = historyChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                            if (double.IsNaN(yCoord))
                                yCoord = 0;
                            else
                            {
                                if (nCurReserve != INIT_RESERVE)
                                {
                                    if (nCurReserve == UP_BUY_RESERVE) //  N 이상
                                    {
                                        reserveArr[UP_BUY_RESERVE].isSelected = true;
                                        reserveArr[UP_BUY_RESERVE].isChosen = false;
                                        reserveArr[UP_BUY_RESERVE].nReserveTime = nCurTime;
                                        reserveArr[UP_BUY_RESERVE].nReserveVolume = nMouseWheel;
                                        reserveArr[UP_BUY_RESERVE].fTouchLine = yCoord;
                                        reserveArr[UP_BUY_RESERVE].nAdjustedPrice = GetAdjustedPrice(yCoord);
                                        reserveArr[UP_BUY_RESERVE].nReservedIdx = nCurIdx;

                                    }
                                    else if (nCurReserve == DOWN_BUY_RESERVE) // M 이하
                                    {
                                        reserveArr[DOWN_BUY_RESERVE].isSelected = true;
                                        reserveArr[DOWN_BUY_RESERVE].isChosen = false;
                                        reserveArr[DOWN_BUY_RESERVE].nReserveTime = nCurTime;
                                        reserveArr[DOWN_BUY_RESERVE].nReserveVolume = nMouseWheel;
                                        reserveArr[DOWN_BUY_RESERVE].fTouchLine = yCoord;
                                        reserveArr[DOWN_BUY_RESERVE].nAdjustedPrice = GetAdjustedPrice(yCoord);
                                        reserveArr[DOWN_BUY_RESERVE].nReservedIdx = nCurIdx;

                                    }
                                    else if (nCurReserve == DOWN_SELL_RESERVE)
                                    {
                                        reserveArr[DOWN_SELL_RESERVE].isSelected = true;
                                        reserveArr[DOWN_SELL_RESERVE].isChosen = false;
                                        reserveArr[DOWN_SELL_RESERVE].nReserveTime = nCurTime;
                                        reserveArr[DOWN_SELL_RESERVE].nReserveVolume = nMouseWheel;
                                        reserveArr[DOWN_SELL_RESERVE].fTouchLine = yCoord;
                                        reserveArr[DOWN_SELL_RESERVE].nAdjustedPrice = GetAdjustedPrice(yCoord, false);
                                        reserveArr[DOWN_SELL_RESERVE].nReservedIdx = nCurIdx;
                                    }
                                }
                                else // 0으로 설정 가능
                                {

                                }
                            }
                        }
                    }

                }
                else if (e.Button == MouseButtons.Middle)
                {
                    eBuyMode = SwitchTradeMode(eBuyMode);
                    if (eBuyMode == TRADE_MODE.NONE_MODE)
                        ClearBuyMode();
                    else
                        buyModeLabel.Text = $"buy : {eBuyMode}";
                }
            }
            catch { }
        }

        public int nMouseWheel = 0;

        public void MouseWheelEventHandler(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // Scrolled up
            {
                nMouseWheel++;
            }
            else if (e.Delta < 0)
            {
                nMouseWheel--;
            }
            wheelLabel.Text = $"wheel : {nMouseWheel}";
        }


        public void ClearBuyMode(bool wheelInit = true)
        {
            eBuyMode = TRADE_MODE.NONE_MODE;
            buyModeLabel.Text = $"buy : {eBuyMode}";
            if (wheelInit)
            {
                nMouseWheel = 0;
                wheelLabel.Text = $"wheel : 0";
            }
        }

        public TRADE_MODE SwitchTradeMode(TRADE_MODE m)
        {
            if (m == TRADE_MODE.NONE_MODE)
                return TRADE_MODE.BUY_MODE;
            else if (m == TRADE_MODE.BUY_MODE)
                return TRADE_MODE.SELL_MODE;
            else
                return TRADE_MODE.NONE_MODE;
        }

        private void initializeChartDesign()
        {
            // Y축의 라벨 표시 형식 설정
            historyChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,0"; // 천 단위마다 구분

            // 차트 색상
            historyChart.Series["MinuteStick"].SetCustomProperty("PriceUpColor", "Red");
            historyChart.Series["MinuteStick"].SetCustomProperty("PriceDownColor", "Blue");

            historyChart.Legends["Legend1"].Enabled = false;
            // 선 색상 설정
            historyChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            historyChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Y축 간격 모드
            historyChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

            // 이평선
            historyChart.Series["Ma20m"].ChartType = SeriesChartType.Line;
            historyChart.Series["Ma1h"].ChartType = SeriesChartType.Line;
            historyChart.Series["Ma2h"].ChartType = SeriesChartType.Line;

            historyChart.Series["Ma20m"].Points.Clear();
            historyChart.Series["Ma1h"].Points.Clear();
            historyChart.Series["Ma2h"].Points.Clear();

            historyChart.Series["Ma20mGap"].ChartType = SeriesChartType.Line;
            historyChart.Series["Ma1hGap"].ChartType = SeriesChartType.Line;
            historyChart.Series["Ma2hGap"].ChartType = SeriesChartType.Line;


            historyChart.Series["Ma20mGap"].Points.Clear();
            historyChart.Series["Ma1hGap"].Points.Clear();
            historyChart.Series["Ma2hGap"].Points.Clear();
        }

        // DateTimePicker format 초기 셋팅하기
        private void initializeDateTimePicker()
        {
            dateTimePicker1.CustomFormat = DATEFORMAT;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_action(sender);
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker_action(sender);
            }
        }
        public DateTime searchDateTime;
        private async Task dateTimePicker_action(object sender)
        {
            ClearChart();
            searchDateTime = dateTimePicker1.Value;
            searchCode = sCodeTextBox.Text;

            String sdt = searchDateTime.ToString(DATEFORMAT);
            if (searchDate != sdt)
            {
                searchDate = sdt;
                if (searchCode != "")
                {
                    await InitDrawAsync();
                }
            }
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "종목명/종목코드")
            { //텍스트박스 내용이 사용자가 입력한 값이 아닌 Placeholder일 경우에만, 커서 포커스일때 빈칸으로 만들기
                txt.ForeColor = Color.Black; //사용자 입력 진한 글씨
                txt.Text = string.Empty;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                //사용자 입력값이 하나도 없는 경우에 포커스 잃으면 Placeholder 적용해주기
                txt.ForeColor = Color.DarkGray; //Placeholder 흐린 글씨
                txt.Text = "종목명/종목코드";
            }
        }

        private void sCodeTextBox_Leave(object sender, EventArgs e)
        {
            searchCode = sCodeTextBox.Text;
            Console.WriteLine("sCodeTextBox_Leave : " + searchCode);
        }

        private async void sCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClearChart();
            sCodeTextBox.ForeColor = Color.Black;
            if (e.KeyChar == (char)Keys.Enter)
            {
                searchCode = sCodeTextBox.Text;
                await InitDrawAsync();
            }
        }

        private void ViewCurTimerSet(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                timer.Enabled = true;
                Console.WriteLine("재생");
                ((Button)sender).Text = "❚❚";
                ((Button)sender).Font = new Font(((Button)sender).Font.FontFamily, 14);
            }
            else
            {
                timer.Enabled = false;
                Console.WriteLine("일시정지");
                ((Button)sender).Text = "▶";
                ((Button)sender).Font = new Font(((Button)sender).Font.FontFamily, 11);
            }
            DrawChartUntilIdx();
        }

        private async void compLocButton_ClickAsync(object sender, EventArgs e)
        {
            if (searchCompLoc == 0)
            {
                searchCompLoc = 1;
            }
            else
            {
                searchCompLoc = 0;
            }
            compLocButton.Text = searchCompLoc.ToString();
        }

        private void back10_Click(object sender, EventArgs e)
        {
            currentDrawIdx -= 10;
            if (currentDrawIdx < 0)
            {
                currentDrawIdx = 0;
            }

            DrawChartUntilIdx(true);
        }
        private void back5_Click(object sender, EventArgs e)
        {
            currentDrawIdx -= 5;
            if (currentDrawIdx < 0)
            {
                currentDrawIdx = 0;
            }

            DrawChartUntilIdx(true);
        }
        private void back60_Click(object sender, EventArgs e)
        {
            currentDrawIdx -= 60;
            if (currentDrawIdx < 0)
            {
                currentDrawIdx = 0;
            }

            DrawChartUntilIdx(true);
        }

        private void forward5_Click(object sender, EventArgs e)
        {
            currentDrawIdx += 5;
            if (currentDrawIdx >= timelines.Length)
            {
                currentDrawIdx = timelines.Length - 1;
            }

            DrawChartUntilIdx(true);
        }

        private void forward10_Click(object sender, EventArgs e)
        {
            currentDrawIdx += 10;
            if (currentDrawIdx >= timelines.Length)
            {
                currentDrawIdx = timelines.Length - 1;
            }

            DrawChartUntilIdx(true);
        }

        private void forward60_Click(object sender, EventArgs e)
        {
            currentDrawIdx += 60;
            if (currentDrawIdx >= timelines.Length)
            {
                currentDrawIdx = timelines.Length - 1;
            }

            DrawChartUntilIdx(true);
        }

        private void AddListView(UnTradedInfo info)
        {
            unTradedListView.Items.Clear();
            unTradedListView.BeginUpdate();
            unTradedInfoList.Add(info);
            unTradedListView.Items.AddRange(ReturnListViewItems(unTradedInfoList).ToArray());
            unTradedListView.EndUpdate();
        }

        private void UpdateListView()
        {
            unTradedListView.Items.Clear();
            unTradedListView.BeginUpdate();
            unTradedListView.Items.AddRange(ReturnListViewItems(unTradedInfoList).ToArray());
            unTradedListView.EndUpdate();
        }


        // 매수,매도 버튼 눌렸을때
        private void TradeButtonHandler(object sender, EventArgs e)
        {
            if (nCurTime > 0)
            {
                if (sender.Equals(buyButton)) // 매수 시도
                {
                    if (int.TryParse(buyPriceTxtBox.Text, out int priceResult) && int.TryParse(buyVolumeTxtBox.Text, out int volumeResult))
                    {
                        // TODO. 
                        // 내 예수금 한도까지
                        // 그리고 매수대금이 해당호가에 있는지 
                        if (hogaDictionary.ContainsKey(priceResult))
                        {
                            if ((priceResult * volumeResult * (1 + BUY_COMMISION)) > nCurDeposit)
                            {
                                volumeResult = (int)(nCurDeposit / (priceResult * (1 + BUY_COMMISION)));
                            }

                            if (volumeResult > 0)
                            {
                                AddListView(new UnTradedInfo(nOrderNum++, nCurTime, "매수", volumeResult, priceResult, nCurIdx));
                                nCurDeposit -= (int)(volumeResult * priceResult * (1 + BUY_COMMISION));
                                DisplayCurInfos();
                                DrawChartUntilIdx(true);
                            }

                        }
                        else
                        {
                            int pTest = GetAdjustedPrice(priceResult);
                            MessageBox.Show("매수가 수정이 필요");

                            buyPriceTxtBox.Text = pTest.ToString();
                        }
                    }
                }
                else if (sender.Equals(sellButton))
                {
                    if (nPossibleVolume > 0)
                    {
                        if (int.TryParse(sellPriceTxtBox.Text, out int priceResult) && int.TryParse(sellVolumeTxtBox.Text, out int volumeResult))
                        {
                            if (hogaDictionary.ContainsKey(priceResult))
                            {

                                if (nPossibleVolume < volumeResult)
                                    volumeResult = nPossibleVolume;
                                if (volumeResult > 0)
                                {
                                    AddListView(new UnTradedInfo(nOrderNum++, nCurTime, "매도", volumeResult, priceResult, nCurIdx));
                                    nPossibleVolume -= volumeResult;
                                    DrawChartUntilIdx(true);
                                }
                            }
                            else
                            {
                                int pTest = GetAdjustedPrice(priceResult, false);
                                MessageBox.Show("매도가 수정이 필요");
                                sellPriceTxtBox.Text = pTest.ToString();

                            }
                        }
                    }
                }
            }
        }

        private void TradeTextBoxClickHandler(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void nDelaySecondTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 문자가 숫자이거나 마침표(.)이거나 백스페이스인 경우 허용
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true; // 다른 문자 입력을 무시
            }

            // 마침표가 이미 입력되었을 때 추가 입력 방지
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                timer.Interval = (int)(double.Parse(nDelaySecondTextBox.Text) * 1000);
            }
        }

        public void ClearChart()
        {
            void Func()
            {
                historyChart.Series[0].Points.Clear();
                historyChart.Annotations.Clear();

                historyChart.Series["Ma20m"].Points.Clear();
                historyChart.Series["Ma1h"].Points.Clear();
                historyChart.Series["Ma2h"].Points.Clear();

                historyChart.Series["Ma20mGap"].Points.Clear();
                historyChart.Series["Ma1hGap"].Points.Clear();
                historyChart.Series["Ma2hGap"].Points.Clear();
            }

            if (historyChart.InvokeRequired)
            {
                historyChart.Invoke(new MethodInvoker(Func));
            }
            else
                Func();

        }

        // 초기화 함수
        private async Task InitDrawAsync()
        {

            timer.Enabled = false;

            // -------------- 차트 초기화 -----------------
            ClearChart();
            currentDrawIdx = 0;

            // -------------- 데이터 조회 -----------------
            // 조회 중 메시지 박스 표시
            loadingPanel.Visible = true;
            DisableControls(this);
            String codeType = IsFirstCharDigit(searchCode) ? "CODE" : "NAME";

            // 비동기로 db조회
            var timelinesTask = Task.Run(() => dataManager.getTimeLines(codeType, searchCode, searchDate, searchCompLoc).ToArray());
            var fakereportsTask = Task.Run(() => dataManager.getFakeReports(codeType, searchCode, searchDate, searchCompLoc).ToArray());

            await Task.WhenAll(timelinesTask, fakereportsTask);

            timelines = timelinesTask.Result;
            fakereports = fakereportsTask.Result;

            nCurTime = 0;
            nCurStartPrice = 0;
            nCurHighPrice = 0;
            nCurLowPrice = 0;
            nCurLastPrice = 0;
            nCurSpeed = 0;
            nCurIdx = 0;
            fCurPower = 0;

            nCurHitNum = 0;
            nCurHitType = 0;

            profitnLoss = 0; // 손익
            nCurDeposit = DEFAULT_DEPOSIT; // 현재예수금
            nCurPrice = 0; // 현재가
            nEveragePrice = 0; // 평단가
            pnLRatio = 0; // 손익률
            pnLPrice = 0;
            nOwnVolume = 0;// 보유수량
            nOwnTotalBuyedPrice = 0;
            nPossibleVolume = 0; // 가능수량

            nOrderNum = 0;

            hogaDictionary.Clear();
            hogaList.Clear();
            unTradedInfoList.Clear();
            tradedHistoryList.Clear();

            UpdateListView();

            ClearBuyMode();
            for (int i = 0; i < INIT_RESERVE; i++)
                reserveArr[i].Clear();

            hitDict25.Clear();
            hitDict38.Clear();
            hitDict312.Clear();
            hitDict410.Clear();


            isMouseCursorView = false;
            isHitView = true;

            isViewGapMa = true;
            isViewStartMa = true;
            isBuyArrowVisible = true;
            isSellArrowVisible = true;
            isFakeBuyArrowVisible = true;
            isFakeResistArrowVisible = true;
            isFakeAssistantArrowVisible = true;
            isFakeVolatilityArrowVisible = true;
            isFakeDownArrowVisible = true;
            isPaperBuyArrowVisible = true;
            isPaperSellArrowVisible = true;
            isRealBuyArrowVisible = true;
            isRealSellArrowVisible = true;
            isAllArrowVisible = true;

            DisplayCurInfos();
            ViewCurTimerSet(playButton, EventArgs.Empty);

            loadingPanel.Visible = false;
            EnableControls(this);




            try
            {
                fYesterdayPrice = timelines[0].fOverMa0Gap;
                fFirstPrice = timelines[0].fOverMa0;
                nCurTime = timelines[0].nTime;
                nCurHighPrice = timelines[0].nMaxFs;
                nCurLowPrice = timelines[0].nMinFs;
                nCurStartPrice = timelines[0].nStartFs;
                nCurLastPrice = timelines[0].nLastFs;

                int nTestPriceUp = (int)fYesterdayPrice + GetIntegratedMarketGap((int)fYesterdayPrice);
                int nTestPriceDown = (int)fYesterdayPrice - GetIntegratedMarketGap((int)fYesterdayPrice);


                while ((nTestPriceDown - fYesterdayPrice) / fYesterdayPrice >= -0.3)
                {
                    hogaDictionary[nTestPriceDown] = true;
                    hogaList.Add(nTestPriceDown);
                    nTestPriceDown -= GetIntegratedMarketGap(nTestPriceDown);
                }

                while ((nTestPriceUp - fYesterdayPrice) / fYesterdayPrice <= 0.3)
                {
                    hogaDictionary[nTestPriceUp] = true;
                    hogaList.Add(nTestPriceUp);
                    nTestPriceUp += GetIntegratedMarketGap(nTestPriceUp);
                }

                hogaList.Add((int)fYesterdayPrice);
                hogaDictionary[(int)fYesterdayPrice] = true;

                hogaList.Sort();

            }
            catch
            {

            }

            currentDrawIdx = 0;
        }

        private void DisableControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c != loadingPanel)
                    c.Enabled = false;
                DisableControls(c);
            }
        }

        private void EnableControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                c.Enabled = true;
                EnableControls(c);
            }
        }


        public double fFirstPrice;
        public double fYesterdayPrice;

        public void DrawChartUntilIdx(bool isForceDraw = false)
        {
            void Func()
            {
                if (timer.Enabled || isForceDraw)
                {
                    if (timelines.Length == 0)
                    {
                        ViewCurTimerSet(playButton, EventArgs.Empty);
                        ClearChart();
                        MessageBox.Show("No Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (currentDrawIdx >= timelines.Length)
                    {
                        return;
                    }

                    if (isForceDraw)
                        currentDrawIdx--;

                    ClearChart();

                    hitDict25.Clear();
                    hitDict38.Clear();
                    hitDict312.Clear();
                    hitDict410.Clear();

                    int nArrowCnt = 0;
                    int nFakeBuyArrowTotCnt = 0;
                    int nFakeResistArrowTotCnt = 0;
                    int nFakeAssistantArrowTotCnt = 0;
                    int nFakeVolatilityArrowTotCnt = 0;
                    int nFakeDownArrowTotCnt = 0;
                    int nPaperBuyArrowTotCnt = 0;


                    for (int i = 0; i < currentDrawIdx; i++)
                    {
                        displayedTimelines = new TimeLine[currentDrawIdx];
                        Array.Copy(timelines, displayedTimelines, currentDrawIdx);



                        //if (nFirstPrice == 0)
                        //    nFirstPrice = displayedTimelines[i].fOverMa0Gap;

                        int displayTime = displayedTimelines[i].nTime;

                        // 차트 스케일 조정
                        int maxY = displayedTimelines.Max(timeline => timeline.nMaxFs);
                        int minY = displayedTimelines.Min(timeline => timeline.nMinFs);

                        if (isViewGapMa)
                        {
                            int maxYGap = (int)displayedTimelines.Max(timeLine => timeLine.fOverMa0Gap);
                            maxYGap = (int)displayedTimelines.Max(timeLine => timeLine.fOverMa1Gap);
                            maxYGap = (int)displayedTimelines.Max(timeLine => timeLine.fOverMa2Gap);


                            int minYGap = (int)displayedTimelines.Min(timeLine => timeLine.fOverMa0Gap);
                            minYGap = (int)displayedTimelines.Min(timeLine => timeLine.fOverMa1Gap);
                            minYGap = (int)displayedTimelines.Min(timeLine => timeLine.fOverMa2Gap);

                            maxY = maxY > maxYGap ? maxY : maxYGap;
                            minY = minY < minYGap ? minY : minYGap;

                        }

                        if (isViewStartMa)
                        {
                            int maxYGap = (int)displayedTimelines.Max(timeLine => timeLine.fOverMa0);
                            maxYGap = (int)displayedTimelines.Max(timeLine => timeLine.fOverMa1);
                            maxYGap = (int)displayedTimelines.Max(timeLine => timeLine.fOverMa2);


                            int minYGap = (int)displayedTimelines.Min(timeLine => timeLine.fOverMa0);
                            minYGap = (int)displayedTimelines.Min(timeLine => timeLine.fOverMa1);
                            minYGap = (int)displayedTimelines.Min(timeLine => timeLine.fOverMa2);

                            maxY = maxY > maxYGap ? maxY : maxYGap;
                            minY = minY < minYGap ? minY : minYGap;
                        }

                        double padding = GetIntegratedMarketGap(displayedTimelines[0].nStartFs); // 여백
                        historyChart.ChartAreas[0].AxisY.Maximum = maxY + padding;
                        historyChart.ChartAreas[0].AxisY.Minimum = minY - padding;

                        // Y축의 눈금 조정
                        historyChart.ChartAreas[0].AxisY.Interval = YIntervalGap(maxY - minY); // Y축 간격

                        // 현재 값들 설정
                        nCurTime = displayedTimelines[i].nTime;
                        nCurHighPrice = displayedTimelines[i].nMaxFs;
                        nCurLowPrice = displayedTimelines[i].nMinFs;
                        nCurStartPrice = displayedTimelines[i].nStartFs;
                        nCurLastPrice = displayedTimelines[i].nLastFs;
                        nCurPrice = displayedTimelines[i].nLastFs;
                        nCurSpeed = displayedTimelines[i].nCount;
                        fCurPower = (displayedTimelines[i].nLastFs - fYesterdayPrice) / fYesterdayPrice;
                        nCurIdx = i;

                        // 그래프 그리기
                        // Console.WriteLine($"sDate: {displayedTimelines[i].sDate}, sCodeName: {displayedTimelines[i].sCodeName}, nIdx: {displayedTimelines[i].nIdx}");
                        historyChart.Series["MinuteStick"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].nMaxFs);
                        historyChart.Series["MinuteStick"].Points[i].YValues[1] = displayedTimelines[i].nMinFs;
                        historyChart.Series["MinuteStick"].Points[i].YValues[2] = displayedTimelines[i].nStartFs;
                        historyChart.Series["MinuteStick"].Points[i].YValues[3] = displayedTimelines[i].nLastFs;
                        historyChart.Series["MinuteStick"].Points[i].ToolTip =
                                $"해당시각 : {nCurTime},  인덱스 : {displayedTimelines[i].nIdx}{NEW_LINE}" +
                                            $"종가 : {displayedTimelines[i].nLastFs}{NEW_LINE}" +
                                            $"시가 : {displayedTimelines[i].nStartFs}{NEW_LINE}" +
                                            $"고가 : {displayedTimelines[i].nMaxFs}{NEW_LINE}" +
                                            $"저가 : {displayedTimelines[i].nMinFs}{NEW_LINE}" +
                                            $"거래량 : {displayedTimelines[i].nTotalVolume}, 매수/매도비율 : {Math.Round(displayedTimelines[i].fVolumeRatio, 2)}(%){NEW_LINE}" +
                                            $"속도 : {displayedTimelines[i].nCount}{NEW_LINE}" +
                                            $"*거래대금 : {Math.Round((double)(displayedTimelines[i].fTotalPrice), 2)}(백만원){NEW_LINE}" +
                                            $"매수대금 : {Math.Round((double)(displayedTimelines[i].fBuyPrice), 2)}(백만원){NEW_LINE}" +
                                            $"매도대금 : {Math.Round((double)(displayedTimelines[i].fSellPrice), 2)}(백만원){NEW_LINE}" +
                                            $"누적상승 : {Math.Round(displayedTimelines[i].fAccumUpPower, 2)}(%),  누적하락 : {Math.Round(displayedTimelines[i].fAccumDownPower, 2)}(%){NEW_LINE}" +
                                            $"손익률 : {Math.Round(((double)(displayedTimelines[i].nLastFs - displayedTimelines[i].nStartFs) / fYesterdayPrice) * 100, 2)}(%){NEW_LINE}" +
                                            $"T각도 : {Math.Round(displayedTimelines[i].fMedianAngle, 2)}{NEW_LINE}" +
                                            $"*H각도 : {Math.Round(displayedTimelines[i].fHourAngle, 2)}{NEW_LINE}" +
                                            $"*R각도 : {Math.Round(displayedTimelines[i].fRecentAngle, 2)}{NEW_LINE}" +
                                            $"*D각도 : {Math.Round(displayedTimelines[i].fDAngle, 2)}{NEW_LINE}" +
                                            $"*Ma20m > Ma1h: {(displayedTimelines[i].fOverMa0 > displayedTimelines[i].fOverMa1)}{NEW_LINE}" +
                                            $"*Ma1h > Ma2h: {(displayedTimelines[i].fOverMa1 > displayedTimelines[i].fOverMa2)}{NEW_LINE}" +
                                            $"Ma20m > Ma2h: {(displayedTimelines[i].fOverMa0 > displayedTimelines[i].fOverMa2)}{NEW_LINE}" +
                                            $"*Ma20m-- : {displayedTimelines[i].nDownTimeOverMa0}{NEW_LINE}" +
                                            $"*Ma1h-- : {displayedTimelines[i].nDownTimeOverMa1}{NEW_LINE}" +
                                            $"*Ma2h-- : {displayedTimelines[i].nDownTimeOverMa2}{NEW_LINE}" +
                                            $"Ma20m++ : {displayedTimelines[i].nUpTimeOverMa0}{NEW_LINE}" +
                                            $"Ma1h++ : {displayedTimelines[i].nUpTimeOverMa1}{NEW_LINE}" +
                                            $"Ma2h++ : {displayedTimelines[i].nUpTimeOverMa2}{NEW_LINE}" +
                                            $"*총순위 : {displayedTimelines[i].nSummationRanking}위( {displayedTimelines[i].nSummationMove} ){NEW_LINE}" +
                                            $"*분당순위 : {displayedTimelines[i].nMinuteRanking} 위";

                        // 이평선 그리기

                        if (isViewStartMa)
                        {
                            historyChart.Series["Ma20m"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa0);
                            historyChart.Series["Ma1h"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa1);
                            historyChart.Series["Ma2h"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa2);
                        }

                        if (isViewGapMa)
                        {
                            historyChart.Series["Ma20mGap"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa0Gap);
                            historyChart.Series["Ma1hGap"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa1Gap);
                            historyChart.Series["Ma2hGap"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa2Gap);
                        }

                        // 화살표 그리기
                        Console.WriteLine("displayTime: " + displayTime);
                        List<FakeReport> fakereportList = GetFakeReportsByMin(displayTime);


                        int nFakeBuyArrowCnt = 0;
                        int nFakeResistArrowCnt = 0;
                        int nFakeAssistantArrowCnt = 0;
                        int nFakeVolatilityArrowCnt = 0;
                        int nFakeDownArrowCnt = 0;
                        int nPaperBuyArrowCnt = 0;

                        string sFakeBuyArrowToolTip = "";
                        string sFakeResistArrowToolTip = "";
                        string sFakeAssistantArrowToolTip = "";
                        string sFakeVolatilityArrowToolTip = "";
                        string sFakeDownArrowToolTip = "";
                        string sPaperBuyArrowToolTip = "";

                        for (int j = 0; j < fakereportList.Count; j++)
                        {
                            FakeReport fakereport = fakereportList[j];
                            ArrowAnnotation arrowAnnotation = new ArrowAnnotation();
                            nArrowCnt += 1;



                            switch (fakereport.nBuyStrategyGroupNum)
                            {
                                case ArrowColors.FAKE_BUY_SIGNAL:
                                    nFakeBuyArrowTotCnt += 1;
                                    nFakeBuyArrowCnt += 1;

                                    if (isFakeBuyArrowVisible)
                                    {

                                        if (!isArrowGrouping)
                                        {
                                            arrowAnnotation.Width = -2; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }
                                        else
                                        {
                                            arrowAnnotation.Width = -0.7; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }

                                        if (nFakeBuyArrowCnt == 1)
                                        {
                                            arrowAnnotation.Height = -4;
                                        }
                                        else
                                        {
                                            for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                            {
                                                if (historyChart.Annotations[k].Name.Equals("FB" + i + "." + (nFakeBuyArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                                {
                                                    historyChart.Annotations.RemoveAt(k);
                                                }
                                            }
                                            arrowAnnotation.Height = -7;
                                        }

                                        arrowAnnotation.AnchorOffsetY = -1.5;

                                        sFakeBuyArrowToolTip +=
                                            $"*중첩 : {nFakeBuyArrowCnt}( {nFakeBuyArrowTotCnt} )  가짜전략 : {fakereport.nBuyStrategyIdx}  주문시간 : {fakereport.nRqTime}  페이크매수가격 : {fakereport.nOverPrice}(원){NEW_LINE}" +
                                          //$"페이크명 : {strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx]}{NEW_LINE}{NEW_LINE}";
                                          $"{NEW_LINE}{NEW_LINE}";
                                        arrowAnnotation.ToolTip = $"*페이크매수 총 갯수 : {nFakeBuyArrowCnt}개\n" +
                                               $"=====================================================\n" + sFakeBuyArrowToolTip;

                                        arrowAnnotation.BackColor = Color.Orange;
                                        arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                        arrowAnnotation.Name = "FB" + i + "." + nFakeBuyArrowCnt;
                                        arrowAnnotation.LineColor = Color.Black;

                                        historyChart.Annotations.Add(arrowAnnotation);

                                    }
                                    break;
                                case ArrowColors.FAKE_RESIST_SIGNAL:
                                    nFakeResistArrowTotCnt += 1;
                                    nFakeResistArrowCnt += 1;
                                    if (isFakeResistArrowVisible)
                                    {

                                        if (!isArrowGrouping)
                                        {
                                            arrowAnnotation.Width = 0; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }
                                        else
                                        {
                                            arrowAnnotation.Width = -0.1; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세\
                                        }
                                        arrowAnnotation.Height = -4;// -면 아래쪽방향, + 면 위쪽방향
                                        arrowAnnotation.AnchorOffsetY = -1.5;

                                        sFakeResistArrowToolTip +=
                                          $"*중첩 : {nFakeResistArrowCnt}( {nFakeResistArrowTotCnt} )  가짜전략 : {fakereport.nBuyStrategyIdx}  주문시간 : {fakereport.nRqTime}  페이크저항가격 : {fakereport.nOverPrice}(원){NEW_LINE}" +
                                          $"{NEW_LINE}{NEW_LINE}";  // $"페이크명 : {strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx]}{NEW_LINE}{NEW_LINE}";

                                        arrowAnnotation.ToolTip = $"주문인덱스 : {i}\n" +
                                            $"*페이크저항 총 갯수 : {nFakeResistArrowCnt}개\n" +
                                           $"=====================================================\n" + sFakeResistArrowToolTip;
                                        if (nFakeResistArrowCnt == 1)
                                            arrowAnnotation.Height = -4;
                                        else
                                        {
                                            for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                            {
                                                if (historyChart.Annotations[k].Name.Equals("FR" + i + "." + (nFakeResistArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                                {
                                                    historyChart.Annotations.RemoveAt(k);
                                                }
                                            }
                                            arrowAnnotation.Height = -7;
                                        }
                                        arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                        arrowAnnotation.Name = "FR" + i + "." + nFakeResistArrowCnt;
                                        arrowAnnotation.BackColor = Color.Green;
                                        arrowAnnotation.LineColor = Color.Black;

                                        historyChart.Annotations.Add(arrowAnnotation);
                                    }
                                    break;
                                case ArrowColors.FAKE_ASSISTANT_SIGNAL:
                                    nFakeAssistantArrowTotCnt += 1;
                                    nFakeAssistantArrowCnt += 1;
                                    if (isFakeAssistantArrowVisible)
                                    {

                                        if (!isArrowGrouping)
                                        {
                                            arrowAnnotation.Width = -1; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }
                                        else
                                        {
                                            arrowAnnotation.Width = -0.4; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }

                                        arrowAnnotation.Height = -4;// -면 아래쪽방향, + 면 위쪽방향
                                        arrowAnnotation.AnchorOffsetY = -1.5;

                                        sFakeAssistantArrowToolTip +=
                                          $"*중첩 : {nFakeAssistantArrowCnt}( {nFakeAssistantArrowTotCnt} )  가짜전략 : {fakereport.nBuyStrategyIdx}  주문시간 : {fakereport.nRqTime}  페이크보조가격 : {fakereport.nOverPrice}(원){NEW_LINE}" +
                                          $"{NEW_LINE}{NEW_LINE}";  // $"페이크명 : {strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx]}{NEW_LINE}{NEW_LINE}";

                                        arrowAnnotation.ToolTip =
                                        $"*페이크보조 총 갯수 : {nFakeAssistantArrowCnt}개\n" +
                                        $"주문인덱스 : {i}\n" +
                                           $"=====================================================\n" + sFakeAssistantArrowToolTip;
                                        if (nFakeAssistantArrowCnt == 1)
                                            arrowAnnotation.Height = -4;
                                        else
                                        {
                                            for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                            {
                                                if (historyChart.Annotations[k].Name.Equals("FA" + i + "." + (nFakeAssistantArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                                {
                                                    historyChart.Annotations.RemoveAt(k);
                                                }
                                            }
                                            arrowAnnotation.Height = -7;
                                        }
                                        arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                        arrowAnnotation.Name = "FA" + i + "." + nFakeAssistantArrowCnt;
                                        arrowAnnotation.BackColor = Color.Yellow;
                                        arrowAnnotation.LineColor = Color.Black;

                                        historyChart.Annotations.Add(arrowAnnotation);
                                    }
                                    break;

                                case ArrowColors.FAKE_VOLATILE_SIGNAL:
                                    nFakeVolatilityArrowTotCnt += 1;
                                    nFakeVolatilityArrowCnt += 1;
                                    if (isFakeVolatilityArrowVisible)
                                    {

                                        if (!isArrowGrouping)
                                        {
                                            arrowAnnotation.Width = 2; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }
                                        else
                                        {
                                            arrowAnnotation.Width = 0.5; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세

                                        }
                                        arrowAnnotation.Height = -4;// -면 아래쪽방향, + 면 위쪽방향
                                        arrowAnnotation.AnchorOffsetY = -1.5;
                                        sFakeVolatilityArrowToolTip +=
                                        $"*중첩 : {nFakeVolatilityArrowCnt}( {nFakeVolatilityArrowTotCnt} )  변동성전략 : {fakereport.nBuyStrategyIdx}  주문시간 : {fakereport.nRqTime}  변동성가격 : {fakereport.nOverPrice}(원){NEW_LINE}" +
                                        $"{NEW_LINE}{NEW_LINE}";  // $"변동성명 : {strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx]}{NEW_LINE}{NEW_LINE}";

                                        arrowAnnotation.ToolTip = $"*변동성 총 갯수 : {nFakeVolatilityArrowCnt}개\n" +
                                           $"=====================================================\n" + sFakeVolatilityArrowToolTip;
                                        if (nFakeVolatilityArrowCnt == 1)
                                            arrowAnnotation.Height = -4;
                                        else
                                        {
                                            for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                            {
                                                if (historyChart.Annotations[k].Name.Equals("FV" + i + "." + (nFakeVolatilityArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                                {
                                                    historyChart.Annotations.RemoveAt(k);
                                                }
                                            }
                                            arrowAnnotation.Height = -7;
                                        }
                                        arrowAnnotation.BackColor = Color.Navy;
                                        arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);

                                        arrowAnnotation.Name = "FV" + i + "." + nFakeVolatilityArrowCnt;
                                        arrowAnnotation.LineColor = Color.Black;

                                        historyChart.Annotations.Add(arrowAnnotation);
                                    }
                                    break;
                                case ArrowColors.FAKE_DOWN_SIGNAL:
                                    nFakeDownArrowTotCnt += 1;
                                    nFakeDownArrowCnt += 1;
                                    if (isFakeDownArrowVisible)
                                    {

                                        if (!isArrowGrouping)
                                        {
                                            arrowAnnotation.Width = 3; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }
                                        else
                                        {
                                            arrowAnnotation.Width = 0.8; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세

                                        }
                                        arrowAnnotation.Height = -4;// -면 아래쪽방향, + 면 위쪽방향
                                        arrowAnnotation.AnchorOffsetY = -1.5;
                                        sFakeDownArrowToolTip +=
                                        $"*중첩 : {nFakeDownArrowCnt}( {nFakeDownArrowTotCnt} )  페이크 다운전략 : {fakereport.nBuyStrategyIdx}  주문시간 : {fakereport.nRqTime}  페이크 다운 가격 : {fakereport.nOverPrice}(원){NEW_LINE}" +
                                        $"{NEW_LINE}{NEW_LINE}";  // $"페이크 다운명 : {strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx]}{NEW_LINE}{NEW_LINE}";

                                        arrowAnnotation.ToolTip = $"*페이크 다운 총 갯수 : {nFakeDownArrowCnt}개\n" +
                                           $"=====================================================\n" + sFakeDownArrowToolTip;
                                        if (nFakeDownArrowCnt == 1)
                                            arrowAnnotation.Height = -4;
                                        else
                                        {
                                            for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                            {
                                                if (historyChart.Annotations[k].Name.Equals("FD" + i + "." + (nFakeDownArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                                {
                                                    historyChart.Annotations.RemoveAt(k);
                                                }
                                            }
                                            arrowAnnotation.Height = -7;
                                        }
                                        arrowAnnotation.BackColor = Color.Purple;
                                        arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);

                                        arrowAnnotation.Name = "FD" + i + "." + nFakeDownArrowCnt;
                                        arrowAnnotation.LineColor = Color.Black;

                                        historyChart.Annotations.Add(arrowAnnotation);
                                    }
                                    break;

                                case ArrowColors.PAPER_BUY_SIGNAL:
                                    nPaperBuyArrowTotCnt += 1;
                                    nPaperBuyArrowCnt += 1;
                                    if (isPaperBuyArrowVisible)
                                    {

                                        if (!isArrowGrouping)
                                        {
                                            arrowAnnotation.Width = -3; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                        }
                                        else
                                        {
                                            arrowAnnotation.Width = -1; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세

                                        }
                                        arrowAnnotation.Height = -4;// -면 아래쪽방향, + 면 위쪽방향
                                        arrowAnnotation.AnchorOffsetY = -1.5;

                                        sPaperBuyArrowToolTip +=
                                                     $"매수요청시간 : {fakereport.nRqTime} \n" +
                                                     $"매수블록ID : {nPaperBuyArrowTotCnt}  주문가격(수량) : {fakereport.nOverPrice}(원)\n" +
                                                     $"{NEW_LINE}{NEW_LINE}";  // "매수설명 : " + strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx] + "\n\n";

                                        arrowAnnotation.ToolTip = $"*모의매수 총 갯수 : {nPaperBuyArrowCnt}개\n" +
                                               $"=====================================================\n" + sPaperBuyArrowToolTip;
                                        if (nPaperBuyArrowCnt == 1)
                                            arrowAnnotation.Height = -4;
                                        else
                                        {
                                            for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                            {
                                                if (historyChart.Annotations[k].Name.Equals("P" + i + "." + (nPaperBuyArrowCnt - 1)))  // P + 해당분봉의 최근삽입정보
                                                {
                                                    historyChart.Annotations.RemoveAt(k);
                                                }
                                            }
                                            arrowAnnotation.Height = -7;
                                        }

                                        arrowAnnotation.BackColor = Color.Red;
                                        arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                        // arrowFakeBuy.AnchorY = historyChart.Series["MinuteStick"].Points[curEa.fakeBuyStrategy.arrMinuteIdx[p]].YValues[1]; // 고.저.시종
                                        arrowAnnotation.Name = "P" + i + "." + nPaperBuyArrowCnt;
                                        arrowAnnotation.LineColor = Color.Black;

                                        historyChart.Annotations.Add(arrowAnnotation);
                                    }
                                    break;

                            }

                        }
                        int nHitNum = nFakeBuyArrowCnt + nFakeAssistantArrowCnt + nFakeResistArrowCnt + nFakeVolatilityArrowCnt + nFakeDownArrowCnt + nPaperBuyArrowCnt;
                        int nHitType = 0;

                        if (nFakeBuyArrowCnt > 0)
                            nHitType++;
                        if (nFakeAssistantArrowCnt > 0)
                            nHitType++;
                        if (nFakeResistArrowCnt > 0)
                            nHitType++;
                        if (nFakeVolatilityArrowCnt > 0)
                            nHitType++;
                        if (nFakeDownArrowCnt > 0)
                            nHitType++;
                        if (nPaperBuyArrowCnt > 0)
                            nHitType++;

                        if (nHitType >= 2 && nHitNum >= 5)
                            hitDict25[i] = nCurPrice;
                        if (nHitType >= 3 && nHitNum >= 8)
                            hitDict38[i] = nCurPrice;
                        if (nHitType >= 3 && nHitNum >= 12)
                            hitDict312[i] = nCurPrice;
                        if (nHitType >= 4 && nHitNum >= 10)
                            hitDict410[i] = nCurPrice;

                        nCurHitNum = nHitNum;
                        nCurHitType = nHitType;


                        if (displayedTimelines[i].nCount > 0)
                        {
                            // 예약 계산
                            for (int rNum = 0; rNum < INIT_RESERVE; rNum++)
                            {
                                if (reserveArr[rNum].isSelected && !reserveArr[rNum].isChosen)
                                {

                                    if (reserveArr[rNum].nReserveTime <= nCurTime)
                                    {
                                        if (rNum == UP_BUY_RESERVE)
                                        {
                                            if (reserveArr[rNum].fTouchLine <= nCurHighPrice)
                                            {
                                                reserveArr[rNum].isChosen = true;

                                                // 가격조정이 필요


                                                if ((reserveArr[rNum].nAdjustedPrice * reserveArr[rNum].nReserveVolume * (1 + BUY_COMMISION)) > nCurDeposit)
                                                {
                                                    reserveArr[rNum].nReserveVolume = (int)(nCurDeposit / (reserveArr[rNum].nAdjustedPrice * (1 + BUY_COMMISION)));
                                                }

                                                if (reserveArr[rNum].nReserveVolume > 0)
                                                {
                                                    AddListView(new UnTradedInfo(nOrderNum++, reserveArr[rNum].nReserveTime - (UP_BUY_RESERVE + 1), "매수", reserveArr[rNum].nReserveVolume, reserveArr[rNum].nAdjustedPrice + GetIntegratedMarketGap(reserveArr[rNum].nAdjustedPrice), reserveArr[rNum].nReservedIdx));
                                                    nCurDeposit -= (int)(reserveArr[rNum].nReserveVolume * (reserveArr[rNum].nAdjustedPrice + GetIntegratedMarketGap(reserveArr[rNum].nAdjustedPrice)) * (1 + BUY_COMMISION));
                                                    DisplayCurInfos();
                                                }
                                            }
                                        }
                                        else if (rNum == DOWN_BUY_RESERVE)
                                        {
                                            if (reserveArr[rNum].fTouchLine >= nCurLowPrice)
                                            {
                                                reserveArr[rNum].isChosen = true;
                                                if ((reserveArr[rNum].nAdjustedPrice * reserveArr[rNum].nReserveVolume * (1 + BUY_COMMISION)) > nCurDeposit)
                                                {
                                                    reserveArr[rNum].nReserveVolume = (int)(nCurDeposit / (reserveArr[rNum].nAdjustedPrice * (1 + BUY_COMMISION)));
                                                }

                                                if (reserveArr[rNum].nReserveVolume > 0)
                                                {
                                                    AddListView(new UnTradedInfo(nOrderNum++, reserveArr[rNum].nReserveTime - (DOWN_BUY_RESERVE + 1), "매수", reserveArr[rNum].nReserveVolume, reserveArr[rNum].nAdjustedPrice + GetIntegratedMarketGap(reserveArr[rNum].nAdjustedPrice), reserveArr[rNum].nReservedIdx));
                                                    nCurDeposit -= (int)(reserveArr[rNum].nReserveVolume * (reserveArr[rNum].nAdjustedPrice + GetIntegratedMarketGap(reserveArr[rNum].nAdjustedPrice)) * (1 + BUY_COMMISION));
                                                    DisplayCurInfos();
                                                }

                                            }
                                        }
                                        else if (rNum == DOWN_SELL_RESERVE)
                                        {
                                            if (reserveArr[rNum].fTouchLine >= nCurLowPrice)
                                            {
                                                reserveArr[rNum].isChosen = true;
                                                if (nPossibleVolume < reserveArr[rNum].nReserveVolume)
                                                    reserveArr[rNum].nReserveVolume = nPossibleVolume;

                                                if (reserveArr[rNum].nReserveVolume > 0)
                                                {
                                                    AddListView(new UnTradedInfo(nOrderNum++, reserveArr[rNum].nReserveTime - (DOWN_SELL_RESERVE + 1), "매도", reserveArr[rNum].nReserveVolume, reserveArr[rNum].nAdjustedPrice - GetIntegratedMarketGap(reserveArr[rNum].nAdjustedPrice), reserveArr[rNum].nReservedIdx));
                                                    nPossibleVolume -= reserveArr[rNum].nReserveVolume;
                                                    DisplayCurInfos();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // 미체결 잔고 계산
                            for (int unTradedIdx = 0; unTradedIdx < unTradedInfoList.Count; unTradedIdx++)
                            {
                                if (nCurTime > unTradedInfoList[unTradedIdx].nTime && nCurIdx > unTradedInfoList[unTradedIdx].nIdx) // 다음 분봉 이후부터 계산하기로 정함
                                {
                                    if (unTradedInfoList[unTradedIdx].sType.Equals("매수"))
                                    {
                                        if (unTradedInfoList[unTradedIdx].nPrice > nCurLowPrice + GetIntegratedMarketGap(nCurLowPrice)) // 혹은 더 비싸게 산다면
                                        {
                                            if (unTradedInfoList[unTradedIdx].nPrice >= nCurHighPrice + GetIntegratedMarketGap(nCurHighPrice))
                                            {
                                                nCurDeposit += (int)(unTradedInfoList[unTradedIdx].nPrice * unTradedInfoList[unTradedIdx].nVolume * (1 + BUY_COMMISION));
                                                nCurDeposit -= (int)((nCurHighPrice + GetIntegratedMarketGap(nCurHighPrice)) * unTradedInfoList[unTradedIdx].nVolume * (1 + BUY_COMMISION));
                                                unTradedInfoList[unTradedIdx].nPrice = nCurHighPrice + GetIntegratedMarketGap(nCurHighPrice);
                                            }

                                            // ok 체결완료
                                            nOwnVolume += unTradedInfoList[unTradedIdx].nVolume;
                                            nPossibleVolume += unTradedInfoList[unTradedIdx].nVolume;
                                            nOwnTotalBuyedPrice += unTradedInfoList[unTradedIdx].nVolume * unTradedInfoList[unTradedIdx].nPrice;
                                            nEveragePrice = nOwnTotalBuyedPrice / nOwnVolume;

                                            ThreadSoundTask(BUY_SIGNAL);
                                            tradedHistoryList.Add(new UnTradedInfo(unTradedInfoList[unTradedIdx].nNum, unTradedInfoList[unTradedIdx].nTime, unTradedInfoList[unTradedIdx].sType, unTradedInfoList[unTradedIdx].nVolume, unTradedInfoList[unTradedIdx].nPrice, (unTradedInfoList[unTradedIdx].nIdx == nCurIdx) ? nCurIdx + 1 : nCurIdx)); // unTradedInfoList[unTradedIdx].nIdx
                                            unTradedInfoList.RemoveAt(unTradedIdx--);
                                            UpdateListView();

                                        }
                                    }
                                    else if (unTradedInfoList[unTradedIdx].sType.Equals("매도"))
                                    {
                                        if (unTradedInfoList[unTradedIdx].nPrice < nCurHighPrice - GetIntegratedMarketGap(nCurHighPrice))
                                        {
                                            if (unTradedInfoList[unTradedIdx].nPrice <= nCurLowPrice - GetIntegratedMarketGap(nCurLowPrice))
                                            {
                                                unTradedInfoList[unTradedIdx].nPrice = nCurLowPrice - GetIntegratedMarketGap(nCurLowPrice);
                                            }

                                            nCurDeposit += (int)(unTradedInfoList[unTradedIdx].nPrice * unTradedInfoList[unTradedIdx].nVolume * (1 - (STOCK_TAX + SELL_COMMISION)));
                                            profitnLoss += (int)(
                                                ((unTradedInfoList[unTradedIdx].nPrice - nEveragePrice) * unTradedInfoList[unTradedIdx].nVolume
                                                - (unTradedInfoList[unTradedIdx].nPrice * unTradedInfoList[unTradedIdx].nVolume * (STOCK_TAX + SELL_COMMISION))
                                                - (nEveragePrice * unTradedInfoList[unTradedIdx].nVolume * BUY_COMMISION)));
                                            nOwnVolume -= unTradedInfoList[unTradedIdx].nVolume;
                                            nOwnTotalBuyedPrice -= nEveragePrice * unTradedInfoList[unTradedIdx].nVolume;

                                            if (nOwnVolume == 0)
                                            {
                                                pnLRatio = 0;
                                                pnLPrice = 0;
                                                nEveragePrice = 0;
                                                nOwnTotalBuyedPrice = 0;
                                            }

                                            ThreadSoundTask(SELL_SIGNAL);
                                            tradedHistoryList.Add(new UnTradedInfo(unTradedInfoList[unTradedIdx].nNum, unTradedInfoList[unTradedIdx].nTime, unTradedInfoList[unTradedIdx].sType, unTradedInfoList[unTradedIdx].nVolume, unTradedInfoList[unTradedIdx].nPrice, (unTradedInfoList[unTradedIdx].nIdx == nCurIdx) ? nCurIdx + 1 : nCurIdx)); // unTradedInfoList[unTradedIdx].nIdx
                                            unTradedInfoList.RemoveAt(unTradedIdx--);
                                            UpdateListView();
                                        }
                                    }
                                }

                            }
                        }

                    }

                    int nRealBuyTotalCnt = 0;
                    int nRealSellTotalCnt = 0;

                    int nRealBuyMinuteCnt = 0; // 분이 달라진다면 초기화 필요
                    int nRealSellMinuteCnt = 0;

                    int nRealBuyMinuteTimeIdx = 0; // 분 구분을 위한 테스트변수
                    int nRealSellMinuteTimeIdx = 0;

                    string sRealBuyMinuteToolTip = "";
                    string sRealSellMinuteToolTip = "";

                    for (int h = 0; h < tradedHistoryList.Count; h++)
                    {
                        ArrowAnnotation realAnnotation = new ArrowAnnotation();
                        if (tradedHistoryList[h].sType.Equals("매수"))
                        {
                            if (isRealBuyArrowVisible)
                            {

                                if (tradedHistoryList[h].nIdx < historyChart.Series["MinuteStick"].Points.Count)
                                {
                                    if (tradedHistoryList[h].nIdx != nRealBuyMinuteTimeIdx)
                                    {
                                        nRealBuyMinuteCnt = 0;
                                        sRealBuyMinuteToolTip = "";
                                    }
                                    nRealBuyMinuteTimeIdx = tradedHistoryList[h].nIdx;
                                    nRealBuyTotalCnt += 1;
                                    nRealBuyMinuteCnt += 1;


                                    if (!isArrowGrouping)
                                    {
                                        realAnnotation.Width = -3; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                    }
                                    else
                                    {
                                        realAnnotation.Width = -1; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                    }

                                    realAnnotation.AnchorOffsetY = +1.5;

                                    sRealBuyMinuteToolTip += $"번호 : { tradedHistoryList[h].nNum} 시간 : {tradedHistoryList[h].nTime} 가격 : {tradedHistoryList[h].nPrice} 수량 : {tradedHistoryList[h].nVolume}\n\n";

                                    realAnnotation.ToolTip =
                                        $"*실매수 총 갯수 : {nRealBuyMinuteCnt}개\n" +
                                        $"=====================================================\n" + sRealBuyMinuteToolTip;

                                    if (nRealBuyMinuteCnt == 1) // 해당데이터가 첫번째면은
                                        realAnnotation.Height = +4;
                                    else
                                    {
                                        for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                        {
                                            if (historyChart.Annotations[k].Name.Equals("Z" + nRealBuyMinuteTimeIdx + "." + (nRealBuyMinuteCnt - 1)))  // M + 해당분봉의 최근삽입정보
                                            {
                                                historyChart.Annotations.RemoveAt(k);
                                            }
                                        }
                                        realAnnotation.Height = +7;
                                    }

                                    realAnnotation.BackColor = Color.HotPink;
                                    realAnnotation.LineColor = Color.Black;

                                    int nBuyAnnotationIdx = tradedHistoryList[h].nIdx;

                                    realAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[nBuyAnnotationIdx]);
                                    realAnnotation.AnchorY = historyChart.Series["MinuteStick"].Points[nBuyAnnotationIdx].YValues[1]; // 고.저.시종
                                    realAnnotation.Name = "Z" + nRealBuyMinuteTimeIdx + "." + nRealBuyMinuteCnt;

                                    historyChart.Annotations.Add(realAnnotation);

                                }
                            }
                        }
                        else if (tradedHistoryList[h].sType.Equals("매도"))
                        {
                            if (isRealSellArrowVisible)
                            {
                                if (tradedHistoryList[h].nIdx < historyChart.Series["MinuteStick"].Points.Count)
                                {
                                    if (tradedHistoryList[h].nIdx != nRealSellMinuteTimeIdx)
                                    {
                                        nRealSellMinuteCnt = 0;
                                        sRealSellMinuteToolTip = "";
                                    }
                                    nRealSellMinuteTimeIdx = tradedHistoryList[h].nIdx;
                                    nRealSellTotalCnt += 1;
                                    nRealSellMinuteCnt += 1;


                                    if (!isArrowGrouping)
                                    {
                                        realAnnotation.Width = +1; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                    }
                                    else
                                    {
                                        realAnnotation.Width = +0.2; // -가 왼쪽으로 기움, + 가 오른쪽으로 기움 , 0이 수직자세
                                    }

                                    realAnnotation.AnchorOffsetY = +1.5;

                                    sRealSellMinuteToolTip += $"번호 : {tradedHistoryList[h].nNum} 시간 : {tradedHistoryList[h].nTime} 가격 : {tradedHistoryList[h].nPrice} 수량 : {tradedHistoryList[h].nVolume} \n\n";

                                    realAnnotation.ToolTip =
                                        $"*실매수 총 갯수 : {nRealSellMinuteCnt}개\n" +
                                        $"=====================================================\n" + sRealSellMinuteToolTip;

                                    if (nRealSellMinuteCnt == 1) // 해당데이터가 첫번째면은
                                        realAnnotation.Height = +4;
                                    else
                                    {
                                        for (int k = 0; k < historyChart.Annotations.Count; k++) // 어노테이션들 중
                                        {
                                            if (historyChart.Annotations[k].Name.Equals("X" + nRealSellMinuteTimeIdx + "." + (nRealSellMinuteCnt - 1)))  // M + 해당분봉의 최근삽입정보
                                            {
                                                historyChart.Annotations.RemoveAt(k);
                                            }
                                        }
                                        realAnnotation.Height = +7;
                                    }

                                    realAnnotation.BackColor = Color.LightSkyBlue;
                                    realAnnotation.LineColor = Color.Black;

                                    int nSellAnnotationIdx = tradedHistoryList[h].nIdx;


                                    realAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[nSellAnnotationIdx]);
                                    realAnnotation.AnchorY = historyChart.Series["MinuteStick"].Points[nSellAnnotationIdx].YValues[1]; // 고.저.시종
                                    realAnnotation.Name = "X" + nRealSellMinuteTimeIdx + "." + nRealSellMinuteCnt;

                                    historyChart.Annotations.Add(realAnnotation);

                                }
                            }
                        }

                    }


                    //  현재정보들 출력
                    CalcCurrentCashFlow();
                    DisplayCurInfos();

                    currentDrawIdx++;

                }
            }

            if (historyChart.InvokeRequired)
            {
                historyChart.Invoke(new MethodInvoker(Func));
            }
            else
                Func();
        }

        public static int AddTimeBySec(int timeToBeAdd, int addSec)
        {
            int secToBeAdd = (int)(timeToBeAdd / 10000) * 3600 + (int)(timeToBeAdd / 100) % 100 * 60 + timeToBeAdd % 100;
            secToBeAdd += addSec;
            int hour = secToBeAdd / 3600;
            int minute = (secToBeAdd % 3600) / 60;
            int second = secToBeAdd % 60;

            return hour * 10000 + minute * 100 + second;
        }

        public const int BUY_SIGNAL = 0;
        public const int SELL_SIGNAL = 1;
        public void ThreadSoundTask(int nType)
        {
            try
            {
                if (nType == 0)
                {
                    buySoundPlayer.Play();
                }
                else if (nType == 1)
                {
                    sellSoundPlayer.Play();
                }
            }
            catch { }
        }

        public bool isCtrlPushed = false;
        public void KeyDownHandler(object sender, KeyEventArgs e)
        {
            char cPressed = (char)e.KeyValue;

            if (cPressed == 49 || cPressed == 50 || cPressed == 57)
            {

                ClearBuyMode(false);

                if (cPressed == 49)
                {
                    nCurReserve = UP_BUY_RESERVE;
                }
                else if (cPressed == 50)
                {
                    nCurReserve = DOWN_BUY_RESERVE;
                }
                else if (cPressed == 57)
                {
                    nCurReserve = DOWN_SELL_RESERVE;
                }

            }

            if (cPressed == 17) // ctrl
            {
                isCtrlPushed = true;
            }

        }

        public void ChartClickHandler(object sender, EventArgs e)
        {
            // Set focus to the chart when it is clicked
            historyChart.Focus();
        }


        public void DisplayCurInfos()
        {
            if (profitnLoss > 0)
                profitnLossLabel.ForeColor = Color.Red;
            else if (profitnLoss < 0)
                profitnLossLabel.ForeColor = Color.Blue;
            else
                profitnLossLabel.ForeColor = Color.Black;
            profitnLossLabel.Text = $"{profitnLoss}";
            curDepositLabel.Text = $"{nCurDeposit}";
            curPriceLabel.Text = $"{nCurPrice}";
            everagePriceLabel.Text = $"{nEveragePrice}";
            if (pnLPrice > 0)
                pnLPriceLabel.ForeColor = Color.Red;
            else if (pnLPrice < 0)
                pnLPriceLabel.ForeColor = Color.Blue;
            else
                pnLPriceLabel.ForeColor = Color.Black;
            pnLPriceLabel.Text = $"{pnLPrice}";

            if (pnLRatio > 0)
                pnLRatioLabel.ForeColor = Color.Red;
            else if (pnLRatio < 0)
                pnLRatioLabel.ForeColor = Color.Blue;
            else
                pnLRatioLabel.ForeColor = Color.Black;
            pnLRatioLabel.Text = $"{Math.Round(pnLRatio, 3)}";
            ownVolumeLabel.Text = $"{nOwnVolume}";
            possibleVolumeLabel.Text = $"{nPossibleVolume}";

            hitNumLabel.Text = $"{nCurHitNum}";
            hitTypeLabel.Text = $"{nCurHitType}";

            powerLabel.Text = $"{Math.Round(fCurPower, 3)}";
            speedLabel.Text = $"{nCurSpeed}";
            curTimeLabel.Text = $"현재시간 : {nCurTime}";
            highLabel.Text = $"{nCurHighPrice}";
            lowLabel.Text = $"{nCurLowPrice}";
            startLabel.Text = $"{nCurStartPrice}";
            lastLabel.Text = $"{nCurLastPrice}";

        }

        public void CalcCurrentCashFlow()
        {
            if (nOwnVolume > 0)
            {
                pnLRatio = (double)((nCurPrice - nEveragePrice) * nOwnVolume
                    - (nCurPrice * nOwnVolume * (STOCK_TAX + SELL_COMMISION))
                    - (nEveragePrice * nOwnVolume * BUY_COMMISION)) / (nEveragePrice * nOwnVolume);

                pnLPrice = (int)((nCurPrice - nEveragePrice) * nOwnVolume
                    - (nCurPrice * nOwnVolume * (STOCK_TAX + SELL_COMMISION))
                    - (nEveragePrice * nOwnVolume * BUY_COMMISION));
            }
        }

        public void ChartControlKeyUpHandler(object sender, KeyEventArgs e)
        {
            char cUp = (char)e.KeyValue;

            if (cUp == 189) // -
            {
                isArrowGrouping = true;
                DrawChartUntilIdx(true);
            }
            if (cUp == 187) // +
            {
                isArrowGrouping = false;
                DrawChartUntilIdx(true);
            }

            if (cUp == 192) // `
            {
                isHitView = !isHitView;
                DrawChartUntilIdx(true);
            }


            if (cUp == 'G')
            {
                isViewGapMa = !isViewGapMa;
                DrawChartUntilIdx(true);
            }
            if (cUp == 'H')
            {
                isViewStartMa = !isViewStartMa;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'M')
            {
                isMouseCursorView = !isMouseCursorView;
            }

            if(cUp == 'N')
            {
                RegisterGroupBox.Visible = !RegisterGroupBox.Visible;
            }

            if(cUp == 'B')
            {
                CallThreadRegisterBlockByCode();
            }

            if (cUp == 'Q') // q가 눌렸는데 실매수
            {
                isPaperBuyArrowVisible = !isPaperBuyArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'W') // 실매도
            {
                isPaperSellArrowVisible = !isPaperSellArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'E') // 페이크매수
            {
                isFakeBuyArrowVisible = !isFakeBuyArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'R') // 페이크저항
            {

                isFakeResistArrowVisible = !isFakeResistArrowVisible;
                DrawChartUntilIdx(true);

            }

            if (cUp == 'S') // 페이크보조
            {

                isFakeAssistantArrowVisible = !isFakeAssistantArrowVisible;
                DrawChartUntilIdx(true);

            }


            if (cUp == 'D') // 페이크 변동성
            {

                isFakeVolatilityArrowVisible = !isFakeVolatilityArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'F') // 페이크 다운
            {
                isFakeDownArrowVisible = !isFakeDownArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'Z')
            {
                isRealBuyArrowVisible = !isRealBuyArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'X')
            {
                isRealSellArrowVisible = !isRealSellArrowVisible;
                DrawChartUntilIdx(true);
            }

            if (cUp == 'A')
            {
                ReverseAllArrowVisible();
                DrawChartUntilIdx(true);
            }

            if (cUp == 17)
            {
                isCtrlPushed = false;
            }
            if (cUp == 49 || cUp == 50 || cUp == 57)
            {

                if (isCtrlPushed) // 취소
                {
                    if (cUp == 49)
                    {
                        reserveArr[UP_BUY_RESERVE].Clear();
                        reserveChosenLabel.Text = "이상매수 예약 취소";
                        priceViewLabel.Text = "";
                    }
                    if (cUp == 50)
                    {
                        reserveArr[DOWN_BUY_RESERVE].Clear();
                        reserveChosenLabel.Text = "이하매수 예약 취소";
                        priceViewLabel.Text = "";
                    }
                    if (cUp == 57)
                    {
                        reserveArr[DOWN_SELL_RESERVE].Clear();
                        reserveChosenLabel.Text = "이하매도 예약 취소";
                        priceViewLabel.Text = "";
                    }
                }
                else
                {
                    reserveChosenLabel.Text = "";
                    priceViewLabel.Text = "";
                }
                nCurReserve = INIT_RESERVE;
            }

        }
        public List<FakeReport> GetFakeReportsByMin(int nTime)
        {
            List<FakeReport> fakeReportList = new List<FakeReport>();

            foreach (var fakeReport in fakereports)
            {
                double d1 = fakeReport.nRqTime * 0.01;
                double roundedNumber = Math.Truncate(d1);
                double fakeResportTime = roundedNumber * 100;

                if (fakeResportTime == nTime)
                {
                    Console.WriteLine("리스트 추가 : " + nTime);
                    fakeReportList.Add(fakeReport);
                }

            }

            return fakeReportList;
        }

        public void ReverseAllArrowVisible()
        {
            isAllArrowVisible = !isAllArrowVisible;
            isBuyArrowVisible = isAllArrowVisible;
            isSellArrowVisible = isAllArrowVisible;
            isFakeBuyArrowVisible = isAllArrowVisible;
            isFakeResistArrowVisible = isAllArrowVisible;
            isFakeAssistantArrowVisible = isAllArrowVisible;
            isFakeVolatilityArrowVisible = isAllArrowVisible;
            isFakeDownArrowVisible = isAllArrowVisible;
            isPaperBuyArrowVisible = isAllArrowVisible;
            isRealBuyArrowVisible = isAllArrowVisible;
            isRealSellArrowVisible = isAllArrowVisible;
            isPaperSellArrowVisible = isAllArrowVisible;
        }

        public int GetIntegratedMarketGap(int price)
        {
            int gap;
            if (price < 2000)
                gap = 1;
            else if (price < 5000)
                gap = 5;
            else if (price < 20000)
                gap = 10;
            else if (price < 50000)
                gap = 50;
            else if (price < 200000)
                gap = 100;
            else if (price < 500000)
                gap = 500;
            else
                gap = 1000;
            return gap;
        }

        private void shootBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSuccess = dataManager.InsertRemarkableData(searchCompLoc, searchCode, searchDateTime,
                    int.Parse(nRegisterNumberTxtBox.Text), sRegisterMemoTxtBox.Text);

                if (isSuccess)
                    MessageBox.Show("DB 삽입 성공");
                else
                    MessageBox.Show("DB 삽입 실패");
            }
            catch { }                
        }

        public int YIntervalGap(int yValueRange)
        {
            int yIntervalGap = -1;
            int closesetGap = -1;
            int closesetDiff = int.MaxValue;

            // 원하는 눈금 개수 범위 설정
            int intervalCntMin = 10;
            int intervalCntMax = 15;

            // 가격 간격 값 후보
            int[] possibleIntervals = new int[] { 1000, 500, 250, 100, 50, 20, 10 };

            // 간격 값 설정
            foreach (int interval in possibleIntervals)
            {
                int intervalCnt = (int)Math.Ceiling((double)yValueRange / interval) + 1;
                if (intervalCnt >= intervalCntMin && intervalCnt <= intervalCntMax)
                {
                    yIntervalGap = interval;
                    break;
                }

                // 원하는 범위에 없는 경우, 가장 조건에 가까운 값
                int diff = Math.Abs(intervalCnt - intervalCntMin);
                if (diff < closesetDiff)
                {
                    closesetDiff = diff;
                    closesetGap = interval;
                }
            }

            if (yIntervalGap == -1)
            {
                yIntervalGap = closesetGap;
            }

            return yIntervalGap;
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dTradeTimeDateTimePicker.Value;
            sCodeTextBox.Text = sCodeNameTxtBox.Text;

            dateTimePicker_action(sender);
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            if(nViewIdx < nViewPass - 1)
            {
                nViewIdx++;
                sCodeNameTxtBox.Text = viewList[nViewIdx].sCodeName;
                dTradeTimeDateTimePicker.Value = viewList[nViewIdx].dTradeTime;
            }
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            if (nViewIdx > 0 )
            {
                nViewIdx--;
                sCodeNameTxtBox.Text = viewList[nViewIdx].sCodeName;
                dTradeTimeDateTimePicker.Value = viewList[nViewIdx].dTradeTime;
            }
        }

        public static bool IsFirstCharDigit(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; // 빈 문자열이나 null은 숫자도 문자도 아닙니다.
            }

            char firstChar = input[0];
            return char.IsDigit(firstChar);
        }

        public void CallThreadRegisterBlockByCode()
        {
            try
            {
                new Thread(() => new RegisterCodeForm(this).ShowDialog()).Start();
            }
            catch { }
        }

    }
}
