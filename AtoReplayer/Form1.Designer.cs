
namespace AtoReplayer
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series29 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series30 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series31 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series32 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series33 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series34 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series35 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.historyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.sCodeTextBox = new System.Windows.Forms.TextBox();
            this.playButton = new System.Windows.Forms.Button();
            this.compLocButton = new System.Windows.Forms.Button();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.back5 = new System.Windows.Forms.Button();
            this.back10 = new System.Windows.Forms.Button();
            this.forward5 = new System.Windows.Forms.Button();
            this.forward10 = new System.Windows.Forms.Button();
            this.back60 = new System.Windows.Forms.Button();
            this.forward60 = new System.Windows.Forms.Button();
            this.nDelaySecondTextBox = new System.Windows.Forms.TextBox();
            this.curLocLabel = new System.Windows.Forms.Label();
            this.curLocPowerLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sellButton = new System.Windows.Forms.Button();
            this.buyButton = new System.Windows.Forms.Button();
            this.sellVolumeTxtBox = new System.Windows.Forms.TextBox();
            this.sellPriceTxtBox = new System.Windows.Forms.TextBox();
            this.buyVolumeTxtBox = new System.Windows.Forms.TextBox();
            this.buyPriceTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.unTradedListView = new System.Windows.Forms.ListView();
            this.curTimeLabel = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lastLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.lowLabel = new System.Windows.Forms.Label();
            this.highLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).BeginInit();
            this.loadingPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // historyChart
            // 
            this.historyChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.CursorX.IsUserSelectionEnabled = true;
            chartArea5.Name = "ChartArea1";
            this.historyChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.historyChart.Legends.Add(legend5);
            this.historyChart.Location = new System.Drawing.Point(30, 86);
            this.historyChart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.historyChart.Name = "historyChart";
            series29.ChartArea = "ChartArea1";
            series29.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series29.Legend = "Legend1";
            series29.Name = "MinuteStick";
            series29.YValuesPerPoint = 4;
            series30.ChartArea = "ChartArea1";
            series30.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series30.Color = System.Drawing.Color.Red;
            series30.Legend = "Legend1";
            series30.Name = "Ma20m";
            series31.ChartArea = "ChartArea1";
            series31.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series31.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series31.Legend = "Legend1";
            series31.Name = "Ma1h";
            series32.ChartArea = "ChartArea1";
            series32.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series32.Color = System.Drawing.Color.Yellow;
            series32.Legend = "Legend1";
            series32.Name = "Ma2h";
            series33.ChartArea = "ChartArea1";
            series33.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series33.Color = System.Drawing.Color.Green;
            series33.Legend = "Legend1";
            series33.Name = "Ma20mGap";
            series34.ChartArea = "ChartArea1";
            series34.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series34.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series34.Legend = "Legend1";
            series34.Name = "Ma1hGap";
            series35.ChartArea = "ChartArea1";
            series35.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series35.Color = System.Drawing.Color.Purple;
            series35.Legend = "Legend1";
            series35.Name = "Ma2hGap";
            this.historyChart.Series.Add(series29);
            this.historyChart.Series.Add(series30);
            this.historyChart.Series.Add(series31);
            this.historyChart.Series.Add(series32);
            this.historyChart.Series.Add(series33);
            this.historyChart.Series.Add(series34);
            this.historyChart.Series.Add(series35);
            this.historyChart.Size = new System.Drawing.Size(1142, 744);
            this.historyChart.TabIndex = 0;
            this.historyChart.Text = "chart1";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(195, 26);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(127, 25);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // sCodeTextBox
            // 
            this.sCodeTextBox.Location = new System.Drawing.Point(77, 26);
            this.sCodeTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sCodeTextBox.Name = "sCodeTextBox";
            this.sCodeTextBox.Size = new System.Drawing.Size(114, 25);
            this.sCodeTextBox.TabIndex = 2;
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("굴림", 14F);
            this.playButton.Location = new System.Drawing.Point(631, 15);
            this.playButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.playButton.Name = "playButton";
            this.playButton.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.playButton.Size = new System.Drawing.Size(37, 42);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "❚❚";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // compLocButton
            // 
            this.compLocButton.Location = new System.Drawing.Point(30, 26);
            this.compLocButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.compLocButton.Name = "compLocButton";
            this.compLocButton.Size = new System.Drawing.Size(24, 26);
            this.compLocButton.TabIndex = 4;
            this.compLocButton.Text = "1";
            this.compLocButton.UseVisualStyleBackColor = true;
            // 
            // loadingPanel
            // 
            this.loadingPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.loadingPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loadingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loadingPanel.Controls.Add(this.label1);
            this.loadingPanel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.loadingPanel.Location = new System.Drawing.Point(206, 270);
            this.loadingPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(695, 348);
            this.loadingPanel.TabIndex = 5;
            this.loadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(275, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading......";
            // 
            // back5
            // 
            this.back5.Location = new System.Drawing.Point(584, 15);
            this.back5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back5.Name = "back5";
            this.back5.Size = new System.Drawing.Size(37, 42);
            this.back5.TabIndex = 6;
            this.back5.Text = "-5";
            this.back5.UseVisualStyleBackColor = true;
            // 
            // back10
            // 
            this.back10.Location = new System.Drawing.Point(541, 15);
            this.back10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back10.Name = "back10";
            this.back10.Size = new System.Drawing.Size(37, 42);
            this.back10.TabIndex = 7;
            this.back10.Text = "-10";
            this.back10.UseVisualStyleBackColor = true;
            // 
            // forward5
            // 
            this.forward5.Location = new System.Drawing.Point(677, 15);
            this.forward5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.forward5.Name = "forward5";
            this.forward5.Size = new System.Drawing.Size(37, 42);
            this.forward5.TabIndex = 8;
            this.forward5.Text = "+5";
            this.forward5.UseVisualStyleBackColor = true;
            // 
            // forward10
            // 
            this.forward10.Location = new System.Drawing.Point(721, 15);
            this.forward10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.forward10.Name = "forward10";
            this.forward10.Size = new System.Drawing.Size(37, 42);
            this.forward10.TabIndex = 9;
            this.forward10.Text = "+10";
            this.forward10.UseVisualStyleBackColor = true;
            // 
            // back60
            // 
            this.back60.Location = new System.Drawing.Point(497, 15);
            this.back60.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back60.Name = "back60";
            this.back60.Size = new System.Drawing.Size(37, 42);
            this.back60.TabIndex = 10;
            this.back60.Text = "-60";
            this.back60.UseVisualStyleBackColor = true;
            // 
            // forward60
            // 
            this.forward60.Location = new System.Drawing.Point(765, 15);
            this.forward60.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.forward60.Name = "forward60";
            this.forward60.Size = new System.Drawing.Size(37, 42);
            this.forward60.TabIndex = 11;
            this.forward60.Text = "+60";
            this.forward60.UseVisualStyleBackColor = true;
            // 
            // nDelaySecondTextBox
            // 
            this.nDelaySecondTextBox.Location = new System.Drawing.Point(456, 25);
            this.nDelaySecondTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nDelaySecondTextBox.Name = "nDelaySecondTextBox";
            this.nDelaySecondTextBox.Size = new System.Drawing.Size(34, 25);
            this.nDelaySecondTextBox.TabIndex = 12;
            this.nDelaySecondTextBox.Text = "1";
            // 
            // curLocLabel
            // 
            this.curLocLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.curLocLabel.AutoSize = true;
            this.curLocLabel.Location = new System.Drawing.Point(1178, 270);
            this.curLocLabel.Name = "curLocLabel";
            this.curLocLabel.Size = new System.Drawing.Size(67, 15);
            this.curLocLabel.TabIndex = 13;
            this.curLocLabel.Text = "현재좌표";
            // 
            // curLocPowerLabel
            // 
            this.curLocPowerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.curLocPowerLabel.AutoSize = true;
            this.curLocPowerLabel.Location = new System.Drawing.Point(1178, 300);
            this.curLocPowerLabel.Name = "curLocPowerLabel";
            this.curLocPowerLabel.Size = new System.Drawing.Size(67, 15);
            this.curLocPowerLabel.TabIndex = 14;
            this.curLocPowerLabel.Text = "커서파워";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.sellButton);
            this.groupBox1.Controls.Add(this.buyButton);
            this.groupBox1.Controls.Add(this.sellVolumeTxtBox);
            this.groupBox1.Controls.Add(this.sellPriceTxtBox);
            this.groupBox1.Controls.Add(this.buyVolumeTxtBox);
            this.groupBox1.Controls.Add(this.buyPriceTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(1181, 787);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 183);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "매매현황";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "매도량";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "매도가";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "매수량";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "매수가";
            // 
            // sellButton
            // 
            this.sellButton.Location = new System.Drawing.Point(192, 121);
            this.sellButton.Name = "sellButton";
            this.sellButton.Size = new System.Drawing.Size(82, 37);
            this.sellButton.TabIndex = 5;
            this.sellButton.Text = "매도";
            this.sellButton.UseVisualStyleBackColor = true;
            // 
            // buyButton
            // 
            this.buyButton.Location = new System.Drawing.Point(192, 28);
            this.buyButton.Name = "buyButton";
            this.buyButton.Size = new System.Drawing.Size(82, 36);
            this.buyButton.TabIndex = 4;
            this.buyButton.Text = "매수";
            this.buyButton.UseVisualStyleBackColor = true;
            // 
            // sellVolumeTxtBox
            // 
            this.sellVolumeTxtBox.Location = new System.Drawing.Point(65, 140);
            this.sellVolumeTxtBox.Name = "sellVolumeTxtBox";
            this.sellVolumeTxtBox.Size = new System.Drawing.Size(100, 25);
            this.sellVolumeTxtBox.TabIndex = 3;
            // 
            // sellPriceTxtBox
            // 
            this.sellPriceTxtBox.Location = new System.Drawing.Point(65, 109);
            this.sellPriceTxtBox.Name = "sellPriceTxtBox";
            this.sellPriceTxtBox.Size = new System.Drawing.Size(100, 25);
            this.sellPriceTxtBox.TabIndex = 2;
            // 
            // buyVolumeTxtBox
            // 
            this.buyVolumeTxtBox.Location = new System.Drawing.Point(65, 59);
            this.buyVolumeTxtBox.Name = "buyVolumeTxtBox";
            this.buyVolumeTxtBox.Size = new System.Drawing.Size(100, 25);
            this.buyVolumeTxtBox.TabIndex = 1;
            // 
            // buyPriceTxtBox
            // 
            this.buyPriceTxtBox.Location = new System.Drawing.Point(65, 24);
            this.buyPriceTxtBox.Name = "buyPriceTxtBox";
            this.buyPriceTxtBox.Size = new System.Drawing.Size(100, 25);
            this.buyPriceTxtBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(1181, 328);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 225);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "계좌현황";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(171, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 15);
            this.label18.TabIndex = 13;
            this.label18.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(98, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 15);
            this.label19.TabIndex = 12;
            this.label19.Text = "손익";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(171, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(15, 15);
            this.label17.TabIndex = 11;
            this.label17.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(98, 191);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 15);
            this.label16.TabIndex = 10;
            this.label16.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(98, 166);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 15);
            this.label15.TabIndex = 9;
            this.label15.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(98, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 15);
            this.label14.TabIndex = 8;
            this.label14.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(98, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 15);
            this.label13.TabIndex = 7;
            this.label13.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(98, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 15);
            this.label12.TabIndex = 6;
            this.label12.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 5;
            this.label11.Text = "현재잔고";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "수익률";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "평단가";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "가능수량";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "매수량";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "현재가";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.unTradedListView);
            this.groupBox3.Location = new System.Drawing.Point(1181, 559);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 213);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "미체결 잔고";
            // 
            // unTradedListView
            // 
            this.unTradedListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unTradedListView.HideSelection = false;
            this.unTradedListView.Location = new System.Drawing.Point(3, 21);
            this.unTradedListView.Name = "unTradedListView";
            this.unTradedListView.Size = new System.Drawing.Size(274, 189);
            this.unTradedListView.TabIndex = 0;
            this.unTradedListView.UseCompatibleStateImageBehavior = false;
            // 
            // curTimeLabel
            // 
            this.curTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.curTimeLabel.AutoSize = true;
            this.curTimeLabel.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.curTimeLabel.Location = new System.Drawing.Point(1128, 27);
            this.curTimeLabel.Name = "curTimeLabel";
            this.curTimeLabel.Size = new System.Drawing.Size(188, 30);
            this.curTimeLabel.TabIndex = 18;
            this.curTimeLabel.Text = "현재시간 : 0";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.Location = new System.Drawing.Point(1178, 104);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 17);
            this.label21.TabIndex = 19;
            this.label21.Text = "고가";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.Location = new System.Drawing.Point(1178, 138);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 17);
            this.label22.TabIndex = 20;
            this.label22.Text = "저가";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.Location = new System.Drawing.Point(1178, 178);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(44, 17);
            this.label23.TabIndex = 21;
            this.label23.Text = "시가";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.Location = new System.Drawing.Point(1178, 219);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(44, 17);
            this.label24.TabIndex = 22;
            this.label24.Text = "종가";
            // 
            // lastLabel
            // 
            this.lastLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lastLabel.AutoSize = true;
            this.lastLabel.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lastLabel.Location = new System.Drawing.Point(1254, 219);
            this.lastLabel.Name = "lastLabel";
            this.lastLabel.Size = new System.Drawing.Size(17, 17);
            this.lastLabel.TabIndex = 26;
            this.lastLabel.Text = "0";
            // 
            // startLabel
            // 
            this.startLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startLabel.AutoSize = true;
            this.startLabel.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.startLabel.Location = new System.Drawing.Point(1254, 178);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(17, 17);
            this.startLabel.TabIndex = 25;
            this.startLabel.Text = "0";
            // 
            // lowLabel
            // 
            this.lowLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lowLabel.AutoSize = true;
            this.lowLabel.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lowLabel.Location = new System.Drawing.Point(1254, 138);
            this.lowLabel.Name = "lowLabel";
            this.lowLabel.Size = new System.Drawing.Size(17, 17);
            this.lowLabel.TabIndex = 24;
            this.lowLabel.Text = "0";
            // 
            // highLabel
            // 
            this.highLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.highLabel.AutoSize = true;
            this.highLabel.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.highLabel.Location = new System.Drawing.Point(1254, 104);
            this.highLabel.Name = "highLabel";
            this.highLabel.Size = new System.Drawing.Size(17, 17);
            this.highLabel.TabIndex = 23;
            this.highLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 986);
            this.Controls.Add(this.lastLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.lowLabel);
            this.Controls.Add(this.highLabel);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.curTimeLabel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.curLocPowerLabel);
            this.Controls.Add(this.curLocLabel);
            this.Controls.Add(this.nDelaySecondTextBox);
            this.Controls.Add(this.forward60);
            this.Controls.Add(this.back60);
            this.Controls.Add(this.forward10);
            this.Controls.Add(this.forward5);
            this.Controls.Add(this.back10);
            this.Controls.Add(this.back5);
            this.Controls.Add(this.loadingPanel);
            this.Controls.Add(this.compLocButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.sCodeTextBox);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.historyChart);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).EndInit();
            this.loadingPanel.ResumeLayout(false);
            this.loadingPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart historyChart;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox sCodeTextBox;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button compLocButton;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button back5;
        private System.Windows.Forms.Button back10;
        private System.Windows.Forms.Button forward5;
        private System.Windows.Forms.Button forward10;
        private System.Windows.Forms.Button back60;
        private System.Windows.Forms.Button forward60;
        private System.Windows.Forms.TextBox nDelaySecondTextBox;
        private System.Windows.Forms.Label curLocLabel;
        private System.Windows.Forms.Label curLocPowerLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button sellButton;
        private System.Windows.Forms.Button buyButton;
        private System.Windows.Forms.TextBox sellVolumeTxtBox;
        private System.Windows.Forms.TextBox sellPriceTxtBox;
        private System.Windows.Forms.TextBox buyVolumeTxtBox;
        private System.Windows.Forms.TextBox buyPriceTxtBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView unTradedListView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label curTimeLabel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lastLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label lowLabel;
        private System.Windows.Forms.Label highLabel;
    }
}

