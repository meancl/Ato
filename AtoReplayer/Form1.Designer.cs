
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.historyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.sCodeTextBox = new System.Windows.Forms.TextBox();
            this.playButton = new System.Windows.Forms.Button();
            this.compLocButton = new System.Windows.Forms.Button();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).BeginInit();
            this.loadingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // historyChart
            // 
            this.historyChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.historyChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.historyChart.Legends.Add(legend2);
            this.historyChart.Location = new System.Drawing.Point(22, 41);
            this.historyChart.Name = "historyChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series2.Legend = "Legend1";
            series2.Name = "MinuteStick";
            series2.YValuesPerPoint = 4;
            this.historyChart.Series.Add(series2);
            this.historyChart.Size = new System.Drawing.Size(623, 397);
            this.historyChart.TabIndex = 0;
            this.historyChart.Text = "chart1";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(167, 14);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(112, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // sCodeTextBox
            // 
            this.sCodeTextBox.Location = new System.Drawing.Point(63, 14);
            this.sCodeTextBox.Name = "sCodeTextBox";
            this.sCodeTextBox.Size = new System.Drawing.Size(100, 21);
            this.sCodeTextBox.TabIndex = 2;
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("굴림", 14F);
            this.playButton.Location = new System.Drawing.Point(709, 41);
            this.playButton.Name = "playButton";
            this.playButton.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.playButton.Size = new System.Drawing.Size(32, 34);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "❚❚";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // compLocButton
            // 
            this.compLocButton.Location = new System.Drawing.Point(22, 14);
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
            this.loadingPanel.Location = new System.Drawing.Point(105, 93);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

