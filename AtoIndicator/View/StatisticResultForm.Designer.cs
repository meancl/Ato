
namespace AtoIndicator.View.StatisticResult
{
    partial class StatisticResultForm
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
            this.updateButton = new System.Windows.Forms.Button();
            this.sharedTimeLabel = new System.Windows.Forms.Label();
            this.qCheckBox = new System.Windows.Forms.CheckBox();
            this.kCheckBox = new System.Windows.Forms.CheckBox();
            this.jCheckBox = new System.Windows.Forms.CheckBox();
            this.rCheckBox = new System.Windows.Forms.CheckBox();
            this.eCheckBox = new System.Windows.Forms.CheckBox();
            this.wCheckBox = new System.Windows.Forms.CheckBox();
            this.hit38CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(672, 13);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(108, 35);
            this.updateButton.TabIndex = 0;
            this.updateButton.Text = "업데이트( U )";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // sharedTimeLabel
            // 
            this.sharedTimeLabel.AutoSize = true;
            this.sharedTimeLabel.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sharedTimeLabel.Location = new System.Drawing.Point(471, 18);
            this.sharedTimeLabel.Name = "sharedTimeLabel";
            this.sharedTimeLabel.Size = new System.Drawing.Size(0, 19);
            this.sharedTimeLabel.TabIndex = 1;
            // 
            // qCheckBox
            // 
            this.qCheckBox.AutoSize = true;
            this.qCheckBox.Location = new System.Drawing.Point(12, 12);
            this.qCheckBox.Name = "qCheckBox";
            this.qCheckBox.Size = new System.Drawing.Size(31, 16);
            this.qCheckBox.TabIndex = 2;
            this.qCheckBox.Text = "q";
            this.qCheckBox.UseVisualStyleBackColor = true;
            // 
            // kCheckBox
            // 
            this.kCheckBox.AutoSize = true;
            this.kCheckBox.Location = new System.Drawing.Point(278, 12);
            this.kCheckBox.Name = "kCheckBox";
            this.kCheckBox.Size = new System.Drawing.Size(30, 16);
            this.kCheckBox.TabIndex = 3;
            this.kCheckBox.Text = "k";
            this.kCheckBox.UseVisualStyleBackColor = true;
            // 
            // jCheckBox
            // 
            this.jCheckBox.AutoSize = true;
            this.jCheckBox.Location = new System.Drawing.Point(224, 12);
            this.jCheckBox.Name = "jCheckBox";
            this.jCheckBox.Size = new System.Drawing.Size(27, 16);
            this.jCheckBox.TabIndex = 4;
            this.jCheckBox.Text = "j";
            this.jCheckBox.UseVisualStyleBackColor = true;
            // 
            // rCheckBox
            // 
            this.rCheckBox.AutoSize = true;
            this.rCheckBox.Location = new System.Drawing.Point(172, 12);
            this.rCheckBox.Name = "rCheckBox";
            this.rCheckBox.Size = new System.Drawing.Size(28, 16);
            this.rCheckBox.TabIndex = 5;
            this.rCheckBox.Text = "r";
            this.rCheckBox.UseVisualStyleBackColor = true;
            // 
            // eCheckBox
            // 
            this.eCheckBox.AutoSize = true;
            this.eCheckBox.Location = new System.Drawing.Point(118, 12);
            this.eCheckBox.Name = "eCheckBox";
            this.eCheckBox.Size = new System.Drawing.Size(31, 16);
            this.eCheckBox.TabIndex = 6;
            this.eCheckBox.Text = "e";
            this.eCheckBox.UseVisualStyleBackColor = true;
            // 
            // wCheckBox
            // 
            this.wCheckBox.AutoSize = true;
            this.wCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.wCheckBox.Location = new System.Drawing.Point(61, 12);
            this.wCheckBox.Name = "wCheckBox";
            this.wCheckBox.Size = new System.Drawing.Size(34, 16);
            this.wCheckBox.TabIndex = 7;
            this.wCheckBox.Text = "w";
            this.wCheckBox.UseVisualStyleBackColor = true;
            // 
            // hit38CheckBox
            // 
            this.hit38CheckBox.AutoSize = true;
            this.hit38CheckBox.Location = new System.Drawing.Point(329, 12);
            this.hit38CheckBox.Name = "hit38CheckBox";
            this.hit38CheckBox.Size = new System.Drawing.Size(49, 16);
            this.hit38CheckBox.TabIndex = 8;
            this.hit38CheckBox.Text = "hit38";
            this.hit38CheckBox.UseVisualStyleBackColor = true;
            // 
            // StatisticResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.hit38CheckBox);
            this.Controls.Add(this.wCheckBox);
            this.Controls.Add(this.eCheckBox);
            this.Controls.Add(this.rCheckBox);
            this.Controls.Add(this.jCheckBox);
            this.Controls.Add(this.kCheckBox);
            this.Controls.Add(this.qCheckBox);
            this.Controls.Add(this.sharedTimeLabel);
            this.Controls.Add(this.updateButton);
            this.Name = "StatisticResultForm";
            this.Text = "StatisticResultForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label sharedTimeLabel;
        private System.Windows.Forms.CheckBox qCheckBox;
        private System.Windows.Forms.CheckBox kCheckBox;
        private System.Windows.Forms.CheckBox jCheckBox;
        private System.Windows.Forms.CheckBox rCheckBox;
        private System.Windows.Forms.CheckBox eCheckBox;
        private System.Windows.Forms.CheckBox wCheckBox;
        private System.Windows.Forms.CheckBox hit38CheckBox;
    }
}