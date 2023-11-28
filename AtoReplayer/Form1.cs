using AtoReplayer.Controller;
using AtoReplayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class Form1 : Form
    {
        private DataManager dataManager;
        private const string DATEFORMAT = "yyyy-MM-dd";

        private string searchCode = ""; // 주식 코드 or 주식명
        private string searchDate = DateTime.Today.ToString(DATEFORMAT);
        private int searchCompLoc = 0;

        private bool isPlaying = true;
        TimeLine[] timelines = new TimeLine[0];
        FakeReport[] fakereports = new FakeReport[0];
        TimeLine[] displayedTimelines = new TimeLine[0];
        int delayInterval = 1000;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token;
        int currentDrawIdx = 0;

        StrategyNames strategyNames = new StrategyNames();
        public bool isArrowGrouping = true;
        public string NEW_LINE = Environment.NewLine;

        public bool isViewGapMa = true;


        public bool isBuyArrowVisible = true;
        public bool isSellArrowVisible = true;
        public bool isFakeBuyArrowVisible = true;
        public bool isFakeResistArrowVisible = true;
        public bool isFakeAssistantArrowVisible = true;
        public bool isFakeVolatilityArrowVisible = true;
        public bool isFakeDownArrowVisible = true;
        public bool isPaperBuyArrowVisible = true;
        public bool isPaperSellArrowVisible = true;

        public bool isAllArrowVisible = true;

        public Form1()
        {
            InitializeComponent();

            initializeDateTimePicker();
            initializeChartDesign();

            dataManager = new DataManager();

            compLocButton.Text = searchCompLoc.ToString();

            this.KeyPreview = true;
            this.KeyDown += KeyDownHandler;


            //처음 공백 Placeholder 지정
            sCodeTextBox.ForeColor = Color.DarkGray;
            sCodeTextBox.Text = "종목명/종목코드";

            historyChart.MouseMove += ChartMouseMoveHandler;

            // 컴포넌트 이벤트 핸들러
            this.KeyUp += KeyUpHandler;
            dateTimePicker1.CloseUp += dateTimePicker1_ValueChanged;
            dateTimePicker1.KeyDown += dateTimePicker1_KeyDown;
            sCodeTextBox.GotFocus += RemovePlaceholder;
            sCodeTextBox.Leave += SetPlaceholder;
            sCodeTextBox.Leave += sCodeTextBox_Leave;
            sCodeTextBox.KeyPress += sCodeTextBox_KeyPress;
            playButton.Click += playButton_Click;
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

            // 테스트용 기본값
            // searchCode = "동화약품";
            // sCodeTextBox.Text = "동화약품";
            // dateTimePicker1.Value = Convert.ToDateTime("2023-11-10");
            // searchDate = "2023-06-16";
        }

        public int prevXMove = 0, prevYMove = 0;
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
                curLocPowerLabel.Text = $"커서파워 : {Math.Round((double)(yCoord - nFirstPrice) / nFirstPrice, 3)}";
            }

        }
        private void initializeChartDesign()
        {
            // Y축의 라벨 표시 형식 설정
            historyChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,0"; // 천 단위마다 구분

            // 차트 색상
            historyChart.Series["MinuteStick"].SetCustomProperty("PriceUpColor", "Red");
            historyChart.Series["MinuteStick"].SetCustomProperty("PriceDownColor", "Blue");

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

        private async Task dateTimePicker_action(object sender)
        {
            DateTime dt = dateTimePicker1.Value;
            String sdt = dt.ToString(DATEFORMAT);
            if (searchDate != sdt)
            {
                searchDate = sdt;
                if (searchCode != "")
                {
                    await InitDrawAsync();
                    DrawChartPerSecondAsync();
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
            sCodeTextBox.ForeColor = Color.Black;
            if (e.KeyChar == (char)Keys.Enter)
            {
                searchCode = sCodeTextBox.Text;
                await InitDrawAsync();
                DrawChartPerSecondAsync();
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            isPlaying = !isPlaying;

            if (isPlaying)
            {
                Console.WriteLine("재생");
                ((Button)sender).Text = "❚❚";
                ((Button)sender).Font = new Font(((Button)sender).Font.FontFamily, 14);
                DrawChartPerSecondAsync();
            }
            else
            {
                Console.WriteLine("일시정지");
                ((Button)sender).Text = "▶";
                ((Button)sender).Font = new Font(((Button)sender).Font.FontFamily, 11);
                tokenSource.Cancel();
            }
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
            await InitDrawAsync();
            DrawChartPerSecondAsync();
        }

        private void back10_Click(object sender, EventArgs e)
        {
            currentDrawIdx -= 10;
            if (currentDrawIdx < 0)
            {
                currentDrawIdx = 0;
            }

            DrawChartUntilIdx();
        }
        private void back5_Click(object sender, EventArgs e)
        {
            currentDrawIdx -= 5;
            if (currentDrawIdx < 0)
            {
                currentDrawIdx = 0;
            }

            DrawChartUntilIdx();
        }
        private void back60_Click(object sender, EventArgs e)
        {
            currentDrawIdx -= 60;
            if (currentDrawIdx < 0)
            {
                currentDrawIdx = 0;
            }

            DrawChartUntilIdx();
        }

        private void forward5_Click(object sender, EventArgs e)
        {
            currentDrawIdx += 5;
            if (currentDrawIdx >= timelines.Length)
            {
                currentDrawIdx = timelines.Length - 1;
            }

            DrawChartUntilIdx();
        }

        private void forward10_Click(object sender, EventArgs e)
        {
            currentDrawIdx += 10;
            if (currentDrawIdx >= timelines.Length)
            {
                currentDrawIdx = timelines.Length - 1;
            }

            DrawChartUntilIdx();
        }

        private void forward60_Click(object sender, EventArgs e)
        {
            currentDrawIdx += 60;
            if (currentDrawIdx >= timelines.Length)
            {
                currentDrawIdx = timelines.Length - 1;
            }

            DrawChartUntilIdx();
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
                delayInterval = (int)(double.Parse(nDelaySecondTextBox.Text) * 1000);
            }
        }

        public void ClearChart()
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

        private async Task InitDrawAsync()
        {
            Boolean oldPlayingFlag = isPlaying;
            isPlaying = false;

            // -------------- 차트 초기화 -----------------
            ClearChart();

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

            isViewGapMa = true;
            isBuyArrowVisible = true;
            isSellArrowVisible = true;
            isFakeBuyArrowVisible = true;
            isFakeResistArrowVisible = true;
            isFakeAssistantArrowVisible = true;
            isFakeVolatilityArrowVisible = true;
            isFakeDownArrowVisible = true;
            isPaperBuyArrowVisible = true;
            isPaperSellArrowVisible = true;
            isAllArrowVisible = true;

            loadingPanel.Visible = false;
            EnableControls(this);

            try
            {
                nFirstPrice = timelines[0].fOverMa0Gap;
            }
            catch
            {

            }

            currentDrawIdx = 0;
            isPlaying = oldPlayingFlag;
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

        private void createTokenSource()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            Console.WriteLine("Create CancollationToken");
        }

        public async Task DrawChartPerSecondAsync()
        {
            Console.WriteLine("Call DrawChartPerSecondAsync()");
            Console.WriteLine("timelines: " + timelines.Length);

            if (timelines.Length == 0)
            {
                ClearChart();
                MessageBox.Show("No Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Console.WriteLine("분봉그리기 시작");
                createTokenSource();

                while (isPlaying & token.IsCancellationRequested == false)
                {
                    Console.WriteLine("currentDrawIdx: " + currentDrawIdx);
                    Console.WriteLine("currentDrawTime : " + timelines[currentDrawIdx].nTime);
                    currentDrawIdx += 1;
                    //nFirstPrice = 0;
                    DrawChartUntilIdx();


                    if (currentDrawIdx > timelines.Length)
                    {
                        Console.WriteLine("끝");

                        object senderObject = playButton; // yourButton은 playButton의 이름 또는 인스턴스
                        EventArgs eventArgs = EventArgs.Empty; // 또는 적절한 이벤트 인수

                        // 메소드 호출
                        playButton_Click(senderObject, eventArgs);
                        return;
                    }
                    await Task.Delay(delayInterval);
                }
            }
        }

        public double nFirstPrice;
        public void DrawChartUntilIdx()
        {
            if (currentDrawIdx >= timelines.Length)
            {
                return;
            }

            ClearChart();

            int nArrowCnt = 0;
            int nFakeBuyArrowTotCnt = 0;
            int nFakeResistArrowTotCnt = 0;
            int nFakeAssistantArrowTotCnt = 0;
            int nFakeVolatilityArrowTotCnt = 0;
            int nFakeDownArrowTotCnt = 0;
            int nPaperBuyArrowTotCnt = 0;
            int nPaperSellArrowTotCnt = 0;



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

                double padding = GetIntegratedMarketGap(displayedTimelines[0].nStartFs); // 여백
                historyChart.ChartAreas[0].AxisY.Maximum = maxY + padding;
                historyChart.ChartAreas[0].AxisY.Minimum = minY - padding;

                // Y축의 눈금 조정
                historyChart.ChartAreas[0].AxisY.Interval = YIntervalGap(maxY - minY); // Y축 간격


                // 그래프 그리기
                // Console.WriteLine($"sDate: {displayedTimelines[i].sDate}, sCodeName: {displayedTimelines[i].sCodeName}, nIdx: {displayedTimelines[i].nIdx}");
                historyChart.Series["MinuteStick"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].nMaxFs);
                historyChart.Series["MinuteStick"].Points[i].YValues[1] = displayedTimelines[i].nMinFs;
                historyChart.Series["MinuteStick"].Points[i].YValues[2] = displayedTimelines[i].nStartFs;
                historyChart.Series["MinuteStick"].Points[i].YValues[3] = displayedTimelines[i].nLastFs;
                historyChart.Series["MinuteStick"].ToolTip = "";

                // 이평선 그리기

                historyChart.Series["Ma20m"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa0);
                historyChart.Series["Ma1h"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa1);
                historyChart.Series["Ma2h"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa2);

                if (isViewGapMa)
                {
                    historyChart.Series["Ma20mGap"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa0Gap);
                    historyChart.Series["Ma1hGap"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa1Gap);
                    historyChart.Series["Ma2hGap"].Points.AddXY(displayTime.ToString(), displayedTimelines[i].fOverMa2Gap);
                }

                // 화살표 그리기
                Console.WriteLine("displayTime: " + displayTime);
                List<FakeReport> fakereportList = GetFakeReportsByMin(displayTime);
                for (int j = 0; j < fakereportList.Count; j++)
                {
                    FakeReport fakereport = fakereportList[j];
                    ArrowAnnotation arrowAnnotation = new ArrowAnnotation();
                    nArrowCnt += 1;

                    int nFakeBuyArrowCnt = 0;
                    int nFakeResistArrowCnt = 0;
                    int nFakeAssistantArrowCnt = 0;
                    int nFakeVolatilityArrowCnt = 0;
                    int nFakeDownArrowCnt = 0;
                    int nPaperBuyArrowCnt = 0;
                    int nPaperSellArrowCnt = 0;
                    string sFakeBuyArrowToolTip = "";
                    string sFakeResistArrowToolTip = "";
                    string sFakeAssistantArrowToolTip = "";
                    string sFakeVolatilityArrowToolTip = "";
                    string sFakeDownArrowToolTip = "";
                    string sPaperBuyArrowToolTip = "";
                    string sPaperSellArrowToolTip = "";

                    switch (fakereport.nBuyStrategyGroupNum)
                    {
                        case ArrowColors.FAKE_BUY_SIGNAL:
                            if (isFakeBuyArrowVisible)
                            {
                                nFakeBuyArrowTotCnt += 1;
                                nFakeBuyArrowCnt += 1;

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
                                        if (historyChart.Annotations[k].Name.Equals("FB" + i + "." + j + "." + (nFakeBuyArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                        {
                                            historyChart.Annotations.RemoveAt(k);
                                        }
                                    }
                                    arrowAnnotation.Height = -7;
                                }

                                sFakeBuyArrowToolTip +=
                                    $"*중첩 : {nFakeBuyArrowCnt}( {nFakeBuyArrowTotCnt} )  가짜전략 : {fakereport.nBuyStrategyIdx}  주문시간 : {fakereport.nRqTime}  페이크매수가격 : {fakereport.nOverPrice}(원){NEW_LINE}" +
                                  //$"페이크명 : {strategyNames.arrFakeBuyStrategyName[fakereport.nBuyStrategyIdx]}{NEW_LINE}{NEW_LINE}";
                                  $"{NEW_LINE}{NEW_LINE}";
                                arrowAnnotation.ToolTip = $"*페이크매수 총 갯수 : {nFakeBuyArrowCnt}개\n" +
                                       $"=====================================================\n" + sFakeBuyArrowToolTip;

                                arrowAnnotation.BackColor = Color.Orange;
                                arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                arrowAnnotation.Name = "FB" + i + "." + j + "." + nFakeBuyArrowCnt;
                                arrowAnnotation.LineColor = Color.Black;

                                historyChart.Annotations.Add(arrowAnnotation);

                            }
                            break;
                        case ArrowColors.FAKE_RESIST_SIGNAL:
                            if (isFakeResistArrowVisible)
                            {
                                nFakeResistArrowTotCnt += 1;
                                nFakeResistArrowCnt += 1;

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
                                        if (historyChart.Annotations[k].Name.Equals("FR" + i + "." + j + "." + (nFakeResistArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                        {
                                            historyChart.Annotations.RemoveAt(k);
                                        }
                                    }
                                    arrowAnnotation.Height = -7;
                                }
                                arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                arrowAnnotation.Name = "FR" + i + "." + j + "." + nFakeResistArrowCnt;
                                arrowAnnotation.BackColor = Color.Green;
                                arrowAnnotation.LineColor = Color.Black;

                                historyChart.Annotations.Add(arrowAnnotation);
                            }
                            break;
                        case ArrowColors.FAKE_ASSISTANT_SIGNAL:
                            if (isFakeAssistantArrowVisible)
                            {
                                nFakeAssistantArrowTotCnt += 1;
                                nFakeAssistantArrowCnt += 1;

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
                                        if (historyChart.Annotations[k].Name.Equals("FA" + i + "." + j + "." + (nFakeAssistantArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                        {
                                            historyChart.Annotations.RemoveAt(k);
                                        }
                                    }
                                    arrowAnnotation.Height = -7;
                                }
                                arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                arrowAnnotation.Name = "FA" + i + "." + j + "." + nFakeAssistantArrowCnt;
                                arrowAnnotation.BackColor = Color.Yellow;
                                arrowAnnotation.LineColor = Color.Black;

                                historyChart.Annotations.Add(arrowAnnotation);
                            }
                            break;

                        case ArrowColors.FAKE_VOLATILE_SIGNAL:
                            if (isFakeVolatilityArrowVisible)
                            {
                                nFakeVolatilityArrowTotCnt += 1;
                                nFakeVolatilityArrowCnt += 1;

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
                                        if (historyChart.Annotations[k].Name.Equals("FV" + i + "." + j + "." + (nFakeVolatilityArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                        {
                                            historyChart.Annotations.RemoveAt(k);
                                        }
                                    }
                                    arrowAnnotation.Height = -7;
                                }
                                arrowAnnotation.BackColor = Color.Navy;
                                arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);

                                arrowAnnotation.Name = "FV" + i + "." + j + "." + nFakeVolatilityArrowCnt;
                                arrowAnnotation.LineColor = Color.Black;

                                historyChart.Annotations.Add(arrowAnnotation);
                            }
                            break;
                        case ArrowColors.FAKE_DOWN_SIGNAL:
                            if (isFakeDownArrowVisible)
                            {
                                nFakeDownArrowTotCnt += 1;
                                nFakeDownArrowCnt += 1;

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
                                        if (historyChart.Annotations[k].Name.Equals("FD" + i + "." + j + "." + (nFakeDownArrowCnt - 1)))  // F + 해당분봉의 최근삽입정보
                                        {
                                            historyChart.Annotations.RemoveAt(k);
                                        }
                                    }
                                    arrowAnnotation.Height = -7;
                                }
                                arrowAnnotation.BackColor = Color.Purple;
                                arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);

                                arrowAnnotation.Name = "FD" + i + "." + j + "." + nFakeDownArrowCnt;
                                arrowAnnotation.LineColor = Color.Black;

                                historyChart.Annotations.Add(arrowAnnotation);
                            }
                            break;

                        case ArrowColors.PAPER_BUY_SIGNAL:
                            if (isPaperBuyArrowVisible)
                            {
                                nPaperBuyArrowTotCnt += 1;
                                nPaperBuyArrowCnt += 1;

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
                                        if (historyChart.Annotations[k].Name.Equals("P" + i + "." + j + "." + (nPaperBuyArrowCnt - 1)))  // P + 해당분봉의 최근삽입정보
                                        {
                                            historyChart.Annotations.RemoveAt(k);
                                        }
                                    }
                                    arrowAnnotation.Height = -7;
                                }

                                arrowAnnotation.BackColor = Color.Red;
                                arrowAnnotation.SetAnchor(historyChart.Series["MinuteStick"].Points[i]);
                                // arrowFakeBuy.AnchorY = historyChart.Series["MinuteStick"].Points[curEa.fakeBuyStrategy.arrMinuteIdx[p]].YValues[1]; // 고.저.시종
                                arrowAnnotation.Name = "P" + i + "." + j + "." + nPaperBuyArrowCnt;
                                arrowAnnotation.LineColor = Color.Black;

                                historyChart.Annotations.Add(arrowAnnotation);
                            }
                            break;

                    }

                }
            }
        }

        public void KeyDownHandler(object sender, KeyEventArgs e)
        {
            char cDown = (char)e.KeyValue;

        }

        public void KeyUpHandler(object sender, KeyEventArgs e)
        {
            char cUp = (char)e.KeyValue;

            if (cUp == 189) // -
            {
                isArrowGrouping = true;
                DrawChartUntilIdx();
            }
            if (cUp == 187) // +
            {
                isArrowGrouping = false;
                DrawChartUntilIdx();
            }

            if (cUp == 'G')
            {
                isViewGapMa = !isViewGapMa;
                DrawChartUntilIdx();
            }

            if (cUp == 'Q') // q가 눌렸는데 실매수
            {
                isPaperBuyArrowVisible = !isPaperBuyArrowVisible;
                DrawChartUntilIdx();
            }

            if (cUp == 'W') // 실매도
            {
                isPaperSellArrowVisible = !isPaperSellArrowVisible;
                DrawChartUntilIdx();
            }

            if (cUp == 'E') // 페이크매수
            {
                isFakeBuyArrowVisible = !isFakeBuyArrowVisible;
                DrawChartUntilIdx();
            }

            if (cUp == 'R') // 페이크저항
            {

                isFakeResistArrowVisible = !isFakeResistArrowVisible;
                DrawChartUntilIdx();

            }

            if (cUp == 'S') // 페이크보조
            {

                isFakeAssistantArrowVisible = !isFakeAssistantArrowVisible;
                DrawChartUntilIdx();

            }


            if (cUp == 'D') // 페이크 변동성
            {

                isFakeVolatilityArrowVisible = !isFakeVolatilityArrowVisible;
                DrawChartUntilIdx();
            }

            if (cUp == 'F') // 페이크 다운
            {
                isFakeDownArrowVisible = !isFakeDownArrowVisible;
                DrawChartUntilIdx();
            }

            if (cUp == 'A')
            {
                ReverseAllArrowVisible();
                DrawChartUntilIdx();
            }
        }
        public List<FakeReport> GetFakeReportsByMin(int nTime)
        {
            List<FakeReport> fakeReportList = new List<FakeReport>();

            foreach (var fakeReport in fakereports)
            {
                double d1 = fakeReport.nRqTime * 0.01;
                double roundedNumber = Math.Ceiling(d1);
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

        public static bool IsFirstCharDigit(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; // 빈 문자열이나 null은 숫자도 문자도 아닙니다.
            }

            char firstChar = input[0];
            return char.IsDigit(firstChar);
        }

    }
}
