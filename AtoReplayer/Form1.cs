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
- y축 값이 다 안보임 (테스트를 위해 sleep 주석 처리해놓음)
- 뒤로 가기, 앞으로 가기 (-5분, -1분, +1 분, +5분)
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
        TimeLine[] displayedTimelines = new TimeLine[0];
        int delayInterval = 1000;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token;
        int currentDrawIdx = 0;

        public Form1()
        {
            InitializeComponent();

            initializeDateTimePicker();
            initializeChartDesign();

            dataManager = new DataManager();

            dateTimePicker1.CloseUp += dateTimePicker1_ValueChanged;
            dateTimePicker1.KeyDown += dateTimePicker1_KeyDown;
            compLocButton.Text = searchCompLoc.ToString();

            //처음 공백 Placeholder 지정
            sCodeTextBox.ForeColor = Color.DarkGray;
            sCodeTextBox.Text = "종목명/종목코드";
            //텍스트박스 커서 Focus 여부에 따라 이벤트 지정
            sCodeTextBox.GotFocus += RemovePlaceholder;
            sCodeTextBox.Leave += SetPlaceholder;
            sCodeTextBox.Leave += sCodeTextBox_Leave;
            sCodeTextBox.KeyPress += sCodeTextBox_KeyPress;
            playButton.Click += playButton_Click;
            compLocButton.Click += compLocButton_ClickAsync;

            // 테스트용 기본값
            searchCode = "동화약품";
            sCodeTextBox.Text = "동화약품";
            dateTimePicker1.Value = Convert.ToDateTime("2023-06-16");
            searchDate = "2023-06-16";

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
                    DrawChartAsync();
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                searchCode = sCodeTextBox.Text;
                await InitDrawAsync();
                DrawChartAsync();
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
                DrawChartAsync();
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
            DrawChartAsync();
        }

        public void ClearChart()
        {
            historyChart.Series[0].Points.Clear();
        }

        private async Task InitDrawAsync()
        {
            // -------------- 차트 초기화 -----------------
            ClearChart();

            // -------------- 데이터 조회 -----------------
            // 조회 중 메시지 박스 표시
            loadingPanel.Visible = true;
            DisableControls(this);
            String codeType = IsFirstCharDigit(searchCode) ? "CODE" : "NAME";
            timelines = await Task.Run(() => dataManager.getTimeLines(codeType, searchCode, searchDate, searchCompLoc).ToArray());
            loadingPanel.Visible = false;
            EnableControls(this);

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

        private void createTokenSource()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            Console.WriteLine("Create CancollationToken");
        }

        public async Task DrawChartAsync()
        {
            Console.WriteLine("Call DrawChartAsync()");
            Console.WriteLine("timelines: " + timelines.Length);

            if (timelines.Length == 0)
            {
                ClearChart();
                MessageBox.Show("No Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // -------------- 차트 데이터 --------------
                // 분봉 그리기
                Console.WriteLine("분봉그리기 시작");
                createTokenSource();
                for (int i = currentDrawIdx; i < timelines.Length; i++)
                {
                    Console.WriteLine("currentDrawIdx: " + currentDrawIdx);
                    if (isPlaying & token.IsCancellationRequested == false)
                    {
                        displayedTimelines = new TimeLine[currentDrawIdx + 1];
                        Array.Copy(timelines, displayedTimelines, currentDrawIdx + 1);

                        // -------------- 차트 조정 ------------------
                        int maxY = displayedTimelines.Max(timeline => timeline.nMaxFs);
                        int minY = displayedTimelines.Min(timeline => timeline.nMaxFs);

                        // 차트 스케일 조정
                        double padding = GetIntegratedMarketGap(displayedTimelines[0].nStartFs); // 여백
                        historyChart.ChartAreas[0].AxisY.Maximum = maxY + padding;
                        historyChart.ChartAreas[0].AxisY.Minimum = minY - padding;

                        // Y축의 눈금 조정
                        historyChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                        historyChart.ChartAreas[0].AxisY.Interval = YIntervalGap(maxY - minY); // Y축 간격


                        Console.WriteLine($"sDate: {displayedTimelines[i].sDate}, sCodeName: {displayedTimelines[i].sCodeName}, nIdx: {displayedTimelines[i].nIdx}");

                        historyChart.Series["MinuteStick"].Points.AddXY(displayedTimelines[i].nTime.ToString(), displayedTimelines[i].nMaxFs);
                        historyChart.Series["MinuteStick"].Points[i].YValues[1] = displayedTimelines[i].nMinFs;
                        historyChart.Series["MinuteStick"].Points[i].YValues[2] = displayedTimelines[i].nStartFs;
                        historyChart.Series["MinuteStick"].Points[i].YValues[3] = displayedTimelines[i].nLastFs;

                        currentDrawIdx = i+1;
                        //await Task.Delay(delayInterval);
                    }
                }
            }            
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
            // 원하는 눈금 개수 범위 설정
            int intervalCntMin = 10;
            int intervalCntMax = 15;

            // 가능한 간격 값들
            int[] possibleIntervals = new int[] { 1000, 500, 250, 100, 50, 20, 10 };

            // 간격 값 설정
            int selectedInterval = possibleIntervals[0];
            foreach (int interval in possibleIntervals)
            {
                int intervalCnt = (int)Math.Ceiling((double)yValueRange / interval) + 1;
                if (intervalCnt >= intervalCntMin && intervalCnt <= intervalCntMax)
                {
                    selectedInterval = interval;
                    break;
                }
            }

            return selectedInterval;
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
