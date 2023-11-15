
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).BeginInit();
            this.loadingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // historyChart
            // 
            this.historyChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.historyChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.historyChart.Legends.Add(legend1);
            this.historyChart.Location = new System.Drawing.Point(26, 69);
            this.historyChart.Name = "historyChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Legend = "Legend1";
            series1.Name = "MinuteStick";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Ma20m";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series3.Legend = "Legend1";
            series3.Name = "Ma1h";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Yellow;
            series4.Legend = "Legend1";
            series4.Name = "Ma2h";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Green;
            series5.Legend = "Legend1";
            series5.Name = "Ma20mGap";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series6.Legend = "Legend1";
            series6.Name = "Ma1hGap";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Purple;
            series7.Legend = "Legend1";
            series7.Name = "Ma2hGap";
            this.historyChart.Series.Add(series1);
            this.historyChart.Series.Add(series2);
            this.historyChart.Series.Add(series3);
            this.historyChart.Series.Add(series4);
            this.historyChart.Series.Add(series5);
            this.historyChart.Series.Add(series6);
            this.historyChart.Series.Add(series7);
            this.historyChart.Size = new System.Drawing.Size(1071, 613);
            this.historyChart.TabIndex = 0;
            this.historyChart.Text = "chart1";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(171, 21);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(112, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // sCodeTextBox
            // 
            this.sCodeTextBox.Location = new System.Drawing.Point(67, 21);
            this.sCodeTextBox.Name = "sCodeTextBox";
            this.sCodeTextBox.Size = new System.Drawing.Size(100, 21);
            this.sCodeTextBox.TabIndex = 2;
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("굴림", 14F);
            this.playButton.Location = new System.Drawing.Point(552, 12);
            this.playButton.Name = "playButton";
            this.playButton.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.playButton.Size = new System.Drawing.Size(32, 34);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "❚❚";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // compLocButton
            // 
            this.compLocButton.Location = new System.Drawing.Point(26, 21);
            this.compLocButton.Name = "compLocButton";
            this.compLocButton.Size = new System.Drawing.Size(21, 21);
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
            this.loadingPanel.Location = new System.Drawing.Point(180, 216);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(608, 279);
            this.loadingPanel.TabIndex = 5;
            this.loadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(241, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading......";
            // 
            // back5
            // 
            this.back5.Location = new System.Drawing.Point(511, 12);
            this.back5.Name = "back5";
            this.back5.Size = new System.Drawing.Size(32, 34);
            this.back5.TabIndex = 6;
            this.back5.Text = "-5";
            this.back5.UseVisualStyleBackColor = true;
            // 
            // back10
            // 
            this.back10.Location = new System.Drawing.Point(473, 12);
            this.back10.Name = "back10";
            this.back10.Size = new System.Drawing.Size(32, 34);
            this.back10.TabIndex = 7;
            this.back10.Text = "-10";
            this.back10.UseVisualStyleBackColor = true;
            // 
            // forward5
            // 
            this.forward5.Location = new System.Drawing.Point(592, 12);
            this.forward5.Name = "forward5";
            this.forward5.Size = new System.Drawing.Size(32, 34);
            this.forward5.TabIndex = 8;
            this.forward5.Text = "+5";
            this.forward5.UseVisualStyleBackColor = true;
            // 
            // forward10
            // 
            this.forward10.Location = new System.Drawing.Point(631, 12);
            this.forward10.Name = "forward10";
            this.forward10.Size = new System.Drawing.Size(32, 34);
            this.forward10.TabIndex = 9;
            this.forward10.Text = "+10";
            this.forward10.UseVisualStyleBackColor = true;
            // 
            // back60
            // 
            this.back60.Location = new System.Drawing.Point(435, 12);
            this.back60.Name = "back60";
            this.back60.Size = new System.Drawing.Size(32, 34);
            this.back60.TabIndex = 10;
            this.back60.Text = "-60";
            this.back60.UseVisualStyleBackColor = true;
            // 
            // forward60
            // 
            this.forward60.Location = new System.Drawing.Point(669, 12);
            this.forward60.Name = "forward60";
            this.forward60.Size = new System.Drawing.Size(32, 34);
            this.forward60.TabIndex = 11;
            this.forward60.Text = "+60";
            this.forward60.UseVisualStyleBackColor = true;
            // 
            // nDelaySecondTextBox
            // 
            this.nDelaySecondTextBox.Location = new System.Drawing.Point(399, 20);
            this.nDelaySecondTextBox.Name = "nDelaySecondTextBox";
            this.nDelaySecondTextBox.Size = new System.Drawing.Size(30, 21);
            this.nDelaySecondTextBox.TabIndex = 12;
            this.nDelaySecondTextBox.Text = "1";
            // 
            // curLocLabel
            // 
            this.curLocLabel.AutoSize = true;
            this.curLocLabel.Location = new System.Drawing.Point(943, 331);
            this.curLocLabel.Name = "curLocLabel";
            this.curLocLabel.Size = new System.Drawing.Size(38, 12);
            this.curLocLabel.TabIndex = 13;
            this.curLocLabel.Text = "label2";
            // 
            // curLocPowerLabel
            // 
            this.curLocPowerLabel.AutoSize = true;
            this.curLocPowerLabel.Location = new System.Drawing.Point(945, 366);
            this.curLocPowerLabel.Name = "curLocPowerLabel";
            this.curLocPowerLabel.Size = new System.Drawing.Size(38, 12);
            this.curLocPowerLabel.TabIndex = 14;
            this.curLocPowerLabel.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 694);
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
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).EndInit();
            this.loadingPanel.ResumeLayout(false);
            this.loadingPanel.PerformLayout();
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
    }
}

