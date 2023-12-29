
namespace AtoIndicator.View
{
    partial class Configuration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fixedGroupBox = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.floorLabel = new System.Windows.Forms.Label();
            this.floorUpButton = new System.Windows.Forms.Button();
            this.floorDownButton = new System.Windows.Forms.Button();
            this.ceilingLabel = new System.Windows.Forms.Label();
            this.ceilingUpButton = new System.Windows.Forms.Button();
            this.ceilingDownButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.delayPaddingLabel = new System.Windows.Forms.Label();
            this.delayPaddingUpButton = new System.Windows.Forms.Button();
            this.delayPaddingDownButton = new System.Windows.Forms.Button();
            this.delayTimeLabel = new System.Windows.Forms.Label();
            this.delayTimeUpButton = new System.Windows.Forms.Button();
            this.delayTimeDownButton = new System.Windows.Forms.Button();
            this.gapCheckBox = new System.Windows.Forms.CheckBox();
            this.fixedGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // fixedGroupBox
            // 
            this.fixedGroupBox.Controls.Add(this.gapCheckBox);
            this.fixedGroupBox.Controls.Add(this.label1);
            this.fixedGroupBox.Controls.Add(this.label2);
            this.fixedGroupBox.Controls.Add(this.delayPaddingLabel);
            this.fixedGroupBox.Controls.Add(this.delayPaddingUpButton);
            this.fixedGroupBox.Controls.Add(this.delayPaddingDownButton);
            this.fixedGroupBox.Controls.Add(this.delayTimeLabel);
            this.fixedGroupBox.Controls.Add(this.delayTimeUpButton);
            this.fixedGroupBox.Controls.Add(this.delayTimeDownButton);
            this.fixedGroupBox.Controls.Add(this.label10);
            this.fixedGroupBox.Controls.Add(this.label9);
            this.fixedGroupBox.Controls.Add(this.floorLabel);
            this.fixedGroupBox.Controls.Add(this.floorUpButton);
            this.fixedGroupBox.Controls.Add(this.floorDownButton);
            this.fixedGroupBox.Controls.Add(this.ceilingLabel);
            this.fixedGroupBox.Controls.Add(this.ceilingUpButton);
            this.fixedGroupBox.Controls.Add(this.ceilingDownButton);
            this.fixedGroupBox.Location = new System.Drawing.Point(24, 25);
            this.fixedGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fixedGroupBox.Name = "fixedGroupBox";
            this.fixedGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fixedGroupBox.Size = new System.Drawing.Size(324, 159);
            this.fixedGroupBox.TabIndex = 28;
            this.fixedGroupBox.TabStop = false;
            this.fixedGroupBox.Text = "고정식 조정";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 544;
            this.label10.Text = "하한선";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 543;
            this.label9.Text = "상한선";
            // 
            // floorLabel
            // 
            this.floorLabel.AutoSize = true;
            this.floorLabel.Location = new System.Drawing.Point(183, 119);
            this.floorLabel.Name = "floorLabel";
            this.floorLabel.Size = new System.Drawing.Size(39, 12);
            this.floorLabel.TabIndex = 542;
            this.floorLabel.Text = "-0.015";
            // 
            // floorUpButton
            // 
            this.floorUpButton.Location = new System.Drawing.Point(249, 116);
            this.floorUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.floorUpButton.Name = "floorUpButton";
            this.floorUpButton.Size = new System.Drawing.Size(22, 18);
            this.floorUpButton.TabIndex = 541;
            this.floorUpButton.Text = "▶";
            this.floorUpButton.UseVisualStyleBackColor = true;
            // 
            // floorDownButton
            // 
            this.floorDownButton.Location = new System.Drawing.Point(147, 116);
            this.floorDownButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.floorDownButton.Name = "floorDownButton";
            this.floorDownButton.Size = new System.Drawing.Size(22, 18);
            this.floorDownButton.TabIndex = 540;
            this.floorDownButton.Text = "◀";
            this.floorDownButton.UseVisualStyleBackColor = true;
            // 
            // ceilingLabel
            // 
            this.ceilingLabel.AutoSize = true;
            this.ceilingLabel.Location = new System.Drawing.Point(195, 90);
            this.ceilingLabel.Name = "ceilingLabel";
            this.ceilingLabel.Size = new System.Drawing.Size(27, 12);
            this.ceilingLabel.TabIndex = 539;
            this.ceilingLabel.Text = "0.08";
            // 
            // ceilingUpButton
            // 
            this.ceilingUpButton.Location = new System.Drawing.Point(249, 84);
            this.ceilingUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ceilingUpButton.Name = "ceilingUpButton";
            this.ceilingUpButton.Size = new System.Drawing.Size(22, 18);
            this.ceilingUpButton.TabIndex = 538;
            this.ceilingUpButton.Text = "▶";
            this.ceilingUpButton.UseVisualStyleBackColor = true;
            // 
            // ceilingDownButton
            // 
            this.ceilingDownButton.Location = new System.Drawing.Point(147, 87);
            this.ceilingDownButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ceilingDownButton.Name = "ceilingDownButton";
            this.ceilingDownButton.Size = new System.Drawing.Size(22, 18);
            this.ceilingDownButton.TabIndex = 537;
            this.ceilingDownButton.Text = "◀";
            this.ceilingDownButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 552;
            this.label1.Text = "유예패딩";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 551;
            this.label2.Text = "유예시간";
            // 
            // delayPaddingLabel
            // 
            this.delayPaddingLabel.AutoSize = true;
            this.delayPaddingLabel.Location = new System.Drawing.Point(183, 54);
            this.delayPaddingLabel.Name = "delayPaddingLabel";
            this.delayPaddingLabel.Size = new System.Drawing.Size(11, 12);
            this.delayPaddingLabel.TabIndex = 550;
            this.delayPaddingLabel.Text = "0";
            // 
            // delayPaddingUpButton
            // 
            this.delayPaddingUpButton.Location = new System.Drawing.Point(249, 51);
            this.delayPaddingUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delayPaddingUpButton.Name = "delayPaddingUpButton";
            this.delayPaddingUpButton.Size = new System.Drawing.Size(22, 18);
            this.delayPaddingUpButton.TabIndex = 549;
            this.delayPaddingUpButton.Text = "▶";
            this.delayPaddingUpButton.UseVisualStyleBackColor = true;
            // 
            // delayPaddingDownButton
            // 
            this.delayPaddingDownButton.Location = new System.Drawing.Point(147, 51);
            this.delayPaddingDownButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delayPaddingDownButton.Name = "delayPaddingDownButton";
            this.delayPaddingDownButton.Size = new System.Drawing.Size(22, 18);
            this.delayPaddingDownButton.TabIndex = 548;
            this.delayPaddingDownButton.Text = "◀";
            this.delayPaddingDownButton.UseVisualStyleBackColor = true;
            // 
            // delayTimeLabel
            // 
            this.delayTimeLabel.AutoSize = true;
            this.delayTimeLabel.Location = new System.Drawing.Point(199, 25);
            this.delayTimeLabel.Name = "delayTimeLabel";
            this.delayTimeLabel.Size = new System.Drawing.Size(11, 12);
            this.delayTimeLabel.TabIndex = 547;
            this.delayTimeLabel.Text = "0";
            // 
            // delayTimeUpButton
            // 
            this.delayTimeUpButton.Location = new System.Drawing.Point(249, 19);
            this.delayTimeUpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delayTimeUpButton.Name = "delayTimeUpButton";
            this.delayTimeUpButton.Size = new System.Drawing.Size(22, 18);
            this.delayTimeUpButton.TabIndex = 546;
            this.delayTimeUpButton.Text = "▶";
            this.delayTimeUpButton.UseVisualStyleBackColor = true;
            // 
            // delayTimeDownButton
            // 
            this.delayTimeDownButton.Location = new System.Drawing.Point(147, 22);
            this.delayTimeDownButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delayTimeDownButton.Name = "delayTimeDownButton";
            this.delayTimeDownButton.Size = new System.Drawing.Size(22, 18);
            this.delayTimeDownButton.TabIndex = 545;
            this.delayTimeDownButton.Text = "◀";
            this.delayTimeDownButton.UseVisualStyleBackColor = true;
            // 
            // gapCheckBox
            // 
            this.gapCheckBox.AutoSize = true;
            this.gapCheckBox.Location = new System.Drawing.Point(0, 143);
            this.gapCheckBox.Name = "gapCheckBox";
            this.gapCheckBox.Size = new System.Drawing.Size(49, 16);
            this.gapCheckBox.TabIndex = 553;
            this.gapCheckBox.Text = "GAP";
            this.gapCheckBox.UseVisualStyleBackColor = true;
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 234);
            this.Controls.Add(this.fixedGroupBox);
            this.Name = "Configuration";
            this.Text = "Configuration";
            this.fixedGroupBox.ResumeLayout(false);
            this.fixedGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fixedGroupBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label floorLabel;
        private System.Windows.Forms.Button floorUpButton;
        private System.Windows.Forms.Button floorDownButton;
        private System.Windows.Forms.Label ceilingLabel;
        private System.Windows.Forms.Button ceilingUpButton;
        private System.Windows.Forms.Button ceilingDownButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label delayPaddingLabel;
        private System.Windows.Forms.Button delayPaddingUpButton;
        private System.Windows.Forms.Button delayPaddingDownButton;
        private System.Windows.Forms.Label delayTimeLabel;
        private System.Windows.Forms.Button delayTimeUpButton;
        private System.Windows.Forms.Button delayTimeDownButton;
        private System.Windows.Forms.CheckBox gapCheckBox;
    }
}